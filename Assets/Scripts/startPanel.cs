using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPanel : MonoBehaviour
{
    public static startPanel _instance;

    void Awake() {
        _instance = this;
    }
    
    //click, logo disappears, game start

    public dfTweenVector3 tween;

    public void show() {
        tween.EndValue = new Vector3(1300, 400, 0);
        tween.Play();
    }
    
    public void disap() {
        GetComponent<AudioSource>().Play();
        tween.EndValue = new Vector3(Screen.width + 200, 400, 0);
        tween.Play();
    }

}
