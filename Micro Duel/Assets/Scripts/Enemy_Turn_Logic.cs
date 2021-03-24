using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turn_Logic : MonoBehaviour
{
    public List<Player_Action> actions = new List<Player_Action>();
    public Player_Action disengageAction;

    public void StartTurn(Battle_Manager manager)
    {
        //Check the current status of the enemy
        bool cStatus = CheckStatus(manager);
        if(cStatus == true)
        {
            //If status is determined to be bad ie true then use the disengange move
            manager.EnemyAttackOnSelf(disengageAction);
        }
        else
        {
            manager.EnemyAttackOnPlayer(actions[Random.Range(0, actions.Count)], GetBodyPart());
        }

    }

    private string GetBodyPart()
    {
        int chance = Random.Range(0, 5);
        if(chance == 0)
        {
            return "Head";
        } 
        else if(chance == 1)
        {
            return "Torso";
        }
        else if (chance == 2)
        {
            return "Arms";
        }
        else return "Legs";
    }

    private bool CheckStatus(Battle_Manager manager)
    {
        if(manager.enemyStats.currentVulnerability > 45 && manager.enemyStats.combo < 50)
        {
            return true;
        }
        else if (manager.enemyStats.currentVulnerability > 80)
        {
            return true;
        }

        return false;
    }
}
