using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float temp;
    [SerializeField] float damage;
    [SerializeField] GameObject hitExplosion; 
   


    void Update()
    {

        // Actualizar el temmporizador
        temp -= Time.deltaTime;
        if (temp < 0)
        {
            Destroy(gameObject);
        }

        // Actualizar la posición
        transform.Translate(Vector3.down * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Instanciamos la explosión
        Instantiate(hitExplosion, transform.position, Quaternion.identity);

        //Destrucción del disparo 
        Destroy(gameObject);
    }




}
