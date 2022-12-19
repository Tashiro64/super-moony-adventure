using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleWatsonCoin : MonoBehaviour
{

    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    public bool CanGrabPurpleCoin = false;

    void FixedUpdate(){
        Debug.Log(Config.fnc_GotPurpleCoin);
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            CanGrabPurpleCoin = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if(other.gameObject.tag == "Player" && CanGrabPurpleCoin && !Config.fnc_GotPurpleCoin){

            StartCoroutine(DespawnCoin());
            Config.fnc_GotPurpleCoin = true;

        }

    }

    IEnumerator DespawnCoin(){
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<SpriteRenderer>().sprite = null;
        Light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        while(Light.intensity > 0){
            Light.intensity -= 0.02f;
            yield return new WaitForSeconds(0.02f); 
        }
        yield return null;
    }

}
