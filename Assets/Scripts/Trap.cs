using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool placed = false;
    public int trapDamage = 5;
    public float timeToActivate = 10f;
    public bool isActive = true;
    public bool hasChildren;
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isActive) {
            return;
        }
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().RecieveDamage(trapDamage);
            isActive = false;
            StartCoroutine(TrapEnabledAgain(timeToActivate));
        }
    }


    IEnumerator TrapEnabledAgain(float time) {
        yield return new WaitForSeconds(time);
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
    }
}
