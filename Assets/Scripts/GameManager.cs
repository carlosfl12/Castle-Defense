using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public int arrows = 30;
    public int gold;
    public int favor;

    private void Awake() {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
    }
}
