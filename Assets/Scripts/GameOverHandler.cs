using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverDisplay;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private ScoreSystem _scoreSystem;

    public void EndGame()
    {
        _asteroidSpawner.enabled = false;

        int _finalScore = _scoreSystem.EndTimer();

        _gameOverText.text = $"Your score is: {_finalScore}";
        
        _gameOverDisplay.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        //SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
