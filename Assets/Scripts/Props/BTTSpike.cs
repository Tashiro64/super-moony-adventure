using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTSpike : MonoBehaviour
{

    public enum SpikeDirection { PointingDown, PointingUp, PointingLeft, PointingRight };

    [Range(1,50)] public int size = 4;

    public SpikeDirection direction = SpikeDirection.PointingDown;
    public GameObject EfouerringPrefab;

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            Instantiate(EfouerringPrefab, collision.gameObject.transform.position, Quaternion.identity);
        
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(collision.gameObject.GetComponent<Movement>());

            StartCoroutine(WaitBeforeRetry());

        }
    }

    IEnumerator WaitBeforeRetry(){
        Movement.haveControl = false;
        yield return new WaitForSeconds(1f);
        Config.Kill = true;
        ConfigBreakTheTarget.Kill = true;
        
    }

    private void OnDrawGizmos(){

        if(direction == SpikeDirection.PointingDown) { transform.rotation = Quaternion.Euler(0, 0, 0); }
        if(direction == SpikeDirection.PointingUp) { transform.rotation = Quaternion.Euler(0, 0, 180f); }
        if(direction == SpikeDirection.PointingLeft) { transform.rotation = Quaternion.Euler(0, 0, -90f); }
        if(direction == SpikeDirection.PointingRight) { transform.rotation = Quaternion.Euler(0, 0, 90f); }

        GetComponent<SpriteRenderer>().size = new Vector2(size, 1f);
        GetComponent<BoxCollider2D>().size = new Vector2(size, 0.5f);
        GetComponent<BoxCollider2D>().offset = new Vector2(size-(size/2f), 0.25f);

    }
            
}
