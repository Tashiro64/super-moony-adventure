using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Config : MonoBehaviour
{

    [Header("Data Configuration")]
    public int LevelId = 0;
    public float DeathVerticalLimit = -1000f;
    public static GameObject Character;
    public static int Health = 3;
    public static int WatsonCoin = 0;
    public static int WatsonCoin_nb1 = 0;
    public static int WatsonCoin_nb2 = 0;
    public static int WatsonCoin_nb3 = 0;
    public static int PurpleWatsonCoin = 0;
    public static int Coin = 0;
    public static int Timer = 400;

    [Header("Sprites Configuration")]
    public Sprite life_3;
    public Sprite life_2;
    public Sprite life_1;
    public Sprite life_0;

    public Sprite number_0;
    public Sprite number_1;
    public Sprite number_2;
    public Sprite number_3;
    public Sprite number_4;
    public Sprite number_5;
    public Sprite number_6;
    public Sprite number_7;
    public Sprite number_8;
    public Sprite number_9;

    [Header("Pause/Dead Menu")]
    public GameObject pauseMenu;
    public Image opt_continue;
    public Image opt_levelSelect;
    public Image opt_backToMenu;
    public int menuPausePosition = 0;
    public bool canMove = true;
    public GameObject deadMenu;
    public Image opt_dm_retry;
    public Image opt_dm_levelSelect;
    public Image opt_dm_backToMenu;

    [Header("Dynamic Prefab")]
    public GameObject prefab_PurpleWatsonCoin;
    public GameObject emptyGameObject;

    public static bool Kill = false;
    public static bool GetDamaged = false;

    public static bool fnc_UpdateHealth = false;
    public static bool fnc_UpdateWatsonCoin = false;
    public static bool fnc_UpdateCoin = false;
    public static bool fnc_GotPurpleCoin = false;
    public static bool fnc_GotPurpleCoinSpawn = false;
    public static bool fnc_DeadCoroutine = false;
    public static bool fnc_isPaused = false;
    public static bool fnc_GetDamaged = false;
    public bool canPause = false;

    void Awake(){
              
        //load currently obtained Purple / Watson Coins
        if(PlayerPrefs.GetInt("stage" + LevelId + "_pwc") == 1) { PurpleWatsonCoin = 1; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc1") == 1) { WatsonCoin_nb1 = 1; WatsonCoin++; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc2") == 1) { WatsonCoin_nb2 = 1; WatsonCoin++; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc3") == 1) { WatsonCoin_nb3 = 1; WatsonCoin++; }
        Config.fnc_UpdateWatsonCoin = true;
        Movement.haveControl = true;
        Health = 3;
        Config.fnc_DeadCoroutine = false;
        ConfigBreakTheTarget.fnc_RetryCoroutine = false;
        menuPausePosition = 0;
        canPause = true;
        Kill = false;
        Time.timeScale = 1f;

        Health = 3;
        WatsonCoin = 0;
        WatsonCoin_nb1 = 0;
        WatsonCoin_nb2 = 0;
        WatsonCoin_nb3 = 0;
        PurpleWatsonCoin = 0;
        Coin = 0;
        Timer = 400;

        fnc_UpdateHealth = false;
        fnc_UpdateWatsonCoin = false;
        fnc_UpdateCoin = false;
        fnc_GotPurpleCoin = false;
        fnc_GotPurpleCoinSpawn = false;
        fnc_DeadCoroutine = false;
        fnc_isPaused = false;
        fnc_GetDamaged = false;

        //load volume data just in case
        AudioListener.volume = PlayerPrefs.GetFloat("global_volume");

    }

    void Start()
    {
        
        deadMenu.SetActive(false);
        
        Character = GameObject.Find("/Character/Sprite");
        DeathVerticalLimit = GameObject.Find("/Globals/DeathBoundaries").transform.position.y;
        StartCoroutine(TimerDown());

    }

    void Update()
    {

        //Pause Config
        if(canPause){
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
        }

        if(fnc_DeadCoroutine){
            
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
                    SceneManager.LoadScene("TestLevel");
                } else if(menuPausePosition == 1){
                    TitleScreen.loadInBreakTheTarget = true;
                    SceneManager.LoadScene("Scenes/TitleScreen");
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
                    Debug.Log("TO ADVENTURE LEVEL SELECT");
                } else if(menuPausePosition == 2){
                    Time.timeScale = 1f;
                    fnc_isPaused = false;
                    SceneManager.LoadScene("TitleScreen");
                }
            }
        }

        //Kill Config
        if(!fnc_DeadCoroutine && Config.Kill){
            StartCoroutine(CallDeath());
        }
        if(!fnc_GetDamaged && Config.GetDamaged){
            StartCoroutine(GetDamage());
        }
    }


    void FixedUpdate()
    {

        //Kill player if his Y coordonate are under the limit gameobject
        if(Character.transform.position.y < DeathVerticalLimit && !fnc_DeadCoroutine){
            Config.Kill = true;
        }

        //update player Health UI
        if(Config.fnc_UpdateHealth){
            if(Health > 3){ Health = 3; }
            if(Health < 0){ Health = 0; }

            Image life = GameObject.Find("/Canvas/Life").GetComponent<Image>();
            if(Health == 3){ life.sprite = life_3; }
            if(Health == 2){ life.sprite = life_2; }
            if(Health == 1){ life.sprite = life_1; }
            if(Health == 0){ life.sprite = life_0; }
            Config.fnc_UpdateHealth = false;
        }

        //update Watson Coin UI
        if(Config.fnc_UpdateWatsonCoin){
            if(WatsonCoin > 3){ WatsonCoin = 3; }
            if(WatsonCoin < 0){ WatsonCoin = 0; }

            if(Config.WatsonCoin_nb1 == 1){
                GameObject.Find("/Canvas/WatsonCoin/wc1").SetActive(true);
            }
            if(Config.WatsonCoin_nb2 == 1){
                GameObject.Find("/Canvas/WatsonCoin/wc2").SetActive(true);
            }
            if(Config.WatsonCoin_nb3 == 1){
                GameObject.Find("/Canvas/WatsonCoin/wc3").SetActive(true);
            }
            if(Config.PurpleWatsonCoin == 1){
                GameObject.Find("/Canvas/WatsonCoin/PurpleWatsonCoin").SetActive(true);
            }

            Config.fnc_UpdateWatsonCoin = false;
        }

        //update Watson Coin UI
        if(Config.fnc_UpdateCoin){
            if(Coin > 999){ Coin = 999; }
            if(Coin < 0){ Coin = 0; }

            Image coin = GameObject.Find("/Canvas/Coin").GetComponent<Image>();

            string coinstr = Coin.ToString("d3");
            string[] coinArray = new string[coinstr.Length];

            for (int i = 0; i < coinstr.Length; i++) {
                coinArray[i] = coinstr[i].ToString();
                if(coinArray[i] == "9"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_9; }
                if(coinArray[i] == "8"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_8; }
                if(coinArray[i] == "7"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_7; }
                if(coinArray[i] == "6"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_6; }
                if(coinArray[i] == "5"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_5; }
                if(coinArray[i] == "4"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_4; }
                if(coinArray[i] == "3"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_3; }
                if(coinArray[i] == "2"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_2; }
                if(coinArray[i] == "1"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_1; }
                if(coinArray[i] == "0"){ GameObject.Find("/Canvas/Coin_"+(i+1)).GetComponent<Image>().sprite = number_0; }
                
            }

            Config.fnc_UpdateCoin = false;
        }

        //if 100 coins, give purple watson coin
        if(Config.Coin >= 100 && !fnc_GotPurpleCoinSpawn){
            StartCoroutine(SpawnPurpleWatsonCoin());
            fnc_GotPurpleCoinSpawn = true;
        }

        //if dead, deadise lé
        if(Config.Health <= 0 && !fnc_DeadCoroutine){
            Config.Kill = true;
        }
    }

    IEnumerator SpawnPurpleWatsonCoin(){
        GameObject purpleCoin = Instantiate(prefab_PurpleWatsonCoin, new Vector3(Character.transform.position.x, Character.transform.position.y, 0), Quaternion.identity);
        GameObject purpleCoinBloc = purpleCoin.transform.GetChild(0).gameObject;
        GameObject purpleCoinSpawn = purpleCoin.transform.GetChild(1).gameObject;

        purpleCoinBloc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 16f, ForceMode2D.Impulse);
        while(purpleCoinBloc.transform.localScale.x < 1f){
            purpleCoinBloc.transform.localScale = new Vector3(purpleCoinBloc.transform.localScale.x+0.012f,purpleCoinBloc.transform.localScale.y+0.012f,1f);
            yield return new WaitForSeconds(0.005f);
        }

        purpleCoinBloc.transform.localScale = new Vector3(1f,1f,1f);

        yield return null;
    }

    IEnumerator TimerDown(){

        Image Timer_1 = GameObject.Find("/Canvas/Timer_1").GetComponent<Image>();
        Image Timer_2 = GameObject.Find("/Canvas/Timer_2").GetComponent<Image>();
        Image Timer_3 = GameObject.Find("/Canvas/Timer_3").GetComponent<Image>();

        while(Timer > 0){
            if(Health > 0){
                yield return new WaitForSeconds(1f);
                Timer = Timer - 1;

                string time = Timer.ToString("d3");
                string[] timeArray = new string[time.Length];

                for (int i = 0; i < time.Length; i++) {
                    timeArray[i] = time[i].ToString();
                    if(timeArray[i] == "9"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_9; }
                    if(timeArray[i] == "8"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_8; }
                    if(timeArray[i] == "7"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_7; }
                    if(timeArray[i] == "6"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_6; }
                    if(timeArray[i] == "5"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_5; }
                    if(timeArray[i] == "4"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_4; }
                    if(timeArray[i] == "3"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_3; }
                    if(timeArray[i] == "2"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_2; }
                    if(timeArray[i] == "1"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_1; }
                    if(timeArray[i] == "0"){ GameObject.Find("/Canvas/Timer_"+(i+1)).GetComponent<Image>().sprite = number_0; }
                    
                }

               
            } else {
                yield return null;
            }
            yield return null;
        }

        if(Timer <= 0){
            Timer = 0;
            if(!fnc_DeadCoroutine){
                Config.Kill = true;
            }
        }

    }

    IEnumerator CallDeath(){
        Config.fnc_DeadCoroutine = true;
        Config.Health = 0;
        Config.fnc_UpdateHealth = true;

        AudioListener.volume = AudioListener.volume / 2;
        Kill = false;
        deadMenu.SetActive(true);
        menuPausePosition = 0;
        Movement.haveControl = false;
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator GetDamage(){

        fnc_GetDamaged = true; 

        Debug.Log("DAMAGED");
        Config.Health -= 1;
        Config.fnc_UpdateHealth = true;

        StartCoroutine(InvulnerabilityFrames());
        
        yield return new WaitForSeconds(1f);
        Config.GetDamaged = false;
        fnc_GetDamaged = false;
        Config.Character.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        
        Debug.Log("INV. FRAME END");

    }

    IEnumerator InvulnerabilityFrames(){
        SpriteRenderer sprout = Config.Character.GetComponent<SpriteRenderer>();
        while(fnc_GetDamaged){
            sprout.color = new Color(1f,1f,1f,0.3f);
            yield return new WaitForSeconds(0.05f);
            sprout.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.05f);
        }
        sprout.color = new Color(1f,1f,1f,1f);
    }

}
