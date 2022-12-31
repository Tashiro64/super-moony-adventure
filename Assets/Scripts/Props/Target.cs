using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player"){

        }

    }

}
