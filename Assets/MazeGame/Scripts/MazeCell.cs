using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject frontWall;

    [SerializeField]
    private GameObject backWall;

    [SerializeField]
    private GameObject unvisitedBlock;

    public bool IsVisited {  get; private set; }

    public void Visit()
    {
        IsVisited = true;

       // unvisitedBlock.SetActive(false);
        Destroy(unvisitedBlock);
    }

    public void ClearLeftWall()
    {
        //leftWall.SetActive(false);
        Destroy(leftWall);
    }

    public void ClearRightWall()
    {
       // rightWall.SetActive(false);
        Destroy(rightWall);
    }

    public void ClearFrontWall()
    {
       // frontWall.SetActive(false);
        Destroy(frontWall);

    }

    public void ClearBackWall()
    {
       // backWall.SetActive(false);
        Destroy(backWall);
    }
}
