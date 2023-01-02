using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public static bool isGrounded = false;

    void OnTriggerStay2D (Collider2D obj){

        if (obj.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
            Movement.alreadyJumpedOnce = false;
            Movement.canJump = true;
        }
    }

    void OnTriggerExit2D (Collider2D obj){
        if(!Movement.isRolling){
            if(obj.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                isGrounded = false;
                Movement.canJump = false;
            }
        }
    }

}
