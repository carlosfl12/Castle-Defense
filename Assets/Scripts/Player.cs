using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 input;
    public Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // input = new Vector2(Input.GetAxis("Horizontal"), 0);

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     input.y += jumpForce;
        // }

        // transform.position += new Vector3(input.x, input.y, 0) * speed * Time.deltaTime;
    }
}
