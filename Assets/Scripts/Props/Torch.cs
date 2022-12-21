using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{

    public Light2D Light;

    void Start()
    {
        Light = GetComponent<Light2D>();
        StartCoroutine(TorchFire());
    }

    IEnumerator TorchFire(){

        while(true){
            Light.intensity = 0.95f;
            yield return new WaitForSeconds(0.03f);
            Light.intensity = 1.05f;
            yield return new WaitForSeconds(0.05f);
            Light.intensity = 0.98f;
            yield return new WaitForSeconds(0.02f);
            Light.intensity = 0.87f;
            yield return new WaitForSeconds(0.08f);
            Light.intensity = 1.00f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
