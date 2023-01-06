using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDetection : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Prefab" || col.gameObject.tag == "Enemy"){
            col.gameObject.transform.parent.SetParent(gameObject.transform,true);
        } else {
            col.gameObject.transform.SetParent(gameObject.transform,true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Prefab" || col.gameObject.tag == "Enemy"){
            col.gameObject.transform.parent.SetParent(GameObject.Find("/MapObject").transform);
        } else if(col.gameObject.tag == "Player"){
            col.gameObject.transform.SetParent(GameObject.Find("/Character").transform);
        } else {
            col.gameObject.transform.parent = null;
        }
    }

}