using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell mazeCellPrefab;

    [SerializeField]
    private int mazeWidth;

    [SerializeField]
    private int mazeDepth;

    private MazeCell[,] mazeGrid;
    List<Vector3Int> possibleWallHorizontalPosition;
    List<Vector3Int> possibleWallVerticalPosition;

    [SerializeField] List<GameObject> props = new List<GameObject>();

    [SerializeField] List<GameObject> propPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> players = new List<GameObject>();

    [SerializeField] List<GameObject> playerPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> monsters = new List<GameObject>();

    [SerializeField] List<GameObject> monsterPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> boxes = new List<GameObject>();

    [SerializeField] List<GameObject> boxPrefabs = new List<GameObject>();

    public float minCollectableDistanceFromWall;
    public float minPlayerDistanceFromWall;
    public float minMonsterDistanceFromWall;
    public float minBoxDistanceFromWall;

    [SerializeField] float numberofProps;
    [SerializeField] float numberOfPlayers = 0.5f;
    [SerializeField] float numberOfMonsters = 0.5f;
    [SerializeField] float numberOfBoxes = 0.5f;

   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeDepth; z++)
            {
                mazeGrid[x, z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        possibleWallHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();

        GenerateMaze(null, mazeGrid[0, 0]);
        GetComponent<NavMeshSurface>().BuildNavMesh();

    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        new WaitForSeconds(0.05f);

        MazeCell nextCell;

        do
        {



            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        }
        while (nextCell != null);
        if (nextCell == null && props.Count <= numberofProps)
        {
            SpawnProps();
        }
        if (nextCell == null && players.Count <= numberOfPlayers)
        {
            SpawnPlayer();
        }
        if (nextCell == null && monsters.Count <= numberOfMonsters)
        {
            SpawnMonster();
        }
        if (nextCell == null && boxes.Count <= numberOfBoxes)
        {
            SpawnBox();
        }

    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();

    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < mazeWidth)
        {

            var cellToRight = mazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
    private void SpawnProps()
    {
        minCollectableDistanceFromWall = 3.5f;
        


        for (int i = 0; i < numberofProps; i++)
        {
            Vector3 randomPosition;
            bool validPosition;
            

            do
            {
                validPosition = true;
                randomPosition = new Vector3(
                    UnityEngine.Random.Range(0, mazeWidth),
                    0,
                    UnityEngine.Random.Range(0, mazeDepth)
                );

                // Check distance from all wall positions
                foreach (var wallPosition in possibleWallHorizontalPosition)
                {
                    if (Vector3.Distance(randomPosition, wallPosition) < minCollectableDistanceFromWall)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    foreach (var wallPosition in possibleWallVerticalPosition)
                    {
                        if (Vector3.Distance(randomPosition, wallPosition) < minCollectableDistanceFromWall)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                }

                
            } while (!validPosition);
            if (validPosition )
            {
                GameObject randomPropPrefab = propPrefabs[UnityEngine.Random.Range(0, propPrefabs.Count)];
                GameObject prop = Instantiate(randomPropPrefab, randomPosition, Quaternion.identity);
                props.Add(prop);

            }

         
        }
    }

    private void SpawnPlayer()
    {
        minPlayerDistanceFromWall = 3.5f;



        for (int i = 0; i < numberOfPlayers; i++)
        {
            Vector3 randomPosition;
            bool validPosition;


            do
            {
                validPosition = true;
                randomPosition = new Vector3(
                    UnityEngine.Random.Range(0, mazeWidth),
                    0,
                    UnityEngine.Random.Range(0, mazeDepth)
                );

                // Check distance from all wall positions
                foreach (var wallPosition in possibleWallHorizontalPosition)
                {
                    if (Vector3.Distance(randomPosition, wallPosition) < minPlayerDistanceFromWall)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    foreach (var wallPosition in possibleWallVerticalPosition)
                    {
                        if (Vector3.Distance(randomPosition, wallPosition) < minPlayerDistanceFromWall)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                }


            } while (!validPosition);
            if (validPosition)
            {
                GameObject randomPlayerPrefab = playerPrefabs[UnityEngine.Random.Range(0, playerPrefabs.Count)];
                GameObject player = Instantiate(randomPlayerPrefab, randomPosition, Quaternion.identity);
                players.Add(player);

            }


        }
    }

    private void SpawnMonster()
    {
        minMonsterDistanceFromWall = 2f;



        for (int i = 0; i < numberOfMonsters; i++)
        {
            Vector3 randomPosition;
            bool validPosition;


            do
            {
                validPosition = true;
                randomPosition = new Vector3(
                    UnityEngine.Random.Range(0, mazeWidth),
                    0,
                    UnityEngine.Random.Range(0, mazeDepth)
                );

                // Check distance from all wall positions
                foreach (var wallPosition in possibleWallHorizontalPosition)
                {
                    if (Vector3.Distance(randomPosition, wallPosition) < minMonsterDistanceFromWall)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    foreach (var wallPosition in possibleWallVerticalPosition)
                    {
                        if (Vector3.Distance(randomPosition, wallPosition) < minMonsterDistanceFromWall)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                }


            } while (!validPosition);
            if (validPosition)
            {
                GameObject randomMonsterPrefab = monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Count)];
                GameObject monster = Instantiate(randomMonsterPrefab, randomPosition, Quaternion.identity);
                monsters.Add(monster);

            }


        }
    }

    private void SpawnBox()
    {
        minBoxDistanceFromWall = 3.5f;



        for (int i = 0; i < numberOfBoxes; i++)
        {
            Vector3 randomPosition;
            bool validPosition;


            do
            {
                validPosition = true;
                randomPosition = new Vector3(
                    UnityEngine.Random.Range(0, mazeWidth),
                    0,
                    UnityEngine.Random.Range(0, mazeDepth)
                );

                // Check distance from all wall positions
                foreach (var wallPosition in possibleWallHorizontalPosition)
                {
                    if (Vector3.Distance(randomPosition, wallPosition) < minBoxDistanceFromWall)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    foreach (var wallPosition in possibleWallVerticalPosition)
                    {
                        if (Vector3.Distance(randomPosition, wallPosition) < minBoxDistanceFromWall)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                }


            } while (!validPosition);
            if (validPosition)
            {
                GameObject randomBoxPrefab = boxPrefabs[UnityEngine.Random.Range(0, boxPrefabs.Count)];
                GameObject box = Instantiate(randomBoxPrefab, randomPosition, Quaternion.identity);
                boxes.Add(box);

            }


        }
    }


}
