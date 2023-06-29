using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{


    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float duration;
    [SerializeField] int blinkNum;
    //[SerializeField] GameObject fruit;
    Vector3 initialPosition;
    Rigidbody2D rb;
    Animator anim;
    float moveX;
    Collider2D col;
    
    bool jump;
    bool active;
    const float DELAY = 0.25f;
    int sceneId;
    int fruitCount;


    bool checkCollision
    { // Para detectar si el Raycast está colisionando con algo
        get
        {
            hit = Physics2D.Raycast(transform.position + ray, transform.up * -1, distancePlat, layer);
            return hit.collider != null;
        }
    }

    RaycastHit2D hit; // para detectar la plataforma en movimiento
    [SerializeField] Vector3 ray; // para posicionar mejor el Raycast
    [SerializeField] LayerMask layer; // Para que el Raycast detecte el layer de la plataforma en movimiento
    [SerializeField] float distancePlat; // Distancia de la plataforma en movimiento
    [SerializeField] GameObject death;

    
    [SerializeField] AudioClip sfxGameOver;
    [SerializeField] AudioClip sfxNextLevel;
    [SerializeField] GameObject fade;
    private Animator transitionAnimator;
    [SerializeField] float transitionTime;
    
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        StartCoroutine("StartPlayer");
        active = true;
        sceneId = SceneManager.GetActiveScene().buildIndex;
        transitionAnimator = fade.GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        if (!jump && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        PlatformDetector();


    }

    void FixedUpdate()
    {

        Run();
        Flip();
        Jump();

    }

    void Run()
    {
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = vel;
        anim.SetBool("isRunning", Mathf.Abs(rb.velocity.x) > Mathf.Epsilon);

    }
    void Flip()
    {
        float vx = rb.velocity.x;
        if (Mathf.Abs(vx) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(vx), 1);
        }
    }

    void Jump()
    {
        if (!jump)
        {
            return;
        }
        jump = false;
        if (!col.IsTouchingLayers(LayerMask.GetMask("Terrain", "Platforms", "FlyingPlatforms")))
        {
            return;
        }
        rb.velocity += new Vector2(0, jumpSpeed);
        anim.SetTrigger("isJumping");

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bat" || other.gameObject.tag == "Wolf" || other.gameObject.tag == "Werewolf")
        {

            PlayerDeath();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "tramp")
        {

            PlayerDeath();
        }
        if(other.gameObject.tag == "fruit"){
            ++fruitCount;
            if(fruitCount == GameManager.totalFruits[sceneId]){
                AudioSource.PlayClipAtPoint(sfxNextLevel, Camera.main.transform.position, 1f);
                NextScene();              
            }
        }
        

    }

    void NextScene(){
        int nextId = sceneId+1;
        if(nextId == 4){

            nextId = 0;
        }
        


        StartCoroutine (SceneLoad(nextId));

    }

    public IEnumerator SceneLoad(int nextId){
        // Disparar el Trigger para reproducir la animacion FadeIn

        transitionAnimator.SetTrigger("StartFade");
        // Esperar un segundo
        yield return new WaitForSeconds(transitionTime);
        // Cargar la escena
        SceneManager.LoadScene(nextId);

    }


    void PlatformDetector()
    {
        if (checkCollision)
        { //Si se detecta colisión con la plataforma en movimiento, hacemos que player sea hijo de la plataforma
            transform.parent = hit.collider.transform; // Su posición pasará a ser la de la plataforma en movimiento
        }
        else
        {// Si deja de tocar la plataforma en movimiento, deja de ser hijo de la misma
            transform.parent = null;
        }
    }

    // Método para dibujar una pequeña marca en el jugador (no visible durante la partida) para situarlo sobre la plataforma
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + ray, transform.up * -1 * distancePlat);
    }

    IEnumerator StartPlayer()
    {
        
        yield return new WaitForSeconds(DELAY);



        transform.position = initialPosition;
        // Referencia al Material
        Material mat = GetComponent<SpriteRenderer>().material;
        Color color = mat.color;

        float t = 0;
        float t2 = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            // parpadeo

            t2 += Time.deltaTime;

            float newAlpha = blinkNum * (t2 / duration);
            if (newAlpha > 1)
            {
                t2 = 0;
            }
            color.a = newAlpha;
            mat.color = color;

            yield return null;
        }
        // Reseteo canal alpha
        color.a = 1;
        mat.color = color;

        active = true;
        //PlayerActive();
    }

    void PlayerDeath()
    {
        
        active = false;
       //PlayerActive();

        //Instanciamos la animación de la muerte
        Instantiate(death, transform.position, Quaternion.identity);
        

        //Actualizamos las vidas
        
        GameManager.LoseLife(); //Se pierde una vida

        if (!GameManager.isGameOver()) // Si gameOver es falso, comenzamos de nuevo
        {
            active = true;
           // PlayerActive();
           
            StartCoroutine("StartPlayer");
            
        }else{
             AudioSource.PlayClipAtPoint(sfxGameOver, Camera.main.transform.position, 1f);
            gameObject.SetActive(active);
        }        

    }   

    

}


