using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public TMP_Text castleHp;
    public int health;
    public int maxHealth = 999;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        castleHp.text = "Vida: " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecieveDamage(int amount) {
        health -= amount;
        castleHp.text = "Vida: " + health.ToString();
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
        castleHp.text = "Vida: " + health.ToString();
    }


}
