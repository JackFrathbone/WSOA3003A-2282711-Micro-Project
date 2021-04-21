using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Move(Vector2 newPos)
    {
        gameObject.transform.position = newPos;
    }

    public void StartBattle(ActorStats enemyStats)
    {
        gameManager.ActivateBattle(enemyStats);
    }

    public void UseStaircase(Staircase staircase , Vector2 newPos)
    {
        staircase.UseStairs();
        gameObject.transform.position = newPos;
    }

    public void UseItem()
    {
        gameManager.battleManager.playerStats.health += 1;
        gameManager.levelUpVisual.SetActive(true);
        gameManager.levelUpText.text = "You gained +1 Health";
    }
}
