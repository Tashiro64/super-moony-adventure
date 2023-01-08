using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Sprite frame_1;
    public Sprite frame_2;
    public Sprite frame_3;
    public Sprite frame_4;
    public Sprite frame_5;
    public Sprite frame_6;
    public Sprite frame_7;
    public Sprite frame_8;
    public GameObject Smoke;
    public AudioSource audiosource;
    public AudioClip sfx_shoot;
    public AudioClip sfx_wallHit;
    public int wait = 0;
    public bool brokenInWall = false;

    void Start()
    {
        StartCoroutine(Pew());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Tilemap"){
                
            transform.GetChild(0).gameObject.SetActive(true);
            audiosource.clip = sfx_wallHit;
            audiosource.Play();
            brokenInWall = true;

        }
    }

    private IEnumerator Pew(){;
        audiosource.clip = sfx_shoot;
        audiosource.Play();
        if(Movement.facingDirection == 1){
            while(wait < 155 && !brokenInWall){
                transform.position = new Vector2(transform.position.x + 0.05f, transform.position.y);
                transform.Rotate(0, 0, -3f);
                yield return new WaitForSeconds(0.002f);
                wait++;
            }
        } else {
            while(wait < 155 && !brokenInWall){
                transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
                transform.Rotate(0, 0, 3f);
                yield return new WaitForSeconds(0.002f);
                wait++;
            }
        }

        Smoke.SetActive(true);

        Destroy(GetComponent<BoxCollider2D>());

        if(!brokenInWall){
            GetComponent<SpriteRenderer>().sprite = frame_2;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_3;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_4;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_5;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_6;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_7;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = frame_8;
            yield return new WaitForSeconds(0.045f);
            GetComponent<SpriteRenderer>().sprite = null;

        } else {
             GetComponent<SpriteRenderer>().sprite = null;
        }
        
        yield return new WaitForSeconds(2f);
        Destroy(transform.gameObject);
    }
}
