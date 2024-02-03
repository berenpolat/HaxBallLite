using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region General variables 
    
    public int enemyScore = 0;
    public int playerScore = 0;
    public float timer = 0;
    public int budget = 0;
    private bool isBestOfThree;
    private bool isBestOfFive;
    
    #endregion

    #region Texts

    [SerializeField] private Text enScoreText;
    [SerializeField] private Text plScoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text countDownText;
    [SerializeField] private Text budgetText;
    
    #endregion

    #region Gameobjects
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;

    #endregion

    #region Panels

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject marketPanel;

    #endregion
    
    #region Script Instances

    [SerializeField] private EnemyScript es;
    [SerializeField] private PlayerMovements pm;
    [SerializeField] private BallMovements bm;

    #endregion

    #region Unity Methods

    private void Start()
    {
        StopTheGame();
        timer = 0;
    }

    void Update()
    {
        budgetText.text = budget.ToString();
        
        timer += Time.deltaTime;

        enScoreText.text = enemyScore.ToString();
        plScoreText.text = playerScore.ToString();
        timerText.text = Mathf.Floor(timer).ToString();

        if (timer >= 60f)  
        {
            StopTheGame();
            losePanel.SetActive(true);
        }

        if (isBestOfThree)
        {
            //KAZANDI BUDGET VER
            if (playerScore == 3)
            {
                StopTheGame();
                winPanel.SetActive(true);
                budget += 300;
            }

            if (enemyScore == 3)
            {
                StopTheGame();
                losePanel.SetActive(true);
            }
        }
        if (isBestOfFive)
        {//KAZANDI BUDGET VER
            if (playerScore == 5)
            {
                StopTheGame();
                winPanel.SetActive(true);
                budget += 500;
            }

            if (enemyScore == 5)
            {
                StopTheGame();
                losePanel.SetActive(true);
            }
        }
            
    }

    #endregion
    private void StopTheGame()
    {
        mainMenuPanel.SetActive(true);
        enemyScore = 0;
        playerScore = 0;
        enemy.SetActive(false);
        player.SetActive(false);
        ball.SetActive(false);
        timer = 0;
        isBestOfThree = false;  
        isBestOfFive = false;
        enemy.transform.position = es.enemyStartPoint.position;
        player.transform.position = pm.playerStartPoint.position;
        ball.transform.position = bm.ballStartPoint.position;
    }


    public void StartTheBestOfThreeGame()
    {
        mainMenuPanel.SetActive(false);
        StartCountdown();
        timer = 0;
        isBestOfThree = true;
        isBestOfFive = false;
    }

    public void StartTheBestOfFiveGame()
    {
        mainMenuPanel.SetActive(false);
        StartCountdown();
        timer = 0;
        isBestOfThree = false;
        isBestOfFive = true;
    }
    private void StartTheGameplay()
    {
        enemyScore = 0;
        playerScore = 0;
        timer = 0;
        enemy.transform.position = es.enemyStartPoint.position;
        player.transform.position = pm.playerStartPoint.position;
        ball.transform.position = bm.ballStartPoint.position;
        enemy.SetActive(true);
        player.SetActive(true);
        ball.SetActive(true);
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {

        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countDownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        StartTheGameplay();
        countDownText.text = "";
    }

    public void BackToMenuButtonForWinner()
    {
        winPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void BackToMenuButtonForLoser()
    {
        losePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenMarket()
    {
        marketPanel.SetActive(true);
    }

    public void BackFromMarket()
    {
        marketPanel.SetActive(false);
    }
}