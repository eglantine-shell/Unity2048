using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    public void OnClick (dfControl control, dfMouseEventArgs args) {
        startPanel._instance.disap();
        boardPanel._instance.show();
        gameController._instance.playing = true;
        gameController._instance.Start();

    }
}
