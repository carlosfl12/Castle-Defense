using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public Healthbar healthbar;
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

        healthbar.SetHealth(health, maxHealth);

    }


}
