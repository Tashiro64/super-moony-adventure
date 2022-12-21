using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Coin : MonoBehaviour
{

    public Light2D Light;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<AudioSource>().Play();
            Config.Coin++;
            Config.fnc_UpdateCoin = true;
            StartCoroutine(DespawnCoin());
        }
    }

    IEnumerator DespawnCoin(){
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<SpriteRenderer>().sprite = null;
        Light = GetComponent<Light2D>();
        Light.intensity = 1.4f;
        while(Light.intensity > 0){
            Light.intensity -= 0.05f;
            yield return new WaitForSeconds(0.01f); 
        }
        transform.position = new Vector2(30000f,30000f);
    }
}
