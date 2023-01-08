using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PurpleWatsonCoin : MonoBehaviour
{

    public Light2D Light;
    public bool CanGrabPurpleCoin = false;
    private Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(CanGrabPurpleCoin && !Config.fnc_GotPurpleCoin){
            //convoyer belt fix
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            CanGrabPurpleCoin = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if(other.gameObject.tag == "Player" && CanGrabPurpleCoin && !Config.fnc_GotPurpleCoin && Config.Health > 0){

            GameObject.Find("/Canvas/WatsonCoin/PurpleWatsonCoin").SetActive(true);
            StartCoroutine(DespawnCoin());
            Config.fnc_GotPurpleCoin = true;

        }

    }

    IEnumerator DespawnCoin(){
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<SpriteRenderer>().sprite = null;
        Light = GetComponent<Light2D>();
        Light.intensity = 1.2f;
        while(Light.intensity > 0){
            Light.intensity -= 0.05f;
            Light.pointLightOuterRadius += 0.05f;
            yield return new WaitForSeconds(0.02f); 
        }
        transform.position = new Vector2(30000f,30000f);
    }

}
