using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardPanel : MonoBehaviour
{
    public static boardPanel _instance;

    void Awake() {
        _instance = this;
    }

    // score start = 0
    public dfLabel socreCur;
    public int score = 0;

    void Start() {
        updateScore();
    }

    public void updateScore() {
        socreCur.Text = score + "";
    }

    public void addScore(int score) {
        this.score += score;
        updateScore();
        // updateHighest();
    }

    /*public bool updateHighest() {
        float storeHighest = PlayerPrefs.GetFloat("scoreHighest", 0);
        if (score > storeHighest) {
            PlayerPrefs.SetFloat("scoreHighest", score);
            return true;
        }
        return false;
    }*/

    public dfTweenVector3 tween;

    // game start, score board shows
    public void show() {
        tween.EndValue = new Vector3(1300, 350, 0);
        tween.Play();
    }

    //game over, score board disappears
    public void disap() {
        GetComponent<AudioSource>().Play();
        tween.EndValue = new Vector3(Screen.width, 350, 0);
        tween.Play();
    }

}
