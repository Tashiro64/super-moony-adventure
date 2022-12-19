using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatsonCoin : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<AudioSource>().Play();
            Config.WatsonCoin++;
            Config.Coin += 5;
            Config.fnc_UpdateWatsonCoin = true;
            Config.fnc_UpdateCoin = true;
            
            transform.position = new Vector2(10000f,10000f);
        }
    }

}
