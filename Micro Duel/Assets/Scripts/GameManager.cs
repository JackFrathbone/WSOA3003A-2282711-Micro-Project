using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    //Visuals for map mode
    public GameObject levelUpVisual;
    public TextMeshProUGUI levelUpText;

    public GameObject statVisual;
    public TextMeshProUGUI statVisualText;

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

    public void WinMatch(string input)
    {
        _winOverlay.SetActive(true);
        levelUpVisual.SetActive(true);
        levelUpText.text = input;
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
