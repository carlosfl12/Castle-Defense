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
    public Vector2 initialPosition;
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
            launchForce += Time.deltaTime * 30f;
            if ((Vector2)transform.position == initialPosition && launchForce > 15) {
                Shoot(launchForce);
                timer = 0;
            }
        }
        if (arrowAmount <= 0) {
            ReloadPosition();
            hasArrows = false;
        }

        if (hasArrows) {
            InitialPosition();
        }
    }

    public void Shoot(float force) {
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(0, 0, Random.Range(-3, 5)), transform.rotation);

        arrow.GetComponent<Rigidbody2D>().velocity = -transform.right * force;
        arrowAmount--;
        launchForce = 0;
    }

    public void InitialPosition() {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);   
    }

    public void ReloadPosition() {
        transform.position = Vector3.MoveTowards(transform.position, quiver.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, quiver.transform.position) < 1f) {
            if (GameManager.sharedInstance.arrows <= 0) {
                return;
            }
            GameManager.sharedInstance.arrows -= 10;
            arrowAmount = 10;
        }
    }
}
