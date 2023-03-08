using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject quiver;
    public float speed = 5f;
    public float timer;
    public float launchForce = 15f;
    public int arrowAmount = 10;
    public Vector3 initialPosition;
    public bool hasArrows;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2 && arrowAmount > 0) {
            hasArrows = true;
            timer = 0;
            Shoot();
        }
        if (arrowAmount <= 0) {
            ReloadPosition();
            hasArrows = false;
        }

        if (hasArrows) {
            InitialPosition();
        }
    }

    public void Shoot() {
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(0, 0, Random.Range(-3, 5)), transform.rotation);

        arrow.GetComponent<Rigidbody2D>().velocity = -transform.right * launchForce;
        arrowAmount--;
    }

    public void InitialPosition() {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);   
    }

    public void ReloadPosition() {
        transform.position = Vector3.MoveTowards(transform.position, quiver.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, quiver.transform.position) < 1f) {
            GameManager.sharedInstance.arrows -= 10;
            arrowAmount = 10;
        }
    }
}
