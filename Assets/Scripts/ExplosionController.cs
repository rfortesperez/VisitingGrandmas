 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    const float DELAY= 0.25f; 
    [SerializeField] AudioClip clip;
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        Destroy(gameObject, DELAY);
    }

}
