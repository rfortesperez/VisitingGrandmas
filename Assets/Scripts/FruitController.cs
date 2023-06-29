using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FruitController : MonoBehaviour
{
    [SerializeField] GameObject hit;
    [SerializeField] AudioClip clip;
    const float DELAY = 0.25f;
    
    
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyFruit();

        }
    }

    void DestroyFruit()
    {

        //Añadimos la puntuación 
        GameManager.AddScore(gameObject.tag);

        //Reproducimos el sonido de recogida de la fruta
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        //Instanciamos la animación de destrucción de la fruta
        Instantiate(hit, transform.position, Quaternion.identity);

        //Destruimos la fruta
        Destroy(gameObject, DELAY);
        


    }
}
