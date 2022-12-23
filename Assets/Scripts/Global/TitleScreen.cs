using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    public Image opt_adventureMode;
    public Image opt_finalDestination;
    public Image opt_lottery;
    public Image opt_collection;
    public Image opt_options;
    public Image opt_quit;

    public bool canMove = true;

    public int menuPosition = 0;

    void Start()
    {
    }

    void Update()
    {

        if(Input.GetAxisRaw("Vertical") > 0){
            
        }

        if(Input.GetAxisRaw("Vertical") > 0 && canMove){
            menuPosition--;
            canMove = false;
        }
        if(Input.GetAxisRaw("Vertical") < 0 && canMove){
            menuPosition++;
            canMove = false;
        }

        if(menuPosition < 0){
            menuPosition = 0;
        }
        if(menuPosition > 5){
            menuPosition = 5;
        }

        if(Input.GetAxis("Vertical") == 0 && !canMove){
            canMove = true;
        }

        if(menuPosition == 0){
            opt_adventureMode.color = new Color(0f,1f,0.78f,1f);
            opt_finalDestination.color = new Color(1f,1f,1f,1f);
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
            opt_finalDestination.color = new Color(1f,1f,1f,1f);
            opt_lottery.color = new Color(0f,1f,0.78f,1f);
            opt_collection.color = new Color(1f,1f,1f,1f);
            opt_options.color = new Color(1f,1f,1f,1f);
            opt_quit.color = new Color(1f,1f,1f,1f);
        } else if(menuPosition == 3){
            opt_adventureMode.color =  new Color(1f,1f,1f,1f);
            opt_finalDestination.color = new Color(1f,1f,1f,1f);
            opt_lottery.color = new Color(1f,1f,1f,1f);
            opt_collection.color = new Color(0f,1f,0.78f,1f);
            opt_options.color = new Color(1f,1f,1f,1f);
            opt_quit.color = new Color(1f,1f,1f,1f);
        } else if(menuPosition == 4){
            opt_adventureMode.color =  new Color(1f,1f,1f,1f);
            opt_finalDestination.color = new Color(1f,1f,1f,1f);
            opt_lottery.color = new Color(1f,1f,1f,1f);
            opt_collection.color = new Color(1f,1f,1f,1f);
            opt_options.color = new Color(0f,1f,0.78f,1f);
            opt_quit.color = new Color(1f,1f,1f,1f);
        } else if(menuPosition == 5){
            opt_adventureMode.color =  new Color(1f,1f,1f,1f);
            opt_finalDestination.color = new Color(1f,1f,1f,1f);
            opt_lottery.color = new Color(1f,1f,1f,1f);
            opt_collection.color = new Color(1f,1f,1f,1f);
            opt_options.color = new Color(1f,1f,1f,1f);
            opt_quit.color = new Color(0f,1f,0.78f,1f);
        }

    }

}
