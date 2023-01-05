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
            (direction == DirectionDropList.Horizontal ? "x" : "y"), transform.position.x + distance,
            "time", timeToComplete,
            "loopType", "pingPong",
            "delay", 0,
            "easeType", iTween.EaseType.easeInOutSine
        ));
    }

}
