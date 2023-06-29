using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    public GameObject cuadroPausa;


    void Start()
    {
        Resume();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            Resume();
        }

    }

    public void Pause()
    {
        cuadroPausa.SetActive(true);
        isPaused = true;
        Camera.main.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0;

    }

    public void Resume()
    {
        cuadroPausa.SetActive(false);
        isPaused = false;
        Camera.main.GetComponent<AudioSource>().Play();
        Time.timeScale = 1;

    }


}
