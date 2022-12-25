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

    public Image moony;

    public Sprite moony_frame_1;
    public Sprite moony_frame_2;
    public Sprite moony_frame_3;

    public bool canMove = true;
    public int isFinalDestinationUnlocked = 0;
    public Color lockedColor;

    public bool inOptions = false;

    public int menuPosition = 0;
    public int menuPositionOptions = 0;

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

        if(inOptions){
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

        if(inOptions){
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

    void MenuSelect(){

        if(inOptions){
            if(menuPositionOptions == 0){
                Debug.Log("MASTER VOLUME");
                
            } else if(menuPositionOptions == 1){
                Debug.Log("ERASE DATA");
                
            } else if(menuPositionOptions == 2){
                inOptions = false;
                iTween.MoveTo(menu_option, iTween.Hash("position", new Vector3(20f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
                iTween.MoveTo(menu_main, iTween.Hash("position", new Vector3(0f,0f,0f), "time", 1.3f, "easetype", iTween.EaseType.easeOutBack));
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
                Debug.Log("COLLECTION");
                SceneManager.LoadScene("collection");
            } else if(menuPosition == 4){
                inOptions = true;
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
