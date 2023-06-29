using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] GameObject hit;
    
    const float DELAY = 0.25f;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && GameManager.GetInstance().GetLives()<=3)
        {
            OneExtraLife();
        }
    }

    void OneExtraLife()
    {
        GameManager.GetInstance().ExtraLife();
        
        Instantiate(hit, transform.position, Quaternion.identity);
        Destroy(gameObject, DELAY);
    }
}
