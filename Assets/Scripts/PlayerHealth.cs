using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public void Crush()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
