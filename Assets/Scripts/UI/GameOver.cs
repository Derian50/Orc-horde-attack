using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _gameOver;

    private void OnEnable()
    {
        EventBus.onGameOver += ShowGameOver;
    }
    private void OnDisable()
    {
        EventBus.onGameOver -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        _gameOver.SetActive(true);
    }
    public void ReastartLevel()
    {
        YaSDK.ShowFullscreenAdv();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
