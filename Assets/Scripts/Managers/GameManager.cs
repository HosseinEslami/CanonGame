using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static readonly UnityEvent<bool> GameOver = new UnityEvent<bool>();

    public Transform targetPos;
    [HideInInspector] public PoolManager poolManager;
    [HideInInspector] public bool gameOver;
    
    private void OnEnable()
    {
        GameOver.AddListener(GameOverToggle);
    }

    private void OnDisable()
    {
        GameOver.RemoveListener(GameOverToggle);
    }

    private void GameOverToggle(bool isWinner)
    {
        gameOver = true;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        poolManager = GetComponent<PoolManager>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
    
}
