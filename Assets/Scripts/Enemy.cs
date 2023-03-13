using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
    public int maxHealth;
    public int goldToGive;
    public float speed = 5;
    public GameObject arrowPrefab;
    public GameObject[] waypoints;
    public Transform waypoint;
    public Castle castle;
    private GameObject shotPosition;
    public Healthbar healthbar;
    private float timeToShot;
    private EnemyGenerator enemyGenerator;
    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = GameObject.FindGameObjectWithTag("EnemyGenerator").GetComponent<EnemyGenerator>();
        health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject stopPoint in waypoints) {
            if (stopPoint.name.StartsWith(gameObject.name[0]) && transform.position.z == stopPoint.transform.position.z) {
                waypoint = stopPoint.transform;
            }
        }
        castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToShot += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, waypoint.position);
        if (distance > 0.5f) {
            transform.position += transform.right * speed * Time.deltaTime;
        } else {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            if (timeToShot > 2) {
                timeToShot = 0;
                Attack();
            }
        }
    }


    void Attack() {
        if (gameObject.name.StartsWith("A")) {
            Shoot();
        }
        castle.RecieveDamage(damage);
    }

    void Shoot() {
        // transform.LookAt(shotPosition.transform.position);
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(1,1) * 10;
    }

    public void RecieveDamage(int amount) {
        health -= amount;
        healthbar.SetHealth(health, maxHealth);
        if (health <= 0) {
            GameManager.sharedInstance.gold += goldToGive;
            GameManager.sharedInstance.RemoveEnemy(gameObject);
            if (gameObject.name.StartsWith("B")) {
                enemyGenerator.hasRam = false;
            }
            Destroy(gameObject);
        }
    }
}
