using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region General variables 
    
    public int enemyScore = 0;
    public int playerScore = 0;
    public float timer = 0;

    #endregion

    #region Texts

    [SerializeField] private Text enScoreText;
    [SerializeField] private Text plScoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text countDownText;

    #endregion

    #region Gameobjects

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;

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
    }

    void Update()
    {
        timer += Time.deltaTime;

            enScoreText.text = enemyScore.ToString();
            plScoreText.text = playerScore.ToString();
            timerText.text = Mathf.Floor(timer).ToString();

            if (timer >= 60f)  
            {
                StopTheGame();
            }
    }

    #endregion
    private void StopTheGame()
    {
        mainMenu.SetActive(true);
        enemyScore = 0;
        playerScore = 0;
        enemy.SetActive(false);
        player.SetActive(false);
        ball.SetActive(false);
        timer = 0;
    }

    public void StartTheGame()
    {
        mainMenu.SetActive(false);
        StartCountdown();
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
   
}