using System.Net.Mime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    const int LIVES = 3;
    const int SCORE_FRUIT = 15;
    const int SCORE_BAT = 25;
    const int SCORE_WOLF = 50;
    const int SCORE_WEREWOLF = 100;
    static GameManager instance;
    static bool paused;


    [SerializeField] AudioClip sfxExtraLife;
    public static int score { get; private set; } = 0;
    public static int lives { get; private set; } = LIVES;
    public static bool extra { get; private set; }
    public static bool gameOver { get; private set; }
    //public static bool paused { get; private set; }
    public static List<int> totalFruits = new List<int> { 0, 17, 17, 17 };





    public static bool isGameOver()
    {
        return gameOver;
    }

    /*public static bool isPaused()
    {
        return paused;
    }*/

    public int GetLives()
    {
        return lives;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    
    public static void AddScore(string tag)
    {
        int pts = 0; //puntos
        switch (tag)
        {
            case "Bat":
                pts = SCORE_BAT;
                break;
            case "Wolf":
                pts = SCORE_WOLF;
                break;
            case "Werewolf":
                pts = SCORE_WEREWOLF;
                break;
            case "fruit":
                pts = SCORE_FRUIT;
                break;

        }


        score += pts;

    }


    public void ExtraLife()
    {
        if (lives <= 3 && extra == false)
        {
            extra = true;
            ++lives;
            AudioSource.PlayClipAtPoint(sfxExtraLife, Camera.main.transform.position, 0.75f);
        }

    }
    public static void LoseLife()
    {
        lives--;
        extra = false;

        if (lives == 0)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        gameOver = true;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        else if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.Return))
        {   
            gameOver = false;
            score = 0;
            lives = LIVES;
            SceneManager.LoadScene(1);

        }
        else if (isGameOver())
        {

            beginGame();
        }

       
    }



    public static void beginGame()
    {

        if (isGameOver() && Input.GetKeyDown(KeyCode.Return))
        {
            gameOver = false;
            score = 0;
            lives = LIVES;
            SceneManager.LoadScene(0);

        }
    }

    

}




