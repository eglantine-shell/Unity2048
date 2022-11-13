using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPanel : MonoBehaviour
{
    public static endPanel _instance;

    void Awake() {
        _instance = this;
    }
    
    //game over
    public dfTweenVector3 tween;
    public dfLabel score;

    public void show() {
        dfControl control = this.GetComponent<dfControl>();
        tween.EndValue = new Vector3(1115, 203, 0);
        tween.Play();
        score.Text = boardPanel._instance.score + "";
    }

    public void disap() {
        GetComponent<AudioSource>().Play();
        tween.EndValue = new Vector3(Screen.width, 203, 0);
        tween.Play();
    }


}
