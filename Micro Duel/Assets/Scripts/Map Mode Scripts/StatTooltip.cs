using UnityEngine;

public class StatTooltip : MonoBehaviour
{
    public ActorStats playerStats;
    public ActorStats enemyStats;
    public BattleManager battleManager;

    private void Start()
    {
        enemyStats = GetComponent<ActorStats>();
        battleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleManager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorStats>();
    }

    private void OnMouseEnter()
    {

        battleManager.gameManager.statVisual.SetActive(true);
        battleManager.gameManager.statVisual.transform.position = gameObject.transform.position;
        battleManager.gameManager.statVisualText.text = playerStats.GetStatDescription(enemyStats);
    }

    private void OnMouseExit()
    {
        battleManager.gameManager.statVisual.SetActive(false);
    }
}
