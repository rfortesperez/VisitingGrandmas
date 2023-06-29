using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    int hit;
    const int BAT_HITS = 1; // número de disparos para matar los murciélagos
    const int WOLF_HITS = 2; // número de disparos para matar a los lobos
    const int WEREWOLF_HITS = 3;// número de disparos para matar a los hombres lobo
    const float DELAY = 0.25f; // retardo para destruir el gameObject

    [SerializeField] float speed;
    [SerializeField] float distance; // distancia al jugador para iniciar un ataque
    [SerializeField] Transform player; // Referencia al jugador


    Animator anim;



    bool isRight; // flag que nos indica si se mueve hacia la derecha o no

    // Contador de tiempo y tiempo que va a durar el movimiento
    float timeCount;
    [SerializeField] float turnAroundTime;
    [SerializeField] GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeCount = turnAroundTime;
        anim = GetComponent<Animator>();
        if (gameObject.tag == "Bat")
        {
            hit = BAT_HITS;
        }
        else if (gameObject.tag == "Wolf")
        {
            hit = WOLF_HITS;
        }
        else if (gameObject.tag == "Werewolf")
        {
            hit = WEREWOLF_HITS;
        }


    }


    void Update()
    {
        // Movimiento hacia la derecha
        if (isRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Movimiento hacia la izquierda
        if (!isRight)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);

        }
        // Al contador le descontamos Time.deltaTime, y hacemos que gire al llegar a 0
        timeCount -= Time.deltaTime;
        if (timeCount <= 0)
        {
            timeCount = turnAroundTime;
            isRight = !isRight;
        }

        //Lanzamos la animación de ataque
        anim.SetBool("Attack", Mathf.Abs(transform.position.x - player.transform.position.x) < distance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hit--;
        if (hit <= 0)
        {
            DestroyEnemy();

        }

    }

    void DestroyEnemy()
    {
        // Añadimos la puntuación por matar al enemigo
        GameManager.AddScore(gameObject.tag);

        //Instanciamos la explosión
        Instantiate(explosion, transform.position, Quaternion.identity);

        //Destruimos al enemigo
        Destroy(gameObject, DELAY);

    }


}

