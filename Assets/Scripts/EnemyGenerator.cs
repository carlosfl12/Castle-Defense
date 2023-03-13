using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemies;   
    public float yPosition = -6.25f;
    public bool hasRam = false;
    // Start is called before the first frame update
    void Start()
    {
        if (hasRam) {
            InvokeRepeating("SpawnEnemy", 1f, 3f * 3);   
        } else {
            InvokeRepeating("SpawnEnemy", 1f, 3f);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy() {
        int random = Random.Range(0, enemies.Length);

        foreach (GameObject ram in GameManager.sharedInstance.enemiesList) {
            Debug.Log(ram);
            if (ram.name.StartsWith("B")) {
                hasRam = true;
                random = Random.Range(0, enemies.Length - 1);
            } else {
                hasRam = false;
            }
        }
        GameObject enemy = Instantiate(enemies[random], new Vector3(transform.position.x, yPosition , Random.Range(-3, 5)), transform.rotation);

        GameManager.sharedInstance.AddEnemy(enemy);
    }
}
