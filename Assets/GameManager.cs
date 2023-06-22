using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGameOver;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(1);

        }else if (Input.GetKeyDown(KeyCode.Escape) && isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
