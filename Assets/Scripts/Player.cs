using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 input;
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
            SaveData(mousePos);
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 5f * Time.deltaTime, 0, 0));
    }

    public void SaveData(Vector2 mousePosition) {
        Debug.Log("Mouse position: " + mousePosition);
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Debug.Log("Enemy position " + enemy.transform.position);
        }
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
