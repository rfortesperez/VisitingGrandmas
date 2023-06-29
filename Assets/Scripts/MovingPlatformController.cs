using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] Transform[] pose; // array de posiciones
    [SerializeField] float speed;
    int id; // numeración de las posiciones
    int sum; // para determinar la orientación del desplazamiento

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pose[0].position;        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pose[id].position){
            id += sum;
        }
        if(id == pose.Length-1){
            sum = -1;
        }
        if(id == 0){
            sum = 1;
        }

        transform.position = Vector3.MoveTowards(transform.position, pose[id].position, speed * Time.deltaTime);
        
    }
}
