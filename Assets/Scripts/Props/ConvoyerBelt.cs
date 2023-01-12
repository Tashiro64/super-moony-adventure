using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{

    public enum ConvoyerDirection { Right, Left };
    public enum Part { Left, Center, Right };

    public ConvoyerDirection direction;
    public Part part;
    [Range(1,30)] public int size = 4;
    [Range(0f,0.5f)] public float delay = 0.0335f;
    [Range(0f,500f)] public float force = 90f;
    public GameObject clone;

    public Sprite sprite_convoyer_1;
    public Sprite sprite_convoyer_2;
    public Sprite sprite_convoyer_3;
    public Sprite sprite_convoyer_4;
    public Sprite sprite_convoyer_5;
    public Sprite sprite_convoyer_6;
    public Sprite sprite_convoyer_7;
    public Sprite sprite_convoyer_8;

    void Start()
    {
        StartCoroutine(Roll());
    }

    IEnumerator Roll(){

        SpriteRenderer spritePart = this.gameObject.GetComponent<SpriteRenderer>();

        while(true){
            if(direction == ConvoyerDirection.Left){
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_1;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_2;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_3;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_4;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_5;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_6;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_7;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_8;
            } else {
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_8;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_7;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_6;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_5;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_4;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_3;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_2;
                yield return new WaitForSeconds(delay);
                spritePart.sprite = sprite_convoyer_1;
            }
        }
    }

    private void OnDrawGizmos(){
        if(part == Part.Center){
            for(int i = 1; i < size; i++){
                GetComponent<SpriteRenderer>().size = new Vector2(size, 1);
                GetComponent<BoxCollider2D>().size = new Vector2(size, 1);
                GetComponent<BoxCollider2D>().offset = new Vector2(size-(size/2f), 0);
            }

            this.gameObject.transform.parent.GetChild(2).transform.position = new Vector2(
                this.gameObject.transform.position.x + (size+0.5f),
                this.gameObject.transform.position.y
            );

            this.gameObject.transform.parent.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = (direction == ConvoyerDirection.Right ? force : -force);
            this.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(size+2f, 1f);
            this.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f+(size-(size/2f)), 0.1f);
        }

        //2 = 0.5
        //3 = 1f

    }
}
