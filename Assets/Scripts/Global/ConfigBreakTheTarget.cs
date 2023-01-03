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
    public AudioClip sfx_ready;
    public AudioClip sfx_go;
    public AudioSource sfx;

    public int Timer_mil = 0;
    public int Timer_sec = 0;
    public int Timer_min = 0;

    public Image timer_mil2;
    public Image timer_mil1;
    public Image timer_sec2;
    public Image timer_sec1;
    public Image timer_min2;
    public Image timer_min1;

    public Sprite nb_0;
    public Sprite nb_1;
    public Sprite nb_2;
    public Sprite nb_3;
    public Sprite nb_4;
    public Sprite nb_5;
    public Sprite nb_6;
    public Sprite nb_7;
    public Sprite nb_8;
    public Sprite nb_9;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public Image opt_continue;
    public Image opt_retry;
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
    public bool fnc_isPaused = false;
    public bool canPause = false;
    public bool runTimer = false;


    void Start()
    {
        TargetBroken = 0;
        fnc_RetryCoroutine = false;
        Movement.haveControl = false;
        deadMenu.SetActive(false);
        menuPausePosition = 0;
        canPause = false;
        Kill = false;
        Time.timeScale = 1f;

        //load volume data just in case
        AudioListener.volume = PlayerPrefs.GetFloat("global_volume");

        Character = GameObject.Find("/Character/Sprite");
        DeathVerticalLimit = GameObject.Find("/Globals/DeathBoundaries").transform.position.y;
        
        StartCoroutine(Ready());

    }

    void Update()
    {

        if(canPause){
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
            if(menuPausePosition > 3){ menuPausePosition = 3; }

            if(Input.GetAxisRaw("Vertical") == 0 && !canMove){
                canMove = true;
            }

            if(menuPausePosition == 0){
                opt_continue.color = new Color(0f,1f,0.78f,1f);
                opt_retry.color = new Color(1f,1f,1f,1f);
                opt_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 1){
                opt_continue.color = new Color(1f,1f,1f,1f);
                opt_retry.color =  new Color(0f,1f,0.78f,1f);
                opt_levelSelect.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 2){
                opt_continue.color = new Color(1f,1f,1f,1f);
                opt_retry.color = new Color(1f,1f,1f,1f);
                opt_levelSelect.color = new Color(0f,1f,0.78f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPausePosition == 3){
                opt_continue.color = new Color(1f,1f,1f,1f);
                opt_retry.color = new Color(1f,1f,1f,1f);
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
                    SceneManager.LoadScene("Scenes/BreakTheTarget/Stage"+LevelId);
                } else if(menuPausePosition == 2){
                    Debug.Log("TO LEVEL SELECT");
                } else if(menuPausePosition == 3){
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
        runTimer = false;
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
            opacity -= 0.008f;  
            if(opacity < 0){ opacity = 0; }
            width -= 10f;  
            ProgressBar.sizeDelta = new Vector2 (width, 13f);
            ReadyWall.color = new Color(0f,0f,0f, opacity);
            if(width == 180f){
                sfx.clip = sfx_go;
                sfx.Play();
            }
            yield return new WaitForSeconds(0.008f);
        }
        canPause = true;
        runTimer = true;
        StartCoroutine(Timer());
        
        ProgressBar.sizeDelta = new Vector2 (0f, 13f);
        Destroy(Ready);
        Destroy(ReadyWall);

        Movement.haveControl = true;
    }

    IEnumerator Timer(){

        while(true && runTimer){
            
            if(Timer_min >= 60){
                Timer_mil = 00;
                Timer_sec = 00;
                Timer_min = 60;
            } else {
                Timer_mil ++;

                if(Timer_mil >= 100){
                    Timer_sec++;
                    Timer_mil = 0;
                }
                if(Timer_sec >= 60){
                    Timer_min++;
                    Timer_sec = 0;
                }
            }

            //milli
            string time = Timer_mil.ToString("00");
            string[] timeArray = new string[time.Length];
            for (int i = 0; i < time.Length; i++) {
                timeArray[i] = time[i].ToString();
                if(timeArray[i] == "9"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_9; }
                if(timeArray[i] == "8"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_8; }
                if(timeArray[i] == "7"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_7; }
                if(timeArray[i] == "6"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_6; }
                if(timeArray[i] == "5"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_5; }
                if(timeArray[i] == "4"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_4; }
                if(timeArray[i] == "3"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_3; }
                if(timeArray[i] == "2"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_2; }
                if(timeArray[i] == "1"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_1; }
                if(timeArray[i] == "0"){ GameObject.Find("/Canvas/Timer/mil"+(i+1)).GetComponent<Image>().sprite = nb_0; }
            }

            //seconds
            time = Timer_sec.ToString("00");
            timeArray = new string[time.Length];
            for (int i = 0; i < time.Length; i++) {
                timeArray[i] = time[i].ToString();
                if(timeArray[i] == "9"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_9; }
                if(timeArray[i] == "8"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_8; }
                if(timeArray[i] == "7"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_7; }
                if(timeArray[i] == "6"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_6; }
                if(timeArray[i] == "5"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_5; }
                if(timeArray[i] == "4"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_4; }
                if(timeArray[i] == "3"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_3; }
                if(timeArray[i] == "2"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_2; }
                if(timeArray[i] == "1"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_1; }
                if(timeArray[i] == "0"){ GameObject.Find("/Canvas/Timer/sec"+(i+1)).GetComponent<Image>().sprite = nb_0; }
            }

            //minutes
            time = Timer_min.ToString("00");
            timeArray = new string[time.Length];
            for (int i = 0; i < time.Length; i++) {
                timeArray[i] = time[i].ToString();
                if(timeArray[i] == "9"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_9; }
                if(timeArray[i] == "8"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_8; }
                if(timeArray[i] == "7"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_7; }
                if(timeArray[i] == "6"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_6; }
                if(timeArray[i] == "5"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_5; }
                if(timeArray[i] == "4"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_4; }
                if(timeArray[i] == "3"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_3; }
                if(timeArray[i] == "2"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_2; }
                if(timeArray[i] == "1"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_1; }
                if(timeArray[i] == "0"){ GameObject.Find("/Canvas/Timer/min"+(i+1)).GetComponent<Image>().sprite = nb_0; }
            }

            yield return new WaitForSeconds(0.01f);

        }

    }


}
