using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number : MonoBehaviour
{
    public int n = 0; //pow(2, n - 1)
    public int i = 0, j = 0;

    public Vector2 posOffset;

    public dfTweenVector2 tweenShow;
    public dfTweenVector2 tweenDisap;
    public dfTweenVector3 tweenMove;

    // initialization
    void Start () {
        posOffset.x = 38;
        posOffset.y = 38;
        initShow();
        initPos();
        tweenMove.TweenCompleted += this.onTweenMoveCompleted;
        tweenDisap.TweenCompleted += this.onTweenDisapCompleted;
        tweenShow.Play();
    }

    bool isDisap = false;
    public void disap() {
        isDisap = true;
    }

    public void onTweenMoveCompleted (dfTweenPlayableBase sender) {
        if (isDisap) tweenDisap.Play();
    }

    public void onTweenDisapCompleted (dfTweenPlayableBase sender) {
        gameController._instance.matrix[i][j]--;
        Destroy(this.gameObject);
    }

    void initShow() {
        int y = 0;
        switch(n) {
            case 1: y = 0; break;
            case 2: y = 725; break;
            case 3: y = 1450; break;
            case 4: y = 2160; break;
            case 5: y = 2850; break;
            case 6: y = 3570; break;
            case 7: y = 4290; break;
            case 8: y = 5000; break;
            case 9: y = 5730; break;
            case 10: y = 6430; break;
            case 11: y = 7150; break;
            case 12: y = 7870; break;
            case 13: y = 8570; break;
            case 14: {
                y = 9300; 
                congrats._instance.maxNum = true;
                break;
            }
        }
        this.GetComponent<dfTiledSprite>().TileScroll = 
            new Vector2(0, 0.0001f * y);
    }

    void initPos() {
        this.GetComponent<dfControl>().RelativePosition = 
            new Vector3(i * 245 +posOffset.x , j * 245 +posOffset.y , 0);
    }

    // move
    public bool moveToPos (int tarI, int tarJ, bool needUpdate = true) {
        bool moved = (i!=tarI) || (j!=tarJ);
        gameController._instance.matrix[i][j]--;
        gameController._instance.matrix[tarI][tarJ]++;
        if (needUpdate) gameController._instance.matrixObject[tarI][tarJ] = this;
        i = tarI;
        j = tarJ;
        tweenMove.EndValue = new Vector3(i * 245 +posOffset.x , j * 245 +posOffset.y , 0);
        tweenMove.Play();
        return moved;
    }

}
