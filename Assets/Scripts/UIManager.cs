using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private int _score = 0;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = string.Format("Score: {0}", _score);
        _gameOverText.gameObject.SetActive(false);
        this._gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(this._gameManager == null)
        {
            Debug.LogError("UIManager: -> GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator GameOverFlickerRoutine()
    {
        while (this._gameManager.IsGameOver())
        {
            yield return new WaitForSeconds(0.5f);
            if (_gameOverText.gameObject.activeSelf)
            {
                _gameOverText.gameObject.SetActive(false);
            }
            else if (!_gameOverText.gameObject.activeSelf)
            {
                _gameOverText.gameObject.SetActive(true);
            }
        }
        
    }
    public void UpdateScore(int score)
    {
        this._score = score;
        _scoreText.text = string.Format("Score: {0}", _score);
    }

    public void UpdateLives(int currentLives)
    {
        this._livesImg.sprite = _liveSprites[currentLives];
    }

    public void OnPlayerDeath()
    {
        this._gameManager.GameOver();
        _restartText.gameObject.SetActive(this._gameManager.IsGameOver());
        StartCoroutine(this.GameOverFlickerRoutine());
    }

    

}
