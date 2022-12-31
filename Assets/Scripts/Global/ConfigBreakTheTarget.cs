using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigBreakTheTarget : MonoBehaviour
{

    [Header("Data Configuration")]
    public int LevelId = 1;
    public float DeathVerticalLimit = -1000f;
    public static GameObject Character;
    public static int TargetBroken = 0;
    public static int Timer = 400;

    [Header("Sprites Configuration")]

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public Image opt_continue;
    public Image opt_levelSelect;
    public Image opt_backToMenu;
    public int menuPausePosition = 0;
    public bool canMove = true;

    [Header("Dynamic Prefab")]

    public static bool Kill = false;
    public static bool fnc_UpdateTarget = false;
    public static bool fnc_DeadCoroutine = false;
    public static bool fnc_isPaused = false;

    void Awake(){
            
        Movement.haveControl = true;

        //load volume data just in case
        AudioListener.volume = PlayerPrefs.GetFloat("global_volume");

    }

    void Start()
    {
        Character = GameObject.Find("/Character/Sprite");
        DeathVerticalLimit = GameObject.Find("/Globals/DeathBoundaries").transform.position.y;
        //StartCoroutine(TimerDown());

    }

    void Update()
    {

        //Pause Config
        if(!fnc_isPaused && !fnc_DeadCoroutine && Input.GetButtonDown("Start")){
            Time.timeScale = 0f;
            Movement.haveControl = false;
            fnc_isPaused = true;
            pauseMenu.SetActive(true);
            AudioListener.volume = AudioListener.volume / 2;
            menuPausePosition = 0;
        } else if(fnc_isPaused && !fnc_DeadCoroutine && Input.GetButtonDown("Start")){
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            AudioListener.volume = PlayerPrefs.GetFloat("global_volume",1);
            fnc_isPaused = false;
            Movement.haveControl = true;
        }

        if(fnc_isPaused){
            
            if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                menuPausePosition--;
                canMove = false;
            }
            if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                menuPausePosition++;
                canMove = false;
            }

            if(menuPausePosition < 0){ menuPausePosition = 0; }
            if(menuPausePosition > 2){ menuPausePosition = 2; }

            if(Input.GetAxisRaw("Vertical") == 0 && !canMove){
                canMove = true;
            }

            if(menuPausePosition == 0){
                opt_continue.color = new Color(0f,1f,0.78f,1f);
                opt_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 1){
                opt_continue.color = new Color(1f,1f,1f,1f);
                opt_levelSelect.color = new Color(0f,1f,0.78f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 2){
                opt_continue.color = new Color(1f,1f,1f,1f);
                opt_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(0f,1f,0.78f,1f);
            }

            if(Input.GetButtonDown("Start")){
                if(menuPausePosition == 0){
                    Time.timeScale = 1f;
                    pauseMenu.SetActive(false);
                    fnc_isPaused = false;
                    Movement.haveControl = true;
                } else if(menuPausePosition == 1){
                    Debug.Log("TO LEVEL SELECT");
                } else if(menuPausePosition == 2){
                    Time.timeScale = 1f;
                    fnc_isPaused = false;
                    SceneManager.LoadScene("TitleScreen");
                }
            }
        }

        //Kill Config
        if(!fnc_DeadCoroutine && ConfigBreakTheTarget.Kill){
            StartCoroutine(CallDeath());
        }
    }


    void FixedUpdate()
    {

        //Kill player if his Y coordonate are under the limit gameobject
        if(Character.transform.position.y < DeathVerticalLimit && !fnc_DeadCoroutine){
            ConfigBreakTheTarget.Kill = true;
        }

    }


    
    IEnumerator CallDeath(){
        Config.fnc_DeadCoroutine = true;
        Config.Health = 0;
        Movement.haveControl = false;
        Movement.isDead = true;
        Config.fnc_UpdateHealth = true;
        yield return new WaitForSeconds(0.3f);
    }


}
