using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public Image opt_adventureMode;
    public Image opt_finalDestination;
    public Image opt_lottery;
    public Image opt_collection;
    public Image opt_options;
    public Image opt_quit;

    public Image opt_masterVolume;
    public Image opt_eraseData;
    public Image opt_backToMenu;

    public GameObject menu_main;
    public GameObject menu_option;
    public GameObject menu_collection;
    public GameObject menu_background;
    public RectTransform volume_bar;

    public Image moony;

    public Sprite moony_frame_1;
    public Sprite moony_frame_2;
    public Sprite moony_frame_3;

    public bool canMove = true;
    public int isFinalDestinationUnlocked = 0;
    public Color lockedColor;

    public bool inOptions = false;
    public bool inCollection = false;

    public int menuPosition = 0;
    public int menuPositionOptions = 0;

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

    void Start()
    {

        isFinalDestinationUnlocked = PlayerPrefs.GetInt("global_finalDestination_unlocked", 0);

        if(isFinalDestinationUnlocked == 1){
            lockedColor = new Color(1f,1f,1f,1f);
        } else {
            lockedColor = new Color(0.5f,0.5f,0.5f,1f);
        }

        StartCoroutine(MoveMoony());

    }

    void Update()
    {

        if(Input.GetButtonDown("Jump")){
            MenuSelect();
        }

        if(inCollection){

        } else if(inOptions){
            if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                menuPositionOptions--;
                canMove = false;
            }
            if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                menuPositionOptions++;
                canMove = false;
            }

            if(menuPositionOptions < 0){
                menuPositionOptions = 0;
            }
            if(menuPositionOptions > 2){
                menuPositionOptions = 2;
            }
        } else {
            if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                menuPosition--;
                if(isFinalDestinationUnlocked == 0 && menuPosition == 1){
                    menuPosition--;
                }
                canMove = false;
            }
            if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                menuPosition++;
                if(isFinalDestinationUnlocked == 0 && menuPosition == 1){
                    menuPosition++;
                }
                canMove = false;
            }

            if(menuPosition < 0){
                menuPosition = 0;
            }
            if(menuPosition > 5){
                menuPosition = 5;
            }
        }

       

        if(Input.GetAxis("Vertical") == 0 && !canMove){
            canMove = true;
        }

        if(inCollection){

        } else if(inOptions){
            if(menuPositionOptions == 0){
                opt_masterVolume.color = new Color(0f,1f,0.78f,1f);
                opt_eraseData.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPositionOptions == 1){
                opt_masterVolume.color = new Color(1f,1f,1f,1f);
                opt_eraseData.color = new Color(0f,1f,0.78f,1f);
                opt_backToMenu.color = new Color(1f,1f,1f,1f);
            } else if(menuPositionOptions == 2){
                opt_masterVolume.color = new Color(1f,1f,1f,1f);
                opt_eraseData.color = new Color(1f,1f,1f,1f);
                opt_backToMenu.color = new Color(0f,1f,0.78f,1f);
            }
        } else {
            if(menuPosition == 0){
                opt_adventureMode.color = new Color(0f,1f,0.78f,1f);
                opt_finalDestination.color = lockedColor;
                opt_lottery.color = new Color(1f,1f,1f,1f);
                opt_collection.color = new Color(1f,1f,1f,1f);
                opt_options.color = new Color(1f,1f,1f,1f);
                opt_quit.color = new Color(1f,1f,1f,1f);
            } else if(menuPosition == 1){
                opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                opt_finalDestination.color = new Color(0f,1f,0.78f,1f);
                opt_lottery.color = new Color(1f,1f,1f,1f);
                opt_collection.color = new Color(1f,1f,1f,1f);
                opt_options.color = new Color(1f,1f,1f,1f);
                opt_quit.color = new Color(1f,1f,1f,1f);
            } else if(menuPosition == 2){
                opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                opt_finalDestination.color = lockedColor;
                opt_lottery.color = new Color(0f,1f,0.78f,1f);
                opt_collection.color = new Color(1f,1f,1f,1f);
                opt_options.color = new Color(1f,1f,1f,1f);
                opt_quit.color = new Color(1f,1f,1f,1f);
            } else if(menuPosition == 3){
                opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                opt_finalDestination.color = lockedColor;
                opt_lottery.color = new Color(1f,1f,1f,1f);
                opt_collection.color = new Color(0f,1f,0.78f,1f);
                opt_options.color = new Color(1f,1f,1f,1f);
                opt_quit.color = new Color(1f,1f,1f,1f);
            } else if(menuPosition == 4){
                opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                opt_finalDestination.color = lockedColor;
                opt_lottery.color = new Color(1f,1f,1f,1f);
                opt_collection.color = new Color(1f,1f,1f,1f);
                opt_options.color = new Color(0f,1f,0.78f,1f);
                opt_quit.color = new Color(1f,1f,1f,1f);
            } else if(menuPosition == 5){
                opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                opt_finalDestination.color = lockedColor;
                opt_lottery.color = new Color(1f,1f,1f,1f);
                opt_collection.color = new Color(1f,1f,1f,1f);
                opt_options.color = new Color(1f,1f,1f,1f);
                opt_quit.color = new Color(0f,1f,0.78f,1f);
            }
        }
    }

    void FixedUpdate(){
        
        if(inOptions && menuPositionOptions == 0){
            if(Input.GetAxisRaw("Horizontal") > 0 && AudioListener.volume <= 1.0f){
                AudioListener.volume += 0.01f;
            }
            if(Input.GetAxisRaw("Horizontal") < 0 && AudioListener.volume >= 0.0f){
                AudioListener.volume -= 0.01f;
            }
                if(AudioListener.volume > 1) { AudioListener.volume = 1; }
                if(AudioListener.volume < 0) { AudioListener.volume = 0; }
                volume_bar.sizeDelta = new Vector2 (AudioListener.volume * 556.38f, 43.78f);
        }

    }

    void MenuSelect(){

        if(inCollection){
            inOptions = false;
            inCollection = false;
            iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
            iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
            iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
            iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
            menuPosition = 3;
            menuPositionOptions = 0;
        } else if(inOptions){
            if(menuPositionOptions == 0){
                Debug.Log("MASTER VOLUME");
                
            } else if(menuPositionOptions == 1){
                Debug.Log("ERASE DATA");
                
            } else if(menuPositionOptions == 2){
                inOptions = false;
                inCollection = false;
                iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                PlayerPrefs.SetFloat("global_volume", AudioListener.volume);
                menuPosition = 4;
                menuPositionOptions = 0;
            }
        } else {
            if(menuPosition == 0){
                Debug.Log("ADVENTURE MODE");
                SceneManager.LoadScene("adventureMode");
            } else if(menuPosition == 1){
                Debug.Log("FINAL DESTINATION");
                SceneManager.LoadScene("finalDestination");
            } else if(menuPosition == 2){
                Debug.Log("LOTTERY");
                SceneManager.LoadScene("lottery");
            } else if(menuPosition == 3){
                CalculateProgress();
                menu_collection.SetActive(true);
                menu_option.SetActive(false);
                inOptions = false;
                inCollection = true;
                iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(-0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(-20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                menuPosition = 0;
                menuPositionOptions = 0;
            } else if(menuPosition == 4){
                menu_collection.SetActive(false);
                menu_option.SetActive(true);
                inOptions = true;
                inCollection = false;
                iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(-0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(-20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                menuPosition = 0;
                menuPositionOptions = 0;
            } else if(menuPosition == 5){
                Debug.Log("QUIT GAME");
                Application.Quit();
            }
        }
    }

    void CalculateProgress(){

        /*
        Stage 1-8 completed = 8 points
        Stage 1-8 (3 coins, 1 purle coins) = 32 points
        lottery 1-20 = 20 points
        beat all bosses = 5 points
        beat final destination = 1 points;
        total points = 66;
        */

        int GlobalProgress = 0;
        int TrophyProgress = 0;

        if(PlayerPrefs.GetInt("stage1_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage2_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage3_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage4_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage5_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage6_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage7_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("stage8_completed") == 1){ GlobalProgress++; }

        if(PlayerPrefs.GetInt("stage1_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level1/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage1_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level1/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage1_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level1/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage1_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level1/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage2_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level2/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage2_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level2/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage2_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level2/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage2_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level2/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage3_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level3/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage3_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level3/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage3_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level3/wc3").SetActive(true); }

        if(PlayerPrefs.GetInt("stage4_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level4/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage4_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level4/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage4_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level4/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage4_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level4/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage5_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level5/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage5_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level5/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage5_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level5/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage5_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level5/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage6_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level6/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage6_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level6/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage6_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level6/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage6_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level6/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage7_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level7/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage7_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level7/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage7_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level7/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage7_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level7/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("stage8_wc1") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level8/wc1").SetActive(true); }
        if(PlayerPrefs.GetInt("stage8_wc2") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level8/wc2").SetActive(true); }
        if(PlayerPrefs.GetInt("stage8_wc3") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level8/wc3").SetActive(true); }
        if(PlayerPrefs.GetInt("stage8_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level8/pwc").SetActive(true); }

        if(PlayerPrefs.GetInt("boss1_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("boss2_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("boss3_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("boss4_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("boss5_completed") == 1){ GlobalProgress++; }

        if(PlayerPrefs.GetInt("lottery1_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery2_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery3_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery4_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery5_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery6_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery7_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery8_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery9_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery10_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery11_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery12_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery13_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery14_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery15_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery16_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery17_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery18_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery19_obtained") == 1){ GlobalProgress++; TrophyProgress++; }
        if(PlayerPrefs.GetInt("lottery20_obtained") == 1){ GlobalProgress++; TrophyProgress++; }

        if(PlayerPrefs.GetInt("global_finalDestination_completed") == 1){ GlobalProgress++; }


        //death count coins
        int tmpDeathCount = PlayerPrefs.GetInt("death_count",0);
        if(tmpDeathCount > 99999999) { tmpDeathCount = 99999999; PlayerPrefs.SetInt("death_count",tmpDeathCount); }
        string death = tmpDeathCount.ToString("d8");
        string[] deathArray = new string[death.Length];

        for (int i = 0; i < death.Length; i++) {
            deathArray[i] = death[i].ToString();
            if(deathArray[i] == "9"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_9; }
            if(deathArray[i] == "8"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_8; }
            if(deathArray[i] == "7"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_7; }
            if(deathArray[i] == "6"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_6; }
            if(deathArray[i] == "5"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_5; }
            if(deathArray[i] == "4"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_4; }
            if(deathArray[i] == "3"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_3; }
            if(deathArray[i] == "2"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_2; }
            if(deathArray[i] == "1"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_1; }
            if(deathArray[i] == "0"){ GameObject.Find("/Canvas/Collection Menu/death_count/count_"+(i+1)).GetComponent<Image>().sprite = number_0; }
        }
        
        if(PlayerPrefs.GetInt("death_count") <= 10000000 && deathArray[0] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_1").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_1").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 1000000 && deathArray[1] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_2").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_2").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 100000 && deathArray[2] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_3").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_3").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 10000 && deathArray[3] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_4").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_4").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 1000 && deathArray[4] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_5").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_5").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 100 && deathArray[5] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_6").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_6").SetActive(true);
        }
        if(PlayerPrefs.GetInt("death_count") <= 10 && deathArray[6] == "0"){
            GameObject.Find("/Canvas/Collection Menu/death_count/count_7").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/death_count/count_7").SetActive(true);
        }

        
        //available coins
        int tmpAvailableCoins = PlayerPrefs.GetInt("available_coins",0);
        if(tmpAvailableCoins > 99999999) { tmpAvailableCoins = 99999999; PlayerPrefs.SetInt("available_coins",tmpAvailableCoins); }
        string av_coin = tmpAvailableCoins.ToString("d8");
        string[] av_coinArray = new string[av_coin.Length];

        for (int i = 0; i < av_coin.Length; i++) {
            av_coinArray[i] = av_coin[i].ToString();
            if(av_coinArray[i] == "9"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_9; }
            if(av_coinArray[i] == "8"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_8; }
            if(av_coinArray[i] == "7"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_7; }
            if(av_coinArray[i] == "6"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_6; }
            if(av_coinArray[i] == "5"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_5; }
            if(av_coinArray[i] == "4"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_4; }
            if(av_coinArray[i] == "3"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_3; }
            if(av_coinArray[i] == "2"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_2; }
            if(av_coinArray[i] == "1"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_1; }
            if(av_coinArray[i] == "0"){ GameObject.Find("/Canvas/Collection Menu/available_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_0; }
        }
        
        if(PlayerPrefs.GetInt("available_coins") <= 10000000 && av_coinArray[0] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_1").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_1").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 1000000 && av_coinArray[1] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_2").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_2").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 100000 && av_coinArray[2] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_3").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_3").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 10000 && av_coinArray[3] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_4").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_4").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 1000 && av_coinArray[4] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_5").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_5").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 100 && av_coinArray[5] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_6").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_6").SetActive(true);
        }
        if(PlayerPrefs.GetInt("available_coins") <= 10 && av_coinArray[6] == "0"){
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_7").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/available_coins/count_7").SetActive(true);
        }

        
        //collected coins
        int tmpCollectedCoins = PlayerPrefs.GetInt("global_coins",0);
        if(tmpCollectedCoins > 99999999) { tmpCollectedCoins = 99999999; PlayerPrefs.SetInt("global_coins",tmpCollectedCoins); }
        string col_coin = tmpCollectedCoins.ToString("d8");
        string[] col_coinArray = new string[col_coin.Length];

        for (int i = 0; i < col_coin.Length; i++) {
            col_coinArray[i] = col_coin[i].ToString();
            if(col_coinArray[i] == "9"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_9; }
            if(col_coinArray[i] == "8"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_8; }
            if(col_coinArray[i] == "7"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_7; }
            if(col_coinArray[i] == "6"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_6; }
            if(col_coinArray[i] == "5"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_5; }
            if(col_coinArray[i] == "4"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_4; }
            if(col_coinArray[i] == "3"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_3; }
            if(col_coinArray[i] == "2"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_2; }
            if(col_coinArray[i] == "1"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_1; }
            if(col_coinArray[i] == "0"){ GameObject.Find("/Canvas/Collection Menu/collected_coins/count_"+(i+1)).GetComponent<Image>().sprite = number_0; }
        }
        
        if(PlayerPrefs.GetInt("global_coins") <= 10000000 && col_coinArray[0] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_1").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_1").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 1000000 && col_coinArray[1] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_2").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_2").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 100000 && col_coinArray[2] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_3").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_3").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 10000 && col_coinArray[3] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_4").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_4").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 1000 && col_coinArray[4] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_5").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_5").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 100 && col_coinArray[5] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_6").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_6").SetActive(true);
        }
        if(PlayerPrefs.GetInt("global_coins") <= 10 && col_coinArray[6] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_7").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_coins/count_7").SetActive(true);
        }

        float percent = Mathf.Ceil(GlobalProgress * 100 / 66);

        if(percent < 0) { percent = 0; }
        if(percent > 100) { percent = 100; }

    }

    IEnumerator MoveMoony(){
        while(true){
            moony.sprite = moony_frame_1;
            yield return new WaitForSeconds(0.17f);
            moony.sprite = moony_frame_2;
            yield return new WaitForSeconds(0.17f);
            moony.sprite = moony_frame_3;
            yield return new WaitForSeconds(0.17f);
        }
    }

}
