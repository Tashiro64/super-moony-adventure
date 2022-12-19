using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    void FixedUpdate(){
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<AudioSource>().Play();
            Config.Coin++;
            Config.fnc_UpdateCoin = true;
            transform.position = new Vector2(10000f,10000f);
        }
    }
}
