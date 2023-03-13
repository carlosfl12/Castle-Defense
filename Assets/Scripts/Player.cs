using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 input;
    public float speed = 5f;
    public Vector2 startPos;
    public Rigidbody rb;
    public GameObject arrowPrefab;
    public GameObject bow;
    public float chargeForce;
    public Transform shotPoint;
    public float launchForce;
    public int arrowAmount = 30;
    public Vector2 mousePos;
    public Vector3 screenPosition, worldPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        

        if (Input.GetMouseButton(0)) {
            launchForce += Time.deltaTime * 30f;
        }
        if (launchForce >= 0 && Input.GetMouseButtonUp(0)) {
            Shoot(launchForce);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.sharedInstance.arrows += 1000;
            arrowAmount += 1000;
            GameManager.sharedInstance.gold += 1000;
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }
    
    void Shoot(float force) {
        if (arrowAmount <= 0) {
            return;
        }
        GameObject arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        arrow.GetComponent<Rigidbody>().velocity = -bow.transform.right * force;
        arrowAmount--;
        launchForce = 0;

    }

    void Reload() {
        if (GameManager.sharedInstance.arrows <= 0) {
            Debug.Log("Can't Reload");
        } else {
            arrowAmount += 10;
            GameManager.sharedInstance.arrows -= 10;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Castle")) {
            transform.position = startPos;
        }
    
        if (other.gameObject.CompareTag("Quiver")) {
            Reload();
        }
    }


}
