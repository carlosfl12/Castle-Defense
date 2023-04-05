using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public Healthbar healthbar;
    public Sprite[] castleSprites;
    public SpriteRenderer currentSprite;
    public int health;
    public int maxHealth = 999;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecieveDamage(int amount) {
        health -= amount;
        healthbar.SetHealth(health, maxHealth);
        if (health <= 750 && health > 500) {
            currentSprite.sprite = castleSprites[1];
        }
        else if (health <= 500 && health > 250) {
            currentSprite.sprite = castleSprites[2];
        }
        else if (health <= 250 && health > 0) {
            currentSprite.sprite = castleSprites[3];
        }
        else if (health <= 0) {
            currentSprite.sprite = castleSprites[4];
        }
    }

    public void RepairCastle(int amount) {
        if (GameManager.sharedInstance.gold < 20) {
            return;
        }
        
        health += amount;
        
        GameManager.sharedInstance.gold -= 20;
        
        if (health >= maxHealth) {
            health = maxHealth;
        }

        if (health > 750) {
            currentSprite.sprite = castleSprites[0];
        }

        if (health <= 750 && health > 500) {
            currentSprite.sprite = castleSprites[1];
        }
        else if (health <= 500 && health > 250) {
            currentSprite.sprite = castleSprites[2];
        }
        else if (health <= 250 && health > 0) {
            currentSprite.sprite = castleSprites[3];
        }
        else if (health <= 0) {
            currentSprite.sprite = castleSprites[4];
        }

        healthbar.SetHealth(health, maxHealth);

    }


}
