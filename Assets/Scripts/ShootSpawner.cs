using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpawner : MonoBehaviour
{
    [SerializeField] Transform Spawner; //Empty object desde dónde salen los disparos
    [SerializeField] GameObject Projectile; // la bala, que es el prefab shoot
    [SerializeField] Animator anim;
    GameManager game;

    void Start(){
        anim = GetComponent<Animator>();
        //game = GameManager.GetInstance();
    }

   

    void Update()
    {
        // Si se pulsa ctl izq ó el botón izq del ratón, dispara
        if(!PausaManager.isPaused && Input.GetButtonDown("Fire1"))
       //if(Input.GetButtonDown("Fire1"))
        {
            Shooting();
            anim.SetTrigger("isAttacking");
        }
    }


    void Shooting() // Generamos un disparo
    {
        Instantiate(Projectile, Spawner.position, Spawner.rotation);
    }




}