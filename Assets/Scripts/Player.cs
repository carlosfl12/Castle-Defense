using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 input;
    public Rigidbody2D rb;
    public GameObject arrow;
    public GameObject bow;
    public bool bowlean = true;
    public float chargeForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // Debug.Log(mousePos);

        if (Input.GetButtonDown("Fire1")) {
            bowlean = false;
        }
        if (Input.GetButton("Fire1")) {
            chargeForce += 1000f * Time.deltaTime;
            
        }

        if (Input.GetButtonUp("Fire1") || chargeForce >= 500f  && bowlean == true) {
            Instantiate(arrow, bow.transform.position, transform.rotation);
            arrow.GetComponent<Rigidbody2D>().AddForce(transform.right * chargeForce * Time.deltaTime, ForceMode2D.Impulse);
            chargeForce = 0;
            bowlean = true;
        }
    }
}
