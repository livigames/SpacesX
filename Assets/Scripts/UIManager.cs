using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text gameOver;
    [SerializeField] Text restartLevel, quitGame;
    [SerializeField] private Image livesImages;
    [SerializeField] private Sprite[] livesSprite;

    GameManager gameManager;

    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameOver.gameObject.SetActive(false);
        restartLevel.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString(); 
    }

    public void UpdateLives(int currentLives)
    {
        
        livesImages.sprite = livesSprite[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        restartLevel.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
        gameManager.GameOver();
        StartCoroutine(FlickerGameOver());
    }

    private IEnumerator FlickerGameOver()
    {
        while(true)
        {
            gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
