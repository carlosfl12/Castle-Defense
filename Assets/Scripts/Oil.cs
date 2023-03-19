using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    private void Start() {
        StartCoroutine(ChangeToKinematic());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name.StartsWith("B")) {
            other.GetComponent<Enemy>().oiled = true;
        } else {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeToKinematic() {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

    }
}
