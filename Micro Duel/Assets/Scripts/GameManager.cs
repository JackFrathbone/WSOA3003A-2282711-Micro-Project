using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _introOverlay;
    [SerializeField] GameObject _winOverlay;
    [SerializeField] GameObject _loseOverlay;

    private BattleManager battleManager;
    public ActorStats playerStats;

    //Visual for battle mode
    public GameObject battleModeVisuals;
    public GameObject mapModeVisuals;

    private void Start()
    {
        battleManager = GetComponent<BattleManager>();
        _introOverlay.SetActive(true);
        battleModeVisuals.SetActive(false);
    }

    public void ActivateBattle(ActorStats enemySelected)
    {
        battleModeVisuals.SetActive(true);
        mapModeVisuals.SetActive(false);
        battleManager.StartBattle(playerStats, enemySelected);
    }

    public void BackToMap()
    {
        battleModeVisuals.SetActive(false);
        mapModeVisuals.SetActive(true);

        battleManager.ClearMatch();
    }

    public void WinMatch()
    {
        _winOverlay.SetActive(true);
    }

    public void LoseMatch()
    {
        _loseOverlay.SetActive(true);
    }

    public void RemoveOverlays()
    {
        _winOverlay.SetActive(false);
        _loseOverlay.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
