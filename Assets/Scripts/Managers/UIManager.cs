using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Sprite winSprite, loseSprite;
    private Image _statusImage;

    private void OnEnable()
    {
        GameManager.GameOver.AddListener(GameOver);
    }

    private void OnDisable()
    {
        GameManager.GameOver.RemoveListener(GameOver);
    }

    private void Awake()
    {
        _statusImage = gameOverPanel.GetComponentsInChildren<Image>()[1];
    }

    private void GameOver(bool isWinner)
    {
        gameOverPanel.SetActive(true);
        _statusImage.sprite = isWinner ? winSprite : loseSprite;
    }
}
