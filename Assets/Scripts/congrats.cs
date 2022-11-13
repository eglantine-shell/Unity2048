using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congrats : MonoBehaviour
{
    public static congrats _instance;

    void Awake() {
        _instance = this;
    }
    
    public dfTweenVector3 tween;
    public bool maxNum = false;

    public void show() {
        if (maxNum == false) return; 
        tween.EndValue = new Vector3(1130, 450, 0);
        tween.Play();
    }
    
    public void disap() {
        GetComponent<AudioSource>().Play();
        tween.EndValue = new Vector3(Screen.width + 200, 450, 0);
        tween.Play();
    }
}
