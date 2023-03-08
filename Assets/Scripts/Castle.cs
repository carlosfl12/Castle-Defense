using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public TMP_Text castleHp;
    public float health;
    public float maxHealth = 999;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        castleHp.text = "Health: " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecieveDamage(int amount) {
        health -= amount;
        castleHp.text = "Health: " + health.ToString();
    }


}
