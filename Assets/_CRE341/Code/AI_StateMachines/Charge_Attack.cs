using UnityEngine;

public class Charge_Attack : StateMachineBehaviour
{

    GameObject Player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entering Charge Attack State");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On State Update ~ Charge Attack");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting Charge Attack State");
    }

    void Charge()
    {

    }

}
