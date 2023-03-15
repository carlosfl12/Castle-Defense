using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] waypoints;
    public float spawnRate = 10f;
    public bool hasRam = false;
    public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("EG Waypoints");
        if (hasRam) {
            InvokeRepeating("SpawnEnemy", 1f, spawnRate * 3);   
        } else {
            InvokeRepeating("SpawnEnemy", 1f, spawnRate);   
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChangePosition();
    }

    void SpawnEnemy() {
        int random = Random.Range(0, enemies.Length);

        foreach (GameObject ram in GameManager.sharedInstance.enemiesList) {
            if (ram.name.StartsWith("B")) {
                hasRam = true;
                random = Random.Range(0, enemies.Length - 1);
            } else {
                hasRam = false;
            }
        }
        GameObject enemy = Instantiate(enemies[random], new Vector2(transform.position.x, transform.position.y), transform.rotation);
        enemy.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y - 1) * -1;
        GameManager.sharedInstance.AddEnemy(enemy);
        canMove = false;
    }

    void ChangePosition() {
        int randomPosition = Random.Range(0, waypoints.Length);

        transform.position = waypoints[randomPosition].transform.position;
    }
}
