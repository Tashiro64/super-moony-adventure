using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" && !Config.fnc_DeadCoroutine){
            Config.Kill = true;
        }
    }
}
