using UnityEngine;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    public string playerType;

    public TextMeshProUGUI woundText;
    public TextMeshProUGUI vulnText;
    public TextMeshProUGUI comboText;

    public int currentVulnerability;
    public int currentWounds;
    public int combo;
    public int maxWounds;

    public bool headWound;
    public bool torsoWound;
    public bool armWound;
    public bool legWound;

    public void UpdateWound(string bodyPart)
    {
        currentWounds += 1;
        woundText.text = "Wounds: " + currentWounds.ToString() + "/" +maxWounds.ToString();

        //Check what part is hit
        if(bodyPart == "Head")
        {
            headWound = true;
            UpdateVulnerability(15);
        }
        else if(bodyPart == "Torso")
        {
            torsoWound = true;
        }
        else if(bodyPart == "Arms")
        {
            armWound = true;
        }
        else if(bodyPart == "Legs")
        {
            legWound = true;
        }

        if(currentWounds > maxWounds)
        {
            //end match
        }
    }

    public void UpdateVulnerability(int i)
    {
        currentVulnerability += i;
        if(currentVulnerability <= 0)
        {
            currentVulnerability = 0;

            if (headWound)
            {
                currentVulnerability = 15;
            }
        } 
        else if(currentVulnerability > 100)
        {
            currentVulnerability = 100;
        }

        vulnText.text = "Vuln: " + currentVulnerability.ToString() + "%";
    }

    public void UpdateCombo(int i)
    {
        combo += i;
        if (combo < 0)
        {
            combo = 0;
        }
        else if (combo > 100)
        {
            combo = 100;
        }

        comboText.text = "Combo: " + combo.ToString() + "%";
    }
}
