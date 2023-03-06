using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAttacking = false;
    public float damage;
    public GameObject arrowPrefab;
    public GameObject[] waypoints;
    public Transform waypoint;
    public Castle castle;
    private GameObject shotPosition;
    private float timeToShot;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject stopPoint in waypoints) {
            if (stopPoint.name.StartsWith(gameObject.name[0])) {
                waypoint = stopPoint.transform;
            }
        }
        castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, waypoint.position);
        if (distance > 0.5f) {
            transform.position += transform.right * 5f * Time.deltaTime;
        } else {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            Attack();
        }
    }

    // private void OnTriggerStay2D(Collider2D other) {
    //     if (other.gameObject.CompareTag("Castle")) {
    //         other.gameObject.GetComponent<Castle>().RecieveDamage(3f);
    //     }
    // }


    void Attack() {
        if (isAttacking) {
            castle.RecieveDamage(damage);

        }
        if (gameObject.name.StartsWith("A")) {
            Shoot();
        }
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking() {
        isAttacking = false;
        yield return new WaitForSeconds(1.5f);
        isAttacking = true;
    }

    void Shoot() {
        if (!isAttacking) {
            return;
        }
        shotPosition = transform.GetChild(0).gameObject;
        // transform.LookAt(shotPosition.transform.position);
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(1,1) * 10;
    }
}
