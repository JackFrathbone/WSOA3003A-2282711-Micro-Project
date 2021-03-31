using System.Collections.Generic;
using UnityEngine;

public class ActorStats : MonoBehaviour
{
    //Live Battle Stats
    public int currentRisk;
    public int currentCombo;
    public int currentWounds;

    //Character Stats
    public int attackSkill;
    public int defenceSkill;
    ////Sets max wounds
    public int health;
    ////Who goes first
    public int initiativeSkill;

    //BodyParts
    public List<BodyPart> bodyParts = new List<BodyPart>();

    //Available Action
    public List<ActorActions> actions = new List<ActorActions>();

    public BattleManager battleManager;

    private void Start()
    {
        battleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleManager>();

        foreach (BodyPart bodyPart in bodyParts)
        {
            bodyPart.partOwner = this;
        }
    }

    public string GetStatDescription(ActorStats otherStat)
    {
        string attackDif = "";

        //Compares these stats to another in order to get difference
        if (attackSkill > otherStat.attackSkill)
        {
            attackDif = "+" + (attackSkill - otherStat.attackSkill).ToString();
        }
        else if(attackSkill < otherStat.attackSkill)
        {
            attackDif = "-" + (otherStat.attackSkill - attackSkill).ToString();
        } 
        else
        {
            attackDif = "0";
        }

        string defenceDif = "";

        //Compares these stats to another in order to get difference
        if (defenceSkill > otherStat.defenceSkill)
        {
            defenceDif = "+" + (defenceSkill - otherStat.defenceSkill).ToString();
        }
        else if (defenceSkill < otherStat.defenceSkill)
        {
            defenceDif = "-" + (otherStat.defenceSkill - defenceSkill).ToString();
        }
        else
        {
            defenceDif = "0";
        }

        string initDif = "";

        //Compares these stats to another in order to get difference
        if (initiativeSkill > otherStat.initiativeSkill)
        {
            initDif = "+" + (initiativeSkill - otherStat.initiativeSkill).ToString();
        }
        else if (initiativeSkill < otherStat.initiativeSkill)
        {
            initDif = "-" + (otherStat.initiativeSkill - initiativeSkill).ToString();
        }
        else
        {
            initDif = "0";
        }

        return "Attack Difference: " + attackDif + "\n" + "Defence Difference: " + defenceDif + "\n" + "Max Wounds: " + health + "\n" + "Initiative: " + initDif;
    }

    public void SetRisk(int i)
    {
        currentRisk += i;

        if (currentRisk > 100)
        {
            currentRisk = 100;
        }
        else if (currentRisk < 0)
        {
            currentRisk = 0;
        }
    }

    public void SetCombo(int i)
    {
        currentCombo += i;

        if (currentCombo > 100)
        {
            currentCombo = 100;
        }
        else if (currentCombo < 0)
        {
            currentCombo = 0;
        }
    }

    public void AddWound(BodyPart part)
    {
        currentWounds++;
        if(currentWounds >= health)
        {
            battleManager.ActorDeath(this);
        }

        foreach (BodyPart bodyPart in bodyParts)
        {
            if (part == bodyPart)
            {
                part.isWounded = true;
            }
        }
    }
}
