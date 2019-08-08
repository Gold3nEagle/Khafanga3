using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText, scoreText;
    [SerializeField] Joystick joystick;
    int gameScore = 0;

    bool aliveChecker;

    float joystickHoriz, speed, joystickVertical;


    private void Awake()
    {
        
        livesText.text = playerLives.ToString();
        scoreText.text = gameScore.ToString();

        int numGameSessions = FindObjectsOfType<GameController>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        } else { 
            DontDestroyOnLoad(gameObject);
        }
    }
     
    public void Score(int score)
    {
        gameScore += score;
        scoreText.text = gameScore.ToString();
    }
 
    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            RemoveLive();
        } else
        {
            ResetGame();
        }
    }

    void RemoveLive()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

        // OR Scene thisScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(thisScene.name);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void Update()
    {
        aliveChecker = FindObjectOfType<PlayerMovement>().isAlive;
        if (!aliveChecker) { return; }
        ProcessJoystickHorizontal();
        ProcessJoystickVertical();
    }

    void ProcessJoystickHorizontal()
    {
      joystickHoriz =  joystick.Horizontal; 
        FindObjectOfType<PlayerMovement>().Run(joystickHoriz);
    }

    void ProcessJoystickVertical()
    {
         joystickVertical = joystick.Vertical;

       // if (joystickVertical < 0f)
        //{
        //    Debug.Log("Crouch!");
        //}  #Can be used for Crouching!

        FindObjectOfType<PlayerMovement>().ClimbLadder(joystickVertical);
    }

}
