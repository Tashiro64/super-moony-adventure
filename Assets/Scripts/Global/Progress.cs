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
            PlayerPrefs.SetInt("stage2_completed", 0);
            PlayerPrefs.SetInt("stage3_completed", 0);
            PlayerPrefs.SetInt("stage4_completed", 0);
            PlayerPrefs.SetInt("stage5_completed", 0);
            PlayerPrefs.SetInt("stage6_completed", 0);
            PlayerPrefs.SetInt("stage7_completed", 0);
            PlayerPrefs.SetInt("stage8_completed", 0);
            
            PlayerPrefs.SetInt("stage1_wc1", 0);
            PlayerPrefs.SetInt("stage1_wc2", 0);
            PlayerPrefs.SetInt("stage1_wc3", 0);
            PlayerPrefs.SetInt("stage1_pwc", 0);

            PlayerPrefs.SetInt("stage2_wc1", 0);
            PlayerPrefs.SetInt("stage2_wc2", 0);
            PlayerPrefs.SetInt("stage2_wc3", 0);
            PlayerPrefs.SetInt("stage2_pwc", 0);

            PlayerPrefs.SetInt("stage3_wc1", 0);
            PlayerPrefs.SetInt("stage3_wc2", 0);
            PlayerPrefs.SetInt("stage3_wc3", 0);
            PlayerPrefs.SetInt("stage3_pwc", 0);

            PlayerPrefs.SetInt("stage4_wc1", 0);
            PlayerPrefs.SetInt("stage4_wc2", 0);
            PlayerPrefs.SetInt("stage4_wc3", 0);
            PlayerPrefs.SetInt("stage4_pwc", 0);

            PlayerPrefs.SetInt("stage5_wc1", 0);
            PlayerPrefs.SetInt("stage5_wc2", 0);
            PlayerPrefs.SetInt("stage5_wc3", 0);
            PlayerPrefs.SetInt("stage5_pwc", 0);

            PlayerPrefs.SetInt("stage6_wc1", 0);
            PlayerPrefs.SetInt("stage6_wc2", 0);
            PlayerPrefs.SetInt("stage6_wc3", 0);
            PlayerPrefs.SetInt("stage6_pwc", 0);

            PlayerPrefs.SetInt("stage7_wc1", 0);
            PlayerPrefs.SetInt("stage7_wc2", 0);
            PlayerPrefs.SetInt("stage7_wc3", 0);
            PlayerPrefs.SetInt("stage7_pwc", 0);

            PlayerPrefs.SetInt("stage8_wc1", 0);
            PlayerPrefs.SetInt("stage8_wc2", 0);
            PlayerPrefs.SetInt("stage8_wc3", 0);
            PlayerPrefs.SetInt("stage8_pwc", 0);

            PlayerPrefs.SetInt("bonusStage_1_completed", 0);
            PlayerPrefs.SetInt("bonusStage_2_completed", 0);
            PlayerPrefs.SetInt("bonusStage_3_completed", 0);
            PlayerPrefs.SetInt("bonusStage_4_completed", 0);

            PlayerPrefs.SetInt("breakTheTarget_1_completed", 0);
            PlayerPrefs.SetInt("breakTheTarget_2_completed", 0);
            PlayerPrefs.SetInt("breakTheTarget_3_completed", 0);
            PlayerPrefs.SetInt("breakTheTarget_4_completed", 0);
            PlayerPrefs.SetInt("breakTheTarget_5_completed", 0);

            PlayerPrefs.SetInt("boss1_completed", 0);
            PlayerPrefs.SetInt("boss2_completed", 0);
            PlayerPrefs.SetInt("boss3_completed", 0);
            PlayerPrefs.SetInt("boss4_completed", 0);
            PlayerPrefs.SetInt("boss5_completed", 0);

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

            PlayerPrefs.SetInt("death_count", 0);
            PlayerPrefs.SetInt("global_coins", 0);
            PlayerPrefs.SetInt("available_coins", 0);
            PlayerPrefs.SetInt("global_finalDestination_unlocked", 0);
            PlayerPrefs.SetInt("global_finalDestination_completed", 0);
            PlayerPrefs.SetInt("global_breakTheTarget_unlocked", 0);
            PlayerPrefs.SetFloat("global_volume", 1f);

            PlayerPrefs.SetInt("continue", 1);
        } else {
            Debug.Log("CONTINUE");
        }

        //load volume data
        AudioListener.volume = PlayerPrefs.GetFloat("global_volume");

    }


}
