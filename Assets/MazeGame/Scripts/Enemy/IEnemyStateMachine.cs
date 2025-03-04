public interface IEnemyStateMachine
{
    void Enter(EnemyBase enemy);  // Called when entering the state
    void Update(EnemyBase enemy); // Called every frame in this state
    void Exit(EnemyBase enemy);   // Called when exiting the state
    

}
