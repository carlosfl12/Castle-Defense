using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] waypoints;
    public float spawnRate = 7f;
    public bool hasRam = false;
    public bool canMove = false;
    public bool canSpawn = false;
    public float timer = 0;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        waypoints = GameObject.FindGameObjectsWithTag("EG Waypoints");
        //Cuando cargue la escena que se ponga el sonido de la corneta
        audioManager.PlayHorn();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.enemiesDefeated <= 25) {
            spawnRate = 2.7f;
        } else if (GameManager.sharedInstance.enemiesDefeated <= 50) {
            spawnRate = 2.5f;
        } else if(GameManager.sharedInstance.enemiesDefeated <= 100) {
            spawnRate = 2f;
        }
        timer += Time.deltaTime;
        if (GameManager.sharedInstance.win) {
            timer = 0;
        }

        if (timer >= spawnRate) {
            timer = 0;
            SpawnEnemy();
        }
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
