using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDisplay : MonoBehaviour
{

    public string chosenAction;
    private Sprite chosenSprite;
    private Sprite chosenSpritePressed;
    public GameObject button;
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

        button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);

        StartCoroutine(ActionAnimation());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            button.transform.position = new Vector2(Config.Character.transform.position.x, Config.Character.transform.position.y + 1.3f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            button.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        }
    }

    private void OnDrawGizmos() {
        
        if(chosenAction == "A"){ Gizmos.color = new Color(0.2f, 1f, 0.2f, 0.5f); button.GetComponent<SpriteRenderer>().sprite = Action_A; }
        if(chosenAction == "B"){ Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.5f); button.GetComponent<SpriteRenderer>().sprite = Action_B; }
        if(chosenAction == "X"){ Gizmos.color = new Color(0.2f, 0.2f, 1f, 0.5f); button.GetComponent<SpriteRenderer>().sprite = Action_X; }
        if(chosenAction == "Y"){ Gizmos.color = new Color(1f, 1f, 0.2f, 0.5f); button.GetComponent<SpriteRenderer>().sprite = Action_Y; } 
        if(chosenAction == "RT"){ Gizmos.color = Color.gray; button.GetComponent<SpriteRenderer>().sprite = Action_RT; }
        if(chosenAction == "LT"){ Gizmos.color = Color.gray; button.GetComponent<SpriteRenderer>().sprite = Action_LT; }
        if(chosenAction == "RB"){ Gizmos.color = Color.gray; button.GetComponent<SpriteRenderer>().sprite = Action_RB; }
        if(chosenAction == "LB"){ Gizmos.color = Color.gray; button.GetComponent<SpriteRenderer>().sprite = Action_LB; }
        
        Gizmos.DrawCube(transform.position, new Vector3(3f,2f,3f));

    }

    IEnumerator ActionAnimation(){
        while(true){
            button.GetComponent<SpriteRenderer>().sprite = chosenSprite;
            yield return new WaitForSeconds(0.4f);
            button.GetComponent<SpriteRenderer>().sprite = chosenSpritePressed;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
