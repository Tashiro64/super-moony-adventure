using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDoubleDisplay : MonoBehaviour
{

    public string chosenAction;
    public string chosenActionB;
    private Sprite chosenSprite;
    private Sprite chosenSpriteB;
    private Sprite chosenSpritePressed;
    private Sprite chosenSpriteBPressed;
    public GameObject button;
    public GameObject buttonB;
    public Sprite Action_A;
    public Sprite Action_A_p;
    public Sprite Action_B;
    public Sprite Action_B_p;
    public Sprite Action_X;
    public Sprite Action_X_p;
    public Sprite Action_Y;
    public Sprite Action_Y_p;
    public Sprite Action_RT;
    public Sprite Action_RT_p;
    public Sprite Action_LT;
    public Sprite Action_LT_p;
    public Sprite Action_RB;
    public Sprite Action_RB_p;
    public Sprite Action_LB;
    public Sprite Action_LB_p;
    public Sprite Action_Right;
    public Sprite Action_Right_p;
    public Sprite Action_Left;
    public Sprite Action_Left_p;

    void Start()
    {
        if(chosenAction == "A"){
            chosenSprite = Action_A;
            chosenSpritePressed = Action_A_p;
        }
        if(chosenAction == "B"){
            chosenSprite = Action_B;
            chosenSpritePressed = Action_B_p;
        }
        if(chosenAction == "X"){
            chosenSprite = Action_X;
            chosenSpritePressed = Action_X_p;
        }
        if(chosenAction == "Y"){
            chosenSprite = Action_Y;
            chosenSpritePressed = Action_Y_p;
        }
        if(chosenAction == "RT"){
            chosenSprite = Action_RT;
            chosenSpritePressed = Action_RT_p;
        }
        if(chosenAction == "LT"){
            chosenSprite = Action_LT;
            chosenSpritePressed = Action_LT_p;
        }
        if(chosenAction == "RB"){
            chosenSprite = Action_RB;
            chosenSpritePressed = Action_RB_p;
        }
        if(chosenAction == "LB"){
            chosenSprite = Action_LB;
            chosenSpritePressed = Action_LB_p;
        }
        if(chosenAction == "Right"){
            chosenSprite = Action_Right;
            chosenSpritePressed = Action_Right_p;
        }
        if(chosenAction == "Left"){
            chosenSprite = Action_Left;
            chosenSpritePressed = Action_Left_p;
        }

        if(chosenActionB == "A"){
            chosenSpriteB = Action_A;
            chosenSpriteBPressed = Action_A_p;
        }
        if(chosenActionB == "B"){
            chosenSpriteB = Action_B;
            chosenSpriteBPressed = Action_B_p;
        }
        if(chosenActionB == "X"){
            chosenSpriteB = Action_X;
            chosenSpriteBPressed = Action_X_p;
        }
        if(chosenActionB == "Y"){
            chosenSpriteB = Action_Y;
            chosenSpriteBPressed = Action_Y_p;
        }
        if(chosenActionB == "RT"){
            chosenSpriteB = Action_RT;
            chosenSpriteBPressed = Action_RT_p;
        }
        if(chosenActionB == "LT"){
            chosenSpriteB = Action_LT;
            chosenSpriteBPressed = Action_LT_p;
        }
        if(chosenActionB == "RB"){
            chosenSpriteB = Action_RB;
            chosenSpriteBPressed = Action_RB_p;
        }
        if(chosenActionB == "LB"){
            chosenSpriteB = Action_LB;
            chosenSpriteBPressed = Action_LB_p;
        }
        if(chosenActionB == "Right"){
            chosenSpriteB = Action_Right;
            chosenSpriteBPressed = Action_Right_p;
        }
        if(chosenActionB == "Left"){
            chosenSpriteB = Action_Left;
            chosenSpriteBPressed = Action_Left_p;
        }

        button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        buttonB.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);

        StartCoroutine(ActionAnimation());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            buttonB.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            button.transform.position = new Vector2(Config.Character.transform.position.x - 0.6f, Config.Character.transform.position.y + 1.3f);
            buttonB.transform.position = new Vector2(Config.Character.transform.position.x + 0.6f, Config.Character.transform.position.y + 1.3f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            buttonB.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        }
    }

    private void OnDrawGizmos() {
        
        if(chosenAction == "Right"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_Right; }
        if(chosenAction == "Left"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_Left; }
        if(chosenAction == "A"){ Gizmos.color = new Color(0.2f, 1f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_A; }
        if(chosenAction == "B"){ Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_B; }
        if(chosenAction == "X"){ Gizmos.color = new Color(0.2f, 0.2f, 1f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_X; }
        if(chosenAction == "Y"){ Gizmos.color = new Color(1f, 1f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_Y; } 
        if(chosenAction == "RT"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_RT; }
        if(chosenAction == "LT"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_LT; }
        if(chosenAction == "RB"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_RB; }
        if(chosenAction == "LB"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); button.GetComponent<SpriteRenderer>().sprite = Action_LB; }
        Gizmos.DrawCube(transform.position, new Vector3(2.5f,1.5f,2.5f));

        if(chosenActionB == "Right"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_Right; }
        if(chosenActionB == "Left"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_Left; }
        if(chosenActionB == "A"){ Gizmos.color = new Color(0.2f, 1f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_A; }
        if(chosenActionB == "B"){ Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_B; }
        if(chosenActionB == "X"){ Gizmos.color = new Color(0.2f, 0.2f, 1f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_X; }
        if(chosenActionB == "Y"){ Gizmos.color = new Color(1f, 1f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_Y; } 
        if(chosenActionB == "RT"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_RT; }
        if(chosenActionB == "LT"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_LT; }
        if(chosenActionB == "RB"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_RB; }
        if(chosenActionB == "LB"){ Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.3f); buttonB.GetComponent<SpriteRenderer>().sprite = Action_LB; }
        Gizmos.DrawCube(transform.position, new Vector3(3f,2f,3f));
        

    }

    IEnumerator ActionAnimation(){
        while(true){
            button.GetComponent<SpriteRenderer>().sprite = chosenSprite;
            buttonB.GetComponent<SpriteRenderer>().sprite = chosenSpriteB;
            yield return new WaitForSeconds(0.4f);
            button.GetComponent<SpriteRenderer>().sprite = chosenSpritePressed;
            buttonB.GetComponent<SpriteRenderer>().sprite = chosenSpriteBPressed;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
