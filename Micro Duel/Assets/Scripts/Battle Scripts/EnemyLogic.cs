using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int maxRisk;

    public List<ActorActions> actions = new List<ActorActions>();
    private ActorActions disengangeAction;

    public void BeginTurn(BattleManager battleManager)
    {
        actions.Clear();
        StartCoroutine(EnemyTurn(battleManager));
    }

    private IEnumerator EnemyTurn(BattleManager battleManager)
    {
        GetListOfAction(battleManager);
        yield return new WaitForSeconds(1f);

        ActorActions action = ChooseAction(battleManager);

        if (action.CheckUseOnSelf() == true)
        {
            battleManager.UseActionOnSelf(battleManager.enemyStats, action);
        }
        else
        {
            battleManager.UseActionOnOther(battleManager.enemyStats, battleManager.playerStats, action, ChooseBodyPart(battleManager));
        }
    }

    private ActorActions ChooseAction(BattleManager battleManager)
    {
        int chance = Random.Range(0, actions.Count);

        if (battleManager.enemyStats.currentRisk < maxRisk)
        {
            return actions[chance];
        }
        else
        {
            if (battleManager.playerStats.currentRisk >= 65 && battleManager.playerStats.currentRisk < 100)
            {
                int splitChance = Random.Range(0, 2);
                if (splitChance == 0)
                {
                    return actions[chance];
                }
                else
                {
                    return disengangeAction;
                }
            }
            else if(battleManager.playerStats.currentRisk  == 100)
            {
                return actions[chance];
            }

            else return disengangeAction;
        }
    }

    private BodyPart ChooseBodyPart(BattleManager battleManager)
    {
        int chance = Random.Range(0, battleManager.playerStats.bodyParts.Count);
        return battleManager.playerStats.bodyParts[chance];
    }

    private void GetListOfAction(BattleManager battleManager)
    {
        foreach (ActorActions action in battleManager.enemyStats.actions)
        {
            if (action.CheckUseOnSelf() != true)
            {
                actions.Add(action);
            }
            else
            {
                disengangeAction = action;
            }
        }
    }
}
