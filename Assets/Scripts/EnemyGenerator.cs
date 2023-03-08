using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemies;   
    public float yPosition = -6.25f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 3f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy() {
        int random = Random.Range(0, enemies.Length);

        Instantiate(enemies[random], new Vector3(transform.position.x, yPosition , Random.Range(-3, 5)), transform.rotation);
    }
}
