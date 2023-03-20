using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPot : MonoBehaviour
{
    public int currentGold;
    public Sprite[] potSprites;
    public SpriteRenderer potSprite;

    private void Start() {
        potSprite = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate() {
        currentGold = GameManager.sharedInstance.gold;
        ChangePotSprite();
    }

    void ChangePotSprite() {
        if (currentGold > 100) {
            potSprite.sprite = potSprites[potSprites.Length - 1];
        }
        if (currentGold > 50 && currentGold <= 100 ) {
            potSprite.sprite = potSprites[potSprites.Length - 2];
        }
        if (currentGold > 25 && currentGold <= 50) {
            potSprite.sprite = potSprites[potSprites.Length - 3];
        }
        if (currentGold >= 0 && currentGold <= 25) {
            potSprite.sprite = potSprites[0];
        }
    }
}
