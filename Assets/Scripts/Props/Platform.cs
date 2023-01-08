using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public enum DirectionDropList { Horizontal, Vertical };
    public DirectionDropList direction;
    [Range(0f,10f)] public float timeToComplete;
    [Range(0f,30f)] public float distance;

    void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            (direction == DirectionDropList.Horizontal ? "x" : "y"), (direction == DirectionDropList.Horizontal ? transform.position.x + distance : transform.position.y + distance),
            "time", timeToComplete,
            "loopType", "pingPong",
            "delay", 0,
            "easeType", iTween.EaseType.easeInOutSine
        ));
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1f,1f,0f,1f);
        Gizmos.DrawLine(
            transform.position,
            (direction == DirectionDropList.Horizontal ?
                new Vector2(
                    transform.position.x + distance,
                    transform.position.y
                ) :
                new Vector2(
                    transform.position.x,
                    transform.position.y + distance
                )
            )
        );
        Gizmos.color = new Color(0f,0f,0f,1f);
        Gizmos.DrawCube(
            new Vector2(
                (direction == DirectionDropList.Horizontal ? transform.position.x + distance : transform.position.x),
                (direction == DirectionDropList.Horizontal ? transform.position.y : transform.position.y + distance)
            ),
            new Vector2(2f,0.5f)
        );
        Gizmos.color = new Color(1f,1f,0f,1f);
        Gizmos.DrawCube(
            new Vector2(
                (direction == DirectionDropList.Horizontal ? transform.position.x + distance : transform.position.x),
                (direction == DirectionDropList.Horizontal ? transform.position.y : transform.position.y + distance)
            ),
            new Vector2(1.86f,0.37f)
        );
    }

}
