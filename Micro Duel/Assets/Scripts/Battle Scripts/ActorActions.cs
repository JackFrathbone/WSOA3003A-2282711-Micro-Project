using UnityEngine;

[CreateAssetMenu(menuName = "Action")]
public class ActorActions : ScriptableObject
{
    [SerializeField] int _chanceToHitBonus;
    [SerializeField] int _chanceToWoundBonus;
    [SerializeField] int _riskChange;
    [SerializeField] int _comboChange;

    [SerializeField] bool useOnSelf;

    public string GenerateDescription()
    {
        return "Chance to Hit Bonus: " + _chanceToHitBonus.ToString() + "\n" + "Chance to Wound Bonus: " + _chanceToWoundBonus.ToString() + "\n" + "Risk Change: " + _riskChange.ToString() + "\n" + "Combo Change: " + _comboChange.ToString() + "\n";
    }

    public int GetHitBonus()
    {
        return _chanceToHitBonus;
    }

    public int GetWoundBonus()
    {
        return _chanceToWoundBonus;
    }

    public int GetRiskChange()
    {
        return _riskChange;
    }

    public int GetComboChange()
    {
        return _comboChange;
    }

    public bool CheckUseOnSelf()
    {
        return useOnSelf;
    }
}
