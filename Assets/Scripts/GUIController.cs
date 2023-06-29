using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    [SerializeField] Text txtScore;
    [SerializeField] Text txtLevel;
    [SerializeField] Text txtMessage;

    [SerializeField] Image[] imgLives;
    // [SerializeField] AudioClip sfxStartGame;
    int level;


    private void OnGUI()
    {
        //Activar los iconos de las vidas que tengamos
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            for (int i = 0; i < imgLives.Length; i++)
            {
                imgLives[i].enabled = i < GameManager.lives - 1;
            }

            //Mostrar la puntuación del jugador

            txtScore.text = string.Format("{0,4:D4}", GameManager.score);

            // Mostrar el nivel
            level = SceneManager.GetActiveScene().buildIndex;
            txtLevel.text = "Level " + level;


            // Si está el juego pausado:
            /*if (GameManager.isPaused())
            {
                txtMessage.text = "PAUSED\nPRESS <P> TO RESUME";
                if (!GameManager.isPaused())
                {
                    txtMessage.text = "";
                }

            }*/

            if (GameManager.isGameOver())
            {
                txtMessage.text = "GAME OVER\nPRESS <RET> TO RESTART";
                if (!GameManager.isGameOver())
                {
                    txtMessage.text = "";
                }
            }



        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Camera.main.GetComponent<AudioSource>().Stop();

            txtMessage.text = "PRESS <RET> TO START";

        }

    }
    private void Update()
    {
        OnGUI();
        
    }



}
