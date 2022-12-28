using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{

    [Header("Data Configuration")]
    public int LevelId = 1;
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

    [Header("Dynamic Prefab")]
    public GameObject prefab_PurpleWatsonCoin;
    public GameObject emptyGameObject;

    public static bool fnc_UpdateHealth = false;
    public static bool fnc_UpdateWatsonCoin = false;
    public static bool fnc_UpdateCoin = false;
    public static bool fnc_GotPurpleCoin = false;
    public static bool fnc_GotPurpleCoinSpawn = false;

    void Awake(){
              
        //load currently obtained Purple / Watson Coins
        if(PlayerPrefs.GetInt("stage" + LevelId + "_pwc") == 1) { PurpleWatsonCoin = 1; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc1") == 1) { WatsonCoin_nb1 = 1; WatsonCoin++; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc2") == 1) { WatsonCoin_nb2 = 1; WatsonCoin++; }
        if(PlayerPrefs.GetInt("stage" + LevelId + "_wc3") == 1) { WatsonCoin_nb3 = 1; WatsonCoin++; }
        Config.fnc_UpdateWatsonCoin = true;

    }

    void Start()
    {
        Character = GameObject.Find("/Character/Sprite");
        DeathVerticalLimit = GameObject.Find("/Globals/DeathBoundaries").transform.position.y;
        StartCoroutine(TimerDown());

    }

    void FixedUpdate()
    {

        //Kill player if his Y coordonate are under the limit gameobject
        if(Character.transform.position.y < DeathVerticalLimit && !Movement.isDead){
            Movement.haveControl = false;
            Health = 0;
            Config.fnc_UpdateHealth = true;
            Movement.isDead = true; 
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
        if(Movement.isDead){
            //mourraise
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
            Movement.haveControl = false;
            Movement.isDead = true;
        }

    }

    void CallDeath(){

    }

}
