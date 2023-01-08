using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject projectile_bullet;
    public GameObject projectile_burgers;

    void Update()
    {

        if(Movement.haveControl && !Config.fnc_isPaused && !ConfigBreakTheTarget.fnc_isPaused && !Movement.isRolling){
            if(Input.GetButtonDown("X")){
                Instantiate(projectile_bullet, transform.position, Quaternion.identity);
            }
        }

    }

}