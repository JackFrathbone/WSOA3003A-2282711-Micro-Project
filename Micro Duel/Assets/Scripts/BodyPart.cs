using UnityEngine;
public enum PartType
{
    Head,
    Torso,
    Arms,
    Legs
}
public class BodyPart : MonoBehaviour
{
    public PartType partType;
    public ActorStats partOwner;
    public GameObject woundDisplay;

    public bool isWounded;

    private void Start()
    {
        woundDisplay.SetActive(false);
    }

    public void DisplayWound()
    {
        woundDisplay.SetActive(true);
        woundDisplay.GetComponentInChildren<ParticleSystem>().Play();
    }

    public string GetPartWoundingDescription()
    {
        string desc = "";

        if (partType == PartType.Head)
        {
            desc = "lowers ability to wound";
        }
        else if (partType == PartType.Torso)
        {
            desc = "increase chance of being wounded";
        }
        else if (partType == PartType.Arms)
        {
            desc = "lower ability to hit";
        }
        else if (partType == PartType.Legs)
        {
            desc = "increase chance of being hit";
        }

        return desc;
    }

    public string GetPartDescription()
    {
        string desc = "";
        if (partType == PartType.Head)
        {
            if (!isWounded)
            {
                desc = "No effects or wounds on this bodypart";
            }
            else
            {
                desc = "The head has been wounded and makes it harder to deal wounding blows";
            }
        }
        else if (partType == PartType.Torso)
        {
            if (!isWounded)
            {
                desc = "No effects or wounds on this bodypart";
            }
            else
            {
                desc = "The torso has been wounded and has increased the chance of being wounded again";
            }
        }
        else if (partType == PartType.Arms)
        {
            if (!isWounded)
            {
                desc = "No effects or wounds on this bodypart";
            }
            else
            {
                desc = "The arms have been wounded and have made it harder to hit";
            }
        }
        else if (partType == PartType.Legs)
        {
            if (!isWounded)
            {
                desc = "No effects or wounds on this bodypart";
            }
            else
            {
                desc = "The  legs have been wounded and have increased the chance of being hit";
            }
        }
        return desc;
    }

    private void OnMouseDown()
    {
        partOwner.battleManager.ActivateBodyPart(this);
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 245f, 0.5f);
        partOwner.battleManager.ShowChanceTooltip(this);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        partOwner.battleManager.HideChanceToolTip();
    }
}
