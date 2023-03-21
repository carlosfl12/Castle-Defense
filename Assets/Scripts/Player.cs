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
        ChangeArrows();
        Vector2 bowPosition = bow.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - bowPosition;
        bow.transform.right = -direction;
        
        if (launchForce < 1.5f) {
            return;
        }
        if (Input.GetMouseButton(0)) {
            launchForce += Time.deltaTime * 30f;
        }
        if (launchForce >= 0 && Input.GetMouseButtonUp(0) && !GameManager.sharedInstance.isCanvasActive) {
            Shoot(launchForce);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.sharedInstance.arrows += 1000;
            arrowAmount += 1000;
            GameManager.sharedInstance.gold += 1000;
        }
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0));
    }
    
    void Shoot(float force) {
        if (arrowAmount <= 0) {
            return;
        }
        GameObject arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = -bow.transform.right * force;
        arrow.GetComponent<Arrow>().launchForce = launchForce;

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

    void ChangeArrows() {
        if (Input.mouseScrollDelta.y > 0) {
            Arrow.arrowType = Arrow.ArrowType.Fire;
        }
        if (Input.mouseScrollDelta.y < 0) {
            Arrow.arrowType = Arrow.ArrowType.Normal;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
    
        if (other.gameObject.CompareTag("Quiver")) {
            Reload();
        }
    }


}
