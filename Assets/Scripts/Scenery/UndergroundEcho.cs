using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UndergroundEcho : MonoBehaviour
{

    public bool makeEcho = false;
    public AudioMixer audioMixer;
    public AudioMixerGroup exposedParameter;

    public float echo = 0f;
    public float lowpass = 22000f;

    void Start()
    {
        exposedParameter = audioMixer.FindMatchingGroups("Master")[0];
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.name == "CenterCheck"){
            makeEcho = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.name == "CenterCheck"){
            makeEcho = false;
        }
    }

    void FixedUpdate(){
        if(makeEcho){
            if(echo < 0.55f){
                echo += 0.05f;
                exposedParameter.audioMixer.SetFloat("Echo Wetmix", echo);
            }
            if(lowpass > 8000){
                lowpass -= 1000f;
                exposedParameter.audioMixer.SetFloat("LowPass Cutoff", lowpass);
            }
        } else {
            if(echo >= 0.05f){
                echo -= 0.05f;
                exposedParameter.audioMixer.SetFloat("Echo Wetmix", echo);
                exposedParameter.audioMixer.SetFloat("LowPass Cutoff", echo);
            }
            if(lowpass < 22000f){
                lowpass += 500f;
                if(lowpass > 22000f){ lowpass = 22000f; }
                exposedParameter.audioMixer.SetFloat("LowPass Cutoff", lowpass);
            }
        }
    }

}
