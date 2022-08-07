using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Inspector Settings
    [SerializeField]
    private Text _scoreText;

    [SerializeField] 
    private Image _lifeImage;
    
    [SerializeField] 
    private Sprite[] _lifeSprites;

    [SerializeField]
    private Text _gameOverText;
    
    [SerializeField]
    private Text _restartText;
    #endregion

    private GameManager _gameManager;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL!!");
        }
    }

    /// <summary>
    /// Update the score text
    /// </summary>
    /// <param name="playerScore">Score to display</param>
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    /// <summary>
    /// Update the image component to display life image
    /// </summary>
    /// <param name="currentLife">Current life</param>
    public void UpdateLifeImage(int currentLife)
    {
        _lifeImage.sprite = _lifeSprites[currentLife];

        if (currentLife == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    /// <summary>
    /// Display Game Over text
    /// </summary>
    /// <returns>Yield return</returns>
    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
