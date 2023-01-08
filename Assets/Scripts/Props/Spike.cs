using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    public enum SpikeDirection { PointingDown, PointingUp, PointingLeft, PointingRight };

    [Range(1,50)] public int size = 4;
    public SpikeDirection direction = SpikeDirection.PointingDown;

    void OnTriggerEnter2D(Collider2D coll){

        if(!Config.fnc_GetDamaged && coll.gameObject.tag == "Player"){
            Config.GetDamaged = true;
        }

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
