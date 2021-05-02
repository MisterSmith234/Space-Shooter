using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.RestartLevel();
    }

    private void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
