using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigBreakTheTarget : MonoBehaviour
{

    [Header("Data Configuration")]
    public int LevelId = 0;
    public float DeathVerticalLimit = -1000f;
    public static GameObject Character;
    public static int TargetBroken = 0;
    public static int Timer = 400;
    public AudioClip sfx_ready;
    public AudioClip sfx_go;
    public AudioSource sfx;

    public int Timer_mil = 0;
    public int Timer_sec = 0;
    public int Timer_min = 0;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public Image opt_continue;
    public Image opt_levelSelect;
    public Image opt_backToMenu;
    
    public GameObject deadMenu;
    public Image opt_dm_retry;
    public Image opt_dm_levelSelect;
    public Image opt_dm_backToMenu;
    
    public int menuPausePosition = 0;
    public bool canMove = true;

    [Header("Dynamic Prefab")]

    public static bool Kill = false;
    public static bool fnc_UpdateTarget = false;
    public static bool fnc_RetryCoroutine = false;
    public static bool fnc_isPaused = false;


    void Start()
    {
        TargetBroken = 0;
        fnc_RetryCoroutine = false;
        Movement.haveControl = false;
        deadMenu.SetActive(false);
        menuPausePosition = 0;

        Debug.Log(fnc_RetryCoroutine);

        //load volume data just in case
        AudioListener.volume = PlayerPrefs.GetFloat("global_volume");

        Character = GameObject.Find("/Character/Sprite");
        DeathVerticalLimit = GameObject.Find("/Globals/DeathBoundaries").transform.position.y;
        
        StartCoroutine(Ready());

    }

    void Update()
    {

        //Pause Config
        if(!fnc_isPaused && !fnc_RetryCoroutine && Input.GetButtonDown("Start")){
            Time.timeScale = 0f;
            Movement.haveControl = false;
            fnc_isPaused = true;
            pauseMenu.SetActive(true);
            AudioListener.volume = AudioListener.volume / 2;
            menuPausePosition = 0;
            canMove = false;
        } else if(fnc_isPaused && !fnc_RetryCoroutine && Input.GetButtonDown("Start")){
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            AudioListener.volume = PlayerPrefs.GetFloat("global_volume",1);
            fnc_isPaused = false;
            canMove = false;
            StartCoroutine(DelayFrameHaveControl());
        }

        if(fnc_RetryCoroutine){
            
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
                opt_dm_retry.color = new Color(0f,1f,0.78f,1f);
                opt_dm_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_dm_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 1){
                opt_dm_retry.color = new Color(1f,1f,1f,1f);
                opt_dm_levelSelect.color = new Color(0f,1f,0.78f,1f);
                opt_dm_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 2){
                opt_dm_retry.color = new Color(1f,1f,1f,1f);
                opt_dm_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_dm_backToMenu.color = new Color(0f,1f,0.78f,1f);
            }
            
            if(Input.GetButtonDown("Submit")){
                if(menuPausePosition == 0){
                    SceneManager.LoadScene("Scenes/BreakTheTarget/Stage"+LevelId);
                } else if(menuPausePosition == 1){
                    Debug.Log("TO LEVEL SELECT");
                } else if(menuPausePosition == 2){
                    Time.timeScale = 1f;
                    fnc_isPaused = false;
                    SceneManager.LoadScene("Scenes/TitleScreen");
                }
            }
        } else if(fnc_isPaused){
            
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
            
            if(Input.GetButtonDown("Submit")){
                if(menuPausePosition == 0){
                    Time.timeScale = 1f;
                    pauseMenu.SetActive(false);
                    AudioListener.volume = PlayerPrefs.GetFloat("global_volume",1);
                    fnc_isPaused = false;
                    Movement.haveControl = true;
                } else if(menuPausePosition == 1){
                    Debug.Log("TO LEVEL SELECT");
                } else if(menuPausePosition == 2){
                    Time.timeScale = 1f;
                    fnc_isPaused = false;
                    SceneManager.LoadScene("Scenes/TitleScreen");
                }
            }
            
        }

        //Kill Config
        if(!fnc_RetryCoroutine && ConfigBreakTheTarget.Kill){
            StartCoroutine(CallDeath());
        }
    }


    void FixedUpdate()
    {

        //Kill player if his Y coordonate are under the limit gameobject
        if(Character.transform.position.y < DeathVerticalLimit && !fnc_RetryCoroutine){
            ConfigBreakTheTarget.Kill = true;
        }

    }

    IEnumerator DelayFrameHaveControl(){
        yield return new WaitForSeconds(0.1f);
        Movement.haveControl = true;
    }
    
    IEnumerator CallDeath(){
        fnc_RetryCoroutine = true;
        AudioListener.volume = AudioListener.volume / 2;
        Kill = false;
        deadMenu.SetActive(true);
        menuPausePosition = 0;
        Movement.haveControl = false;
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Ready(){
        
        Image ReadyWall = GameObject.Find("/Canvas/ReadyWall").GetComponent<Image>();
        Image Ready = GameObject.Find("/Canvas/Ready").GetComponent<Image>();
        RectTransform ProgressBar = GameObject.Find("/Canvas/ProgressBar").GetComponent<RectTransform>();
        float opacity = 1;
        float width = 1300f;
        ProgressBar.sizeDelta = new Vector2 (width, 13f);

        sfx.clip = sfx_ready;
        sfx.Play();
        
        while(width > 0){
            opacity -= 0.01f;  
            if(opacity < 0){ opacity = 0; }
            width -= 10f;  
            ProgressBar.sizeDelta = new Vector2 (width, 13f);
            ReadyWall.color = new Color(0f,0f,0f, opacity);
            yield return new WaitForSeconds(0.008f);
        }
        StartCoroutine(Timer());
        sfx.clip = sfx_go;
        sfx.Play();
        
        ProgressBar.sizeDelta = new Vector2 (0f, 13f);
        Destroy(Ready);
        Destroy(ReadyWall);

        Movement.haveControl = true;
    }

    IEnumerator Timer(){
        Debug.Log()
        yield return new WaitForSeconds(0.001f);
    }


}
