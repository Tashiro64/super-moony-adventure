using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public GameObject Spawn;
    public bool got = false;
    public bool isSpecialTarget = false;
    
    void OnTriggerEnter2D(Collider2D other) {
        
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "PacaneShuriken") && !got){
            Spawn.SetActive(true);
            ConfigBreakTheTarget.TargetBroken++;
            GameObject.Find("/Canvas/Targets/target"+ConfigBreakTheTarget.TargetBroken).SetActive(false);
            GetComponent<AudioSource>().Play();
            transform.position = new Vector3(1000f,1000f,0);
            got = true;
            if(isSpecialTarget){
                PlayerPrefs.SetInt("gotSpecialTarget", 1);
            }
        }

    }

}
