using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 input;
    public float speed = 5f;
    public Vector2 startPos;
    public Rigidbody2D rb;
    public GameObject arrowPrefab;
    public GameObject bow;
    public float chargeForce;
    public Transform shotPoint;
    public float launchForce;
    public int arrowAmount = 30;
    public Vector2 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPosition = bow.transform.position;
        Vector2 direction = mousePos - bowPosition;
        bow.transform.right = -direction;

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
            // SaveData(mousePos);
            Debug.Log(mousePos);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.sharedInstance.arrows += 1000;
            arrowAmount += 1000;
            GameManager.sharedInstance.gold += 1000;
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }
    
    void Shoot() {
        if (arrowAmount <= 0) {
            return;
        }
        GameObject arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = -bow.transform.right * launchForce;
        arrowAmount--;
    }

    void Reload() {
        if (GameManager.sharedInstance.arrows <= 0) {
            Debug.Log("Can't Reload");
        } else {
            arrowAmount += 10;
            GameManager.sharedInstance.arrows -= 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Castle")) {
            transform.position = startPos;
        }

        if (other.gameObject.CompareTag("Quiver")) {
            Reload();
        }
    }


}
