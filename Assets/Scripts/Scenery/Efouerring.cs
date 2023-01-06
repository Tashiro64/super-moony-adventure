using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efouerring : MonoBehaviour
{

    public GameObject EfouerringPrefab;
    public bool efouerred = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Movement.isRolling && GroundCheck.isGrounded && !efouerred && collision.gameObject.name == "EfouerringCheck"){
            efouerred = true;
            Instantiate(EfouerringPrefab, collision.gameObject.transform.position, Quaternion.identity);
        
            collision.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            Destroy(collision.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>());
            Destroy(collision.gameObject.transform.parent.gameObject.GetComponent<Movement>());

            StartCoroutine(WaitBeforeRetry());

        }
    }

    IEnumerator WaitBeforeRetry(){
        
        yield return new WaitForSeconds(1f);
        ConfigBreakTheTarget.Kill = true;
        
    }
}
