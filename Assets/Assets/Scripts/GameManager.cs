using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Start Screen")]
    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject PlayButton;

    [Header("Score")]
    [SerializeField] private TMP_Text score;

    [Header("Game Ready Section")]
    [SerializeField] private GameObject Ready;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text GameOverScore;
    [SerializeField] private TMP_Text GameOverBestScore;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PipeSpawner pipeSpawner;

    private const string BEST_SCORE_KEY = "BestScore";
     
    private GameState gameState = GameState.Home;

    public GameState GameState => gameState;
    private int currentScore;
    public int CurrentScore
    { get => currentScore;
        set
        { currentScore = value;
        score.text = currentScore.ToString();}
    }
    public int BestScore
    {
        get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
        }
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
            {
            Destroy(gameObject);
            return;
            }
        Instance = this;

    }
    
    private void Start()
    {
        
        gameState = GameState.Home;
        Logo.SetActive(true);
        PlayButton.SetActive(true);

        GameOverPanel.SetActive(false);
        Ready.SetActive(false);
        score.gameObject.SetActive(false);

    }
    public void PlayButtonClick()
    {
        gameState = GameState.GetReady;
        CurrentScore = 0;

        Logo.SetActive(false);
        PlayButton.SetActive(false);

        GameOverPanel.SetActive(false);
        Ready.SetActive(true);
        score.gameObject.SetActive(true);

        ResetGame();
    }

    private void ResetGame()
    {
        playerController.ResetPlayer();
        pipeSpawner.ResetSpawner();
    }

    public void GamePlay()
    {
        gameState = GameState.Playing;
        Ready.SetActive(false);
    }
    public void GameOver()
    {
        gameState = GameState.GameOver;
        score.gameObject.SetActive(false);
        GameOverPanel.SetActive(true);
        PlayButton.SetActive(true);

        GameOverScore.text = CurrentScore.ToString();
        if(CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
        }
        GameOverBestScore.text = BestScore.ToString();
    }

    public void AddScore()
    {
        if(gameState == GameState.Playing)
        {
            CurrentScore++;
        }
    }


}
