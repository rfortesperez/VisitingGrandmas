using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    static bool paused;

    public static bool isPaused(){
            return paused;
        }
    

    void Update()
    {
        if (!GameManager.isGameOver() && Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

     void PauseGame()
        {
            paused = true;
            Camera.main.GetComponent<AudioSource>().Stop();

            Time.timeScale = 0;

        }

         void ResumeGame()
        {
            paused = false;
            Camera.main.GetComponent<AudioSource>().Play();

            Time.timeScale = 1;
        }
        
}
