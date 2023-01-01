using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public GameObject Spawn;
    public bool got = false;
    
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("BOU");
        if(other.gameObject.tag == "Player" && !got){
            Spawn.SetActive(true);
            ConfigBreakTheTarget.TargetBroken++;
            GameObject.Find("/Canvas/Targets/target"+ConfigBreakTheTarget.TargetBroken).SetActive(false);
            GetComponent<AudioSource>().Play();
            transform.position = new Vector3(1000f,1000f,0);
            got = true;
        }

    }

}