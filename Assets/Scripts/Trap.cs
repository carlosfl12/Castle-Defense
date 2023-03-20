using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool placed = false;
    public float trapSlow = 1.5f;
    public float timeToActivate = 10f;
    public bool isActive = true;
    public bool hasChildren;
    public int uses = 3;
    public int goldCost;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        hasChildren = transform.childCount > 0;

        if (hasChildren) {
            foreach (Transform child in transform) {
                child.GetComponent<Rigidbody2D>().isKinematic = true;
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uses <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive) {
            return;
        }
        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.name.StartsWith("B")) {
            other.GetComponent<Enemy>().speed -= trapSlow;
            isActive = false;
            StartCoroutine(TrapEnabledAgain(timeToActivate));
        } else if (other.gameObject.CompareTag("Enemy") && other.gameObject.name.StartsWith("B")) {
            Destroy(gameObject);
        }
    }


    IEnumerator TrapEnabledAgain(float time) {
        yield return new WaitForSeconds(time);
        uses--;
        isActive = true;
    }

    public IEnumerator WaitForOilUse(float time) {
        yield return new WaitForSeconds(time);
        transform.Rotate(new Vector3(0, 0, 90));
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
            if (!child.CompareTag("Trap")) {
                child.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
        yield return new WaitForSeconds(10f);
        uses--;
    }
}
