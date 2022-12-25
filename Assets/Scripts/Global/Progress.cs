using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{

    void Awake()
    {
        
        if(!PlayerPrefs.HasKey("continue")){
            Debug.Log("NEW GAME");
            PlayerPrefs.SetInt("stage1_completed", 0);

            PlayerPrefs.SetInt("stage1_completed", 0);
            PlayerPrefs.SetInt("stage2_completed", 0);
            PlayerPrefs.SetInt("stage3_completed", 0);
            PlayerPrefs.SetInt("stage4_completed", 0);
            PlayerPrefs.SetInt("stage5_completed", 0);
            PlayerPrefs.SetInt("stage6_completed", 0);
            PlayerPrefs.SetInt("stage7_completed", 0);
            PlayerPrefs.SetInt("stage8_completed", 0);
            
            PlayerPrefs.SetInt("stage1_nbCoins", 0);
            PlayerPrefs.SetInt("stage2_nbCoins", 0);
            PlayerPrefs.SetInt("stage3_nbCoins", 0);
            PlayerPrefs.SetInt("stage4_nbCoins", 0);
            PlayerPrefs.SetInt("stage5_nbCoins", 0);
            PlayerPrefs.SetInt("stage6_nbCoins", 0);
            PlayerPrefs.SetInt("stage7_nbCoins", 0);
            PlayerPrefs.SetInt("stage8_nbCoins", 0);

            PlayerPrefs.SetInt("lottery1_obtained", 0);
            PlayerPrefs.SetInt("lottery2_obtained", 0);
            PlayerPrefs.SetInt("lottery3_obtained", 0);
            PlayerPrefs.SetInt("lottery4_obtained", 0);
            PlayerPrefs.SetInt("lottery5_obtained", 0);
            PlayerPrefs.SetInt("lottery6_obtained", 0);
            PlayerPrefs.SetInt("lottery7_obtained", 0);
            PlayerPrefs.SetInt("lottery8_obtained", 0);
            PlayerPrefs.SetInt("lottery9_obtained", 0);
            PlayerPrefs.SetInt("lottery10_obtained", 0);
            PlayerPrefs.SetInt("lottery11_obtained", 0);
            PlayerPrefs.SetInt("lottery12_obtained", 0);
            PlayerPrefs.SetInt("lottery13_obtained", 0);
            PlayerPrefs.SetInt("lottery14_obtained", 0);
            PlayerPrefs.SetInt("lottery15_obtained", 0);
            PlayerPrefs.SetInt("lottery16_obtained", 0);
            PlayerPrefs.SetInt("lottery17_obtained", 0);
            PlayerPrefs.SetInt("lottery18_obtained", 0);
            PlayerPrefs.SetInt("lottery19_obtained", 0);
            PlayerPrefs.SetInt("lottery20_obtained", 0);

            PlayerPrefs.SetInt("global_coins", 0);
            PlayerPrefs.SetInt("global_finalDestination_unlocked", 0);

            PlayerPrefs.SetInt("continue", 1);
        } else {
            Debug.Log("CONTINUE");
        }
    }

}
