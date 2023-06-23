using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText, bestText;
    [SerializeField] Text gameOver;
    [SerializeField] Text restartLevel, quitGame;
    [SerializeField] private Image livesImages;
    [SerializeField] private Sprite[] livesSprite;

    GameManager gameManager;

    public int currentScore;
    public int bestScore;

    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameOver.gameObject.SetActive(false);
        restartLevel.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bestScore = PlayerPrefs.GetInt("HighScore", 0); // load best score
        bestText.text = "Best: " + bestScore;
    }

    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        currentScore += 10;
        scoreText.text = "Score: " + currentScore; 
        
    }

    public void CheckForBestScore()
    {
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", bestScore);  // save best score date
            bestText.text = "Best :" + bestScore;
        }
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
