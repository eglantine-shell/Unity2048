using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum touchDir {
    invalid,
    left,
    right,
    up,
    down
}

public class gameController : MonoBehaviour
{
    public static gameController _instance;
    public GameObject numberPrefab;
    public bool playing = false;

    void Awake() {
        _instance = this;
    }

    //matrix

    public int[][] matrix = new int[4][] {
        new int[4]{0, 0, 0, 0},
        new int[4]{0, 0, 0, 0},
        new int[4]{0, 0, 0, 0},
        new int[4]{0, 0, 0, 0}
    };

    public number[][] matrixObject = new number[4][] {
        new number[4]{null, null, null, null},
        new number[4]{null, null, null, null},
        new number[4]{null, null, null, null},
        new number[4]{null, null, null, null}
    };
    
    // Start is called before the first frame update
    public void Start()
    {
        if (playing == false) return;
        newNumber();
        newNumber();
        newNumber();
        newNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing) moveNumber();
    }

    //new number: random / after combination
    void newNumber (int x = -1, int y = -1, int newN = 1) {
        int newI = x, newJ = y;
        if (x < 0 || y < 0) {
            int count = checkNull();
            if (count == 0) return;
            int ran = Random.RandomRange(1, count);
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (matrix[i][j] == 0) {
                        ran--;
                        if (ran == 0) {
                            newI = i;
                            newJ = j;
                            goto flag;
                        }
                    }
                }
            }
        }
        flag:
        dfControl numControl = this.GetComponent<dfControl>().AddPrefab(numberPrefab);
        number newNum = numControl.GetComponent<number>();
        newNum.n = newN;
        newNum.i = newI;
        newNum.j = newJ;
        matrix[newI][newJ]++;
        matrixObject[newI][newJ] = newNum;

    }

    //move
    Vector3 mouseDownPos;
    touchDir getDir() {
        if (Input.GetMouseButtonUp(0)) {
            Vector3 touchOffset = Input.mousePosition - mouseDownPos;
            if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) &&
                Mathf.Abs(touchOffset.x) > 150){
                    if (touchOffset.x > 0) return touchDir.right;
                    else return touchDir.left;
                }
            else if (Mathf.Abs(touchOffset.y) > 150 && touchOffset.y > 0) return touchDir.up;
            else return touchDir.down;
        }
        return touchDir.invalid;
    }

    void moveNumber () {
        bool isMove = false;
        int countCombine = 0;
        if (Input.GetMouseButtonDown(0)) mouseDownPos = Input.mousePosition;
        if (Input.GetMouseButtonUp(0) == false) return;
        touchDir dir = getDir();
        if (dir == touchDir.invalid) return;
        switch (dir) {
            case touchDir.right: {
                for (int j = 0; j < 4; j++) {
                    number pre = null;
                    int cur = 4;
                    for (int i = 3; i >= 0; i--) {
                        bool needUpdate = true;
                        if (matrix[i][j] == 0) continue;
                        if (pre != null && pre.n == matrixObject[i][j].n && pre.n != 14) {
                            countCombine++;
                            newNumber(cur, j, pre.n + 1);
                            boardPanel._instance.addScore((int)Mathf.Pow(2, pre.n + 1));
                            pre.disap();
                            matrixObject[i][j].disap();
                            pre = null;
                            needUpdate = false;
                        }
                        else {
                            pre = matrixObject[i][j];
                            cur--;
                        }
                        if (matrixObject[i][j].moveToPos(cur, j, needUpdate)) isMove = true;
                    }
                }
                break;
            }
            case touchDir.left: {
                for (int j = 0; j < 4; j++) {
                    number pre = null;
                    int cur = -1;
                    for (int i = 0; i < 4; i++) {
                        bool needUpdate = true;
                        if (matrix[i][j] == 0) continue;
                        if (pre != null && pre.n == matrixObject[i][j].n && pre.n != 14) {
                            countCombine++;
                            newNumber(cur, j, pre.n + 1);
                            boardPanel._instance.addScore((int)Mathf.Pow(2, pre.n + 1));
                            pre.disap();
                            matrixObject[i][j].disap();
                            pre = null;
                            needUpdate = false;
                        }
                        else {
                            pre = matrixObject[i][j];
                            cur++;
                        }
                        if (matrixObject[i][j].moveToPos(cur, j, needUpdate)) isMove = true;
                    }
                }
                break;
            }
            case touchDir.up: {
                for (int i = 0; i < 4; i++) {
                    number pre = null;
                    int cur = -1;
                    for (int j = 0; j < 4; j++) {
                        bool needUpdate = true;
                        if (matrix[i][j] == 0) continue;
                        if (pre != null && pre.n == matrixObject[i][j].n && pre.n != 14) {
                            countCombine++;
                            newNumber(i, cur, pre.n + 1);
                            boardPanel._instance.addScore((int)Mathf.Pow(2, pre.n + 1));
                            pre.disap();
                            matrixObject[i][j].disap();
                            pre = null;
                            needUpdate = false;
                        }
                        else {
                            pre = matrixObject[i][j];
                            cur++;
                        }
                        if (matrixObject[i][j].moveToPos(i, cur, needUpdate)) isMove = true;
                    }
                }
                break;
            }
            case touchDir.down: {
                for (int i = 0; i < 4; i++) {
                    number pre = null;
                    int cur = 4;
                    for (int j = 3; j >= 0; j--) {
                        bool needUpdate = true;
                        if (matrix[i][j] == 0) continue;
                        if (pre != null && pre.n == matrixObject[i][j].n && pre.n != 14) {
                            countCombine++;
                            newNumber(i, cur, pre.n + 1);
                            boardPanel._instance.addScore((int)Mathf.Pow(2, pre.n + 1));
                            pre.disap();
                            matrixObject[i][j].disap();
                            pre = null;
                            needUpdate = false;
                        }
                        else {
                            pre = matrixObject[i][j];
                            cur--;
                        }
                        if (matrixObject[i][j].moveToPos(i, cur, needUpdate)) isMove = true;
                    }
                }
                break;
            }
        }
        if (isMove) newNumber();
        if (isGameOver()) {
            playing = false;
            boardPanel._instance.disap();
            endPanel._instance.show();
        }
    }

    //check if game over
    int checkNull() {
        int countNull = 0;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                if (matrix[i][j] == 0) countNull++;
            }
        }
        return countNull;
    }

    bool isGameOver() {
        if (checkNull() > 0) return false;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 3; j++) {
                if (matrixObject[i][j].n == matrixObject[i][j+1].n) return false;
            }
        }
        for (int j = 0; j < 4; j++) {
            for (int i = 0; i < 3; i++) {
                if (matrixObject[i][j].n == matrixObject[i+1][j].n) return false;
            }
        }
        return true;
    }

}
