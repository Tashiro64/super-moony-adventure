using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public Image opt_adventureMode;
    public Image opt_finalDestination;
    public Image opt_breakTheTarget;
    public Image opt_lottery;
    public Image opt_collection;
    public Image opt_options;
    public Image opt_quit;

    public Image opt_erase_yes;
    public Image opt_erase_no;

    public Image opt_masterVolume;
    public Image opt_eraseData;
    public Image opt_backToMenu;

    public GameObject menu_main;
    public GameObject menu_option;
    public GameObject menu_collection;
    public GameObject menu_background;
    public GameObject menu_confirm;
    
    public RectTransform volume_bar;

    public Image moony;

    public AudioSource moveSoundSource;
    public AudioClip moveSound;
    public AudioClip confirmSound;

    public Sprite moony_frame_1;
    public Sprite moony_frame_2;
    public Sprite moony_frame_3;

    public bool canMove = true;
    public int isFinalDestinationUnlocked = 0;
    public int isBreakTheTargetUnlocked = 0;
    public Color lockedColor;
    public Color lockedColorBTT;

    public bool inOptions = false;
    public bool inCollection = false;
    public bool inEraseConfirm = false;
    public bool blockControl = false;
    public bool splashScreenDone = false;

    public int menuPosition = 0;
    public int menuPositionOptions = 0;
    public int menuPositionErase = 1;

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

    public GameObject SplashScreen_T;
    public GameObject SplashScreen_A;
    public GameObject SplashScreen_S; 
    public GameObject SplashScreen_H;
    public GameObject SplashScreen_I;
    public GameObject SplashScreen_R;
    public GameObject SplashScreen_O;

    public GameObject Presents_P;
    public GameObject Presents_R;
    public GameObject Presents_E;
    public GameObject Presents_S;
    public GameObject Presents_E2;
    public GameObject Presents_N;
    public GameObject Presents_T;
    public GameObject Presents_S2;

    public AudioClip oopelay;

    void Start()
    {

        isFinalDestinationUnlocked = PlayerPrefs.GetInt("global_finalDestination_unlocked", 0);
        isBreakTheTargetUnlocked = PlayerPrefs.GetInt("global_breakTheTarget_unlocked", 0);

        if(isFinalDestinationUnlocked == 1){
            lockedColor = new Color(1f,1f,1f,1f);
        } else {
            lockedColor = new Color(0.5f,0.5f,0.5f,1f);
        }
        if(isBreakTheTargetUnlocked == 1){
            lockedColorBTT = new Color(1f,1f,1f,1f);
        } else {
            lockedColorBTT = new Color(0.5f,0.5f,0.5f,1f);
        }

        StartCoroutine(MoveMoony());
        
        if(!splashScreenDone){
            //splash screen
            if(GameObject.Find("/Canvas/Preload/Image").GetComponent<Image>().color.a > 0f){
                StartCoroutine(SplashScreen());
            } else {
                //skip splash screen for debugging purpose
                blockControl = false;
                splashScreenDone = true;
            }
        }


    }

    void Update()
    {

        if(!blockControl){
            if(Input.GetButtonDown("Submit")){
                MenuSelect();
            }

            if(inCollection){

            } else if(inOptions){
                if(inEraseConfirm){
                    if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                        menuPositionErase--;
                        moveSoundSource.clip = moveSound;
                        moveSoundSource.Play();
                        canMove = false;
                    }
                    if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                        menuPositionErase++;
                        moveSoundSource.clip = moveSound;
                        moveSoundSource.Play();
                        canMove = false;
                    }

                    if(menuPositionErase < 0){
                        menuPositionErase = 0;
                    }
                    if(menuPositionErase > 1){
                        menuPositionErase = 1;
                    }
                } else {
                    if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                        menuPositionOptions--;
                        moveSoundSource.clip = moveSound;
                        moveSoundSource.Play();
                        canMove = false;
                    }
                    if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                        menuPositionOptions++;
                        moveSoundSource.clip = moveSound;
                        moveSoundSource.Play();
                        canMove = false;
                    }

                    if(menuPositionOptions < 0){
                        menuPositionOptions = 0;
                    }
                    if(menuPositionOptions > 2){
                        menuPositionOptions = 2;
                    }
                }
            } else {
                if(Input.GetAxisRaw("Vertical") > 0 && canMove){
                    menuPosition--;
                    moveSoundSource.clip = moveSound;
                    moveSoundSource.Play();
                    if(isBreakTheTargetUnlocked == 0 && menuPosition == 2){
                        
                        menuPosition--;
                    }
                    if(isFinalDestinationUnlocked == 0 && menuPosition == 1){
                        menuPosition--;
                    }
                    canMove = false;
                }
                if(Input.GetAxisRaw("Vertical") < 0 && canMove){
                    menuPosition++;
                    moveSoundSource.clip = moveSound;
                    moveSoundSource.Play();
                    if(isFinalDestinationUnlocked == 0 && menuPosition == 1){
                        menuPosition++;
                    }
                    if(isBreakTheTargetUnlocked == 0 && menuPosition == 2){
                        menuPosition++;
                    }
                    canMove = false;
                }

                if(menuPosition < 0){
                    menuPosition = 0;
                }
                if(menuPosition > 6){
                    menuPosition = 6;
                }
            }

        

            if(Input.GetAxisRaw("Vertical") == 0 && !canMove){
                canMove = true;
            }

            if(inCollection){

            } else if(inOptions){
                if(inEraseConfirm){
                    if(menuPositionErase == 0){
                        opt_erase_yes.color = new Color(0f,1f,0.78f,1f);
                        opt_erase_no.color = new Color(1f,1f,1f,1f);
                    } else if(menuPositionErase == 1){
                        opt_erase_yes.color = new Color(1f,1f,1f,1f);
                        opt_erase_no.color = new Color(0f,1f,0.78f,1f);
                    }
                } else {
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
                }
            } else {
                if(menuPosition == 0){
                    opt_adventureMode.color = new Color(0f,1f,0.78f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 1){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = new Color(0f,1f,0.78f,1f);
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 2){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = new Color(0f,1f,0.78f,1f);;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 3){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(0f,1f,0.78f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 4){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(0f,1f,0.78f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 5){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(0f,1f,0.78f,1f);
                    opt_quit.color = new Color(1f,1f,1f,1f);
                } else if(menuPosition == 6){
                    opt_adventureMode.color =  new Color(1f,1f,1f,1f);
                    opt_finalDestination.color = lockedColor;
                    opt_breakTheTarget.color = lockedColorBTT;
                    opt_lottery.color = new Color(1f,1f,1f,1f);
                    opt_collection.color = new Color(1f,1f,1f,1f);
                    opt_options.color = new Color(1f,1f,1f,1f);
                    opt_quit.color = new Color(0f,1f,0.78f,1f);
                }
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

        if(!blockControl){
            moveSoundSource.clip = confirmSound;
            moveSoundSource.Play();
            if(inCollection){
                inOptions = false;
                inCollection = false;
                iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                menuPosition = 4;
                menuPositionOptions = 0;
            } else if(inOptions){
                if(inEraseConfirm){
                    if(menuPositionErase == 0){
                        StartCoroutine(DeleteSaveData());
                        

                    } else if(menuPositionErase == 1){
                        Debug.Log("GOING BACK TO OPTIONS");
                        menu_confirm.SetActive(false);
                        inEraseConfirm = false;
                    }
                } else {
                    if(menuPositionOptions == 0){
                        Debug.Log("MASTER VOLUME");

                    } else if(menuPositionOptions == 1){
                        menu_confirm.SetActive(true);
                        inEraseConfirm = true;

                    } else if(menuPositionOptions == 2){
                        inOptions = false;
                        inCollection = false;
                        iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                        iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                        iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                        iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                        PlayerPrefs.SetFloat("global_volume", AudioListener.volume);
                        menuPosition = 5;
                        menuPositionOptions = 0;
                    }
                }
            } else {
                if(menuPosition == 0){
                    Debug.Log("ADVENTURE MODE");
                    SceneManager.LoadScene("adventureMode");
                } else if(menuPosition == 1){
                    Debug.Log("FINAL DESTINATION");
                    SceneManager.LoadScene("finalDestination");
                } else if(menuPosition == 2){
                    Debug.Log("BREAK THE TARGET");
                    SceneManager.LoadScene("breakTheTarget");
                } else if(menuPosition == 3){
                    Debug.Log("LOTTERY");
                    SceneManager.LoadScene("lottery");
                } else if(menuPosition == 4){
                    menu_collection.SetActive(true);
                    menu_option.SetActive(false);
                    CalculateProgress();
                    inOptions = false;
                    inCollection = true;
                    iTween.MoveTo(menu_background, iTween.Hash("position", new Vector3(-0.86f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                    iTween.MoveTo(menu_collection, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                    iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                    iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(-20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                    menuPosition = 0;
                    menuPositionOptions = 0;
                } else if(menuPosition == 5){
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
                } else if(menuPosition == 6){
                    Debug.Log("QUIT GAME");
                    Application.Quit();
                }
            }
        }
    }

    void CalculateProgress(){

        /*
        Stage 1-8 completed = 8 points
        Stage 1-8 (3 coins, 1 purle coins) = 32 points
        lottery 1-20 = 20 points
        complete all 4 bonus stage = 4 points
        beat all bosses = 5 points
        beat final destination = 1 points;
        total points = 70;
        */
        int totalPointNeeded = 75;

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
        if(PlayerPrefs.GetInt("stage3_pwc") == 1){ GlobalProgress++; GameObject.Find("/Canvas/Collection Menu/level3/pwc").SetActive(true); }

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

        
        if(PlayerPrefs.GetInt("bonusStage_1_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("bonusStage_2_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("bonusStage_3_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("bonusStage_4_completed") == 1){ GlobalProgress++; }

        if(PlayerPrefs.GetInt("breakTheTarget_1_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("breakTheTarget_2_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("breakTheTarget_3_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("breakTheTarget_4_completed") == 1){ GlobalProgress++; }
        if(PlayerPrefs.GetInt("breakTheTarget_5_completed") == 1){ GlobalProgress++; }

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



        //trophies
        string col_trophy = TrophyProgress.ToString("00");
        string[] col_trophyArray = new string[col_trophy.Length];

        for (int i = 0; i < col_trophy.Length; i++) {
            col_trophyArray[i] = col_trophy[i].ToString();
            if(col_trophyArray[i] == "9"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_9; }
            if(col_trophyArray[i] == "8"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_8; }
            if(col_trophyArray[i] == "7"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_7; }
            if(col_trophyArray[i] == "6"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_6; }
            if(col_trophyArray[i] == "5"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_5; }
            if(col_trophyArray[i] == "4"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_4; }
            if(col_trophyArray[i] == "3"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_3; }
            if(col_trophyArray[i] == "2"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_2; }
            if(col_trophyArray[i] == "1"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_1; }
            if(col_trophyArray[i] == "0"){ GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_"+(i+1)).GetComponent<Image>().sprite = number_0; }
        }
        
        if(TrophyProgress <= 10 && col_trophyArray[0] == "0"){
            GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_1").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/collected_trophies/count_1").SetActive(true);
        }


        //completion percentage
        float percent = Mathf.Ceil(GlobalProgress * 100 / totalPointNeeded);

        if(percent < 0) { percent = 0; }
        if(percent > 100) { percent = 100; }
        
        string col_percent = percent.ToString("000");
        string[] col_percentArray = new string[col_percent.Length];

        for (int i = 0; i < col_percent.Length; i++) {
            col_percentArray[i] = col_percent[i].ToString();
            if(col_percentArray[i] == "9"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_9; }
            if(col_percentArray[i] == "8"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_8; }
            if(col_percentArray[i] == "7"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_7; }
            if(col_percentArray[i] == "6"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_6; }
            if(col_percentArray[i] == "5"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_5; }
            if(col_percentArray[i] == "4"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_4; }
            if(col_percentArray[i] == "3"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_3; }
            if(col_percentArray[i] == "2"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_2; }
            if(col_percentArray[i] == "1"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_1; }
            if(col_percentArray[i] == "0"){ GameObject.Find("/Canvas/Collection Menu/global_completion/count_"+(i+1)).GetComponent<Image>().sprite = number_0; }
        }
        
        
        if(percent <= 100 && col_percentArray[0] == "0"){
            GameObject.Find("/Canvas/Collection Menu/global_completion/count_1").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/global_completion/count_1").SetActive(true);
        }
        if(percent <= 10 && col_percentArray[1] == "0"){
            GameObject.Find("/Canvas/Collection Menu/global_completion/count_2").SetActive(false);
        } else {
            GameObject.Find("/Canvas/Collection Menu/global_completion/count_2").SetActive(true);
        }


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


    IEnumerator DeleteSaveData(){
        blockControl = true;
        PlayerPrefs.DeleteAll();
        //AudioSource audio = GameObject.Find("/Main Camera").GetComponent<AudioSource>().loop = false;
        AudioSource audio = GameObject.Find("/Main Camera").GetComponent<AudioSource>();
        audio.loop = false;
        audio.clip = oopelay;
        audio.Play();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("TitleScreen");
    }


    IEnumerator SplashScreen(){

        blockControl = true;

        Image splashContainer = GameObject.Find("/Canvas/Preload/Image").GetComponent<Image>();
        Image splash_T = GameObject.Find("/Canvas/Preload/T").GetComponent<Image>();
        Image splash_A = GameObject.Find("/Canvas/Preload/A").GetComponent<Image>();
        Image splash_S = GameObject.Find("/Canvas/Preload/S").GetComponent<Image>();
        Image splash_H = GameObject.Find("/Canvas/Preload/H").GetComponent<Image>();
        Image splash_I = GameObject.Find("/Canvas/Preload/I").GetComponent<Image>();
        Image splash_R = GameObject.Find("/Canvas/Preload/R").GetComponent<Image>();
        Image splash_O = GameObject.Find("/Canvas/Preload/O").GetComponent<Image>();

        Image splashp_P = GameObject.Find("/Canvas/Preload/p_P").GetComponent<Image>();
        Image splashp_R = GameObject.Find("/Canvas/Preload/p_R").GetComponent<Image>();
        Image splashp_E = GameObject.Find("/Canvas/Preload/p_E").GetComponent<Image>();
        Image splashp_S = GameObject.Find("/Canvas/Preload/p_S").GetComponent<Image>();
        Image splashp_E2 = GameObject.Find("/Canvas/Preload/p_E2").GetComponent<Image>();
        Image splashp_N = GameObject.Find("/Canvas/Preload/p_N").GetComponent<Image>();
        Image splashp_T = GameObject.Find("/Canvas/Preload/p_T").GetComponent<Image>();
        Image splashp_S2 = GameObject.Find("/Canvas/Preload/p_S2").GetComponent<Image>();
        
        yield return new WaitForSeconds(1f);

        //T
        float opacity = 0f;
        iTween.MoveTo(SplashScreen_T, iTween.Hash("position", new Vector3(-1.5f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_T.color.a < 1){
            splash_T.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_T.color = new Color(1f,1f,1f,1f);

        //A
        opacity = 0f;
        iTween.MoveTo(SplashScreen_A, iTween.Hash("position", new Vector3(-1f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_A.color.a < 1){
            splash_A.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_A.color = new Color(1f,1f,1f,1f);
        
        //S
        opacity = 0f;
        iTween.MoveTo(SplashScreen_S, iTween.Hash("position", new Vector3(-0.5f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_S.color.a < 1){
            splash_S.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_S.color = new Color(1f,1f,1f,1f);

        //H
        opacity = 0f;
        iTween.MoveTo(SplashScreen_H, iTween.Hash("position", new Vector3(0f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_H.color.a < 1){
            splash_H.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_H.color = new Color(1f,1f,1f,1f);
        
        //I
        opacity = 0f;
        iTween.MoveTo(SplashScreen_I, iTween.Hash("position", new Vector3(0.5f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_I.color.a < 1){
            splash_I.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_I.color = new Color(1f,1f,1f,1f);
        
        //R
        opacity = 0f;
        iTween.MoveTo(SplashScreen_R, iTween.Hash("position", new Vector3(1f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_R.color.a < 1){
            splash_R.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_R.color = new Color(1f,1f,1f,1f);
        
        //O
        opacity = 0f;
        iTween.MoveTo(SplashScreen_O, iTween.Hash("position", new Vector3(1.5f,1f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splash_O.color.a < 1){
            splash_O.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splash_O.color = new Color(1f,1f,1f,1f);

        //Presents
        opacity = 0f;
        iTween.MoveTo(Presents_P, iTween.Hash("position", new Vector3(-1.75f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_P.color.a < 1){
            splashp_P.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_P.color = new Color(1f,1f,1f,1f);
        
        //pResents
        opacity = 0f;
        iTween.MoveTo(Presents_R, iTween.Hash("position", new Vector3(-1.25f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_R.color.a < 1){
            splashp_R.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_R.color = new Color(1f,1f,1f,1f);

        //prEsents
        opacity = 0f;
        iTween.MoveTo(Presents_E, iTween.Hash("position", new Vector3(-0.75f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_E.color.a < 1){
            splashp_E.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_E.color = new Color(1f,1f,1f,1f);

        //preSents
        opacity = 0f;
        iTween.MoveTo(Presents_S, iTween.Hash("position", new Vector3(-0.25f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_S.color.a < 1){
            splashp_S.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_S.color = new Color(1f,1f,1f,1f);

        //presEnts
        opacity = 0f;
        iTween.MoveTo(Presents_E2, iTween.Hash("position", new Vector3(0.25f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_E2.color.a < 1){
            splashp_E2.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_E2.color = new Color(1f,1f,1f,1f);

        //preseNts
        opacity = 0f;
        iTween.MoveTo(Presents_N, iTween.Hash("position", new Vector3(0.75f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_N.color.a < 1){
            splashp_N.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_N.color = new Color(1f,1f,1f,1f);

        //presenTs
        opacity = 0f;
        iTween.MoveTo(Presents_T, iTween.Hash("position", new Vector3(1.25f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_T.color.a < 1){
            splashp_T.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_T.color = new Color(1f,1f,1f,1f);

        //presentS
        opacity = 0f;
        iTween.MoveTo(Presents_S2, iTween.Hash("position", new Vector3(1.75f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
        while(splashp_S2.color.a < 1){
            splashp_S2.color = new Color(1f,1f,1f, opacity += 0.1f);
            yield return new WaitForSeconds(0.025f);
        }
        splashp_S2.color = new Color(1f,1f,1f,1f);






        
        yield return new WaitForSeconds(3.5f);
        opacity = 1f;

        splash_T.color = new Color(0f,0f,0f,0f);
        splash_A.color = new Color(0f,0f,0f,0f);
        splash_S.color = new Color(0f,0f,0f,0f);
        splash_H.color = new Color(0f,0f,0f,0f);
        splash_I.color = new Color(0f,0f,0f,0f);
        splash_R.color = new Color(0f,0f,0f,0f);
        splash_O.color = new Color(0f,0f,0f,0f);

        splashp_P.color = new Color(0f,0f,0f,0f);
        splashp_R.color = new Color(0f,0f,0f,0f);
        splashp_E.color = new Color(0f,0f,0f,0f);
        splashp_S.color = new Color(0f,0f,0f,0f);
        splashp_E2.color = new Color(0f,0f,0f,0f);
        splashp_N.color = new Color(0f,0f,0f,0f);
        splashp_T.color = new Color(0f,0f,0f,0f);
        splashp_S2.color = new Color(0f,0f,0f,0f);

        while(splashContainer.color.a > 0){
            splashContainer.color = new Color(1f,1f,1f, opacity -= 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        splashContainer.color = new Color(1f,1f,1f,0f);
        splashScreenDone = true;
        blockControl = false;
        
    }

}
