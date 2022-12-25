using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    
    [Range(0, 20)] public float spinSpeed = 1f;
    [Range(0, 1)] public int spinDirection = 1;

    void Start()
    {
    }

    void FixedUpdate(){
        
        if(spinDirection == 1){
            transform.Rotate(0, 0, spinSpeed);
        } else {
            transform.Rotate(0, 0, -spinSpeed);
        }
            
    }
}
