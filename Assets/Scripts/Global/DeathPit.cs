using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" && !Movement.isDead){
            Movement.haveControl = false;
            Config.Health = 0;
            Config.fnc_UpdateHealth = true; 
            Movement.isDead = true;
        }
    }
}
