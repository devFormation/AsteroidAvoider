using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOverHandler _gameOverHandler;
    
    public void Crush()
    {
        gameObject.SetActive(false);
        _gameOverHandler.EndGame();
    }
}
