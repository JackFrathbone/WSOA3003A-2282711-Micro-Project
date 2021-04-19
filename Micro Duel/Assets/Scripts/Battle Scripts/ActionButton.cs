using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public ActorActions action;
    public BattleManager battleManager;

    public void ActivateButton()
    {
        if(action.CheckUseOnSelf() != true)
        {
            string description = action.GenerateDescription();
            battleManager.ActivateAction(action.name, description, action);
        }
        else
        {
            battleManager.UseActionOnSelf(battleManager.playerStats, action);
        }
       
    }
}
