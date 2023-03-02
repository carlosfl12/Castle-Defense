using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public ShotData shotData;
    public int arrows = 30;
    public int gold;
    public int favor;
    public TMP_Text goldText;
    public TMP_Text arrowsText;

    private void Awake() {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
        shotData = ScriptableObject.CreateInstance<ShotData>();
    }

    public void SaveShot(Vector2 mousePosition, Vector2 enemyPosition) {
        shotData.mousePositions.Add(mousePosition);
        shotData.enemiesPosition.Add(enemyPosition);
    }

    private void Start() {
        
    }

    private void Update() {
        goldText.text = gold.ToString();
        arrowsText.text = arrows.ToString();
    }

    public void BuyArrows() {
        if (gold >= 15) {
            gold -= 15;
            arrows += 10;
        } else {
            Debug.Log("Not enough gold");
        }
    }
}
