using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _introOverlay;
    [SerializeField] GameObject _winOverlay;
    [SerializeField] GameObject _loseOverlay;

    private BattleManager battleManager;
    public ActorStats playerStats;
    public ActorStats currentEnemy;

    private void Start()
    {
        battleManager = GetComponent<BattleManager>();
        _introOverlay.SetActive(true);
    }

    public void ActivateBattle()
    {
        battleManager.StartBattle(playerStats, currentEnemy);
    }

    public void WinMatch()
    {
        _winOverlay.SetActive(true);
    }

    public void LoseMatch()
    {
        _loseOverlay.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
