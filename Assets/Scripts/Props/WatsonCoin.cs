using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WatsonCoin : MonoBehaviour
{

    [Range(1,3)] public int coinNumber = 1;
    public Light2D Light;

    void Start(){
        Debug.Log(Config.WatsonCoin_nb1);
        Debug.Log(Config.WatsonCoin_nb2);
        Debug.Log(Config.WatsonCoin_nb3);
        if(Config.WatsonCoin_nb1 == 1 && coinNumber == 1){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
        }
        if(Config.WatsonCoin_nb2 == 1 && coinNumber == 2){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
        }
        if(Config.WatsonCoin_nb3 == 1 && coinNumber == 3){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.2f);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<AudioSource>().Play();
            Config.WatsonCoin++;
            Config.Coin += 5;
            if(coinNumber == 1){ Config.WatsonCoin_nb1 = 1; }
            if(coinNumber == 2){ Config.WatsonCoin_nb2 = 1; }
            if(coinNumber == 3){ Config.WatsonCoin_nb3 = 1; }
            Config.fnc_UpdateWatsonCoin = true;
            Config.fnc_UpdateCoin = true;
            StartCoroutine(DespawnCoin());
        }
    }

    IEnumerator DespawnCoin(){
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<SpriteRenderer>().sprite = null;
        Light = GetComponent<Light2D>();
        Light.intensity = 0.75f;
        while(Light.intensity > 0){
            Light.intensity -= 0.05f;
            yield return new WaitForSeconds(0.01f); 
        }
        transform.position = new Vector2(30000f,30000f);
    }

}
