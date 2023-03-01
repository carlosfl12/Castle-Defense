using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAttacking = false;
    public Transform wallPosition;
    public Castle castle;
    // Start is called before the first frame update
    void Start()
    {
        wallPosition = GameObject.FindGameObjectWithTag("Wall").transform;
        castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, wallPosition.position);
        if (distance > 0.5f) {
            transform.position += transform.right * 5f * Time.deltaTime;
            // Debug.Log(distance);
        } else {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            castle.RecieveDamage(3f);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Castle")) {
            other.gameObject.GetComponent<Castle>().RecieveDamage(3f);
        }

        if (other.gameObject.CompareTag("Arrow")) {
            Destroy(other.gameObject);
            Debug.Log("Muerto");
            Destroy(gameObject);
        }
    }
}
