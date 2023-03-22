using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 startPos;
    public Rigidbody2D rb;
    public GameObject arrowPrefab;
    public GameObject bow;
    public Transform[] bowPositions;
    public Transform shotPoint;
    public float launchForce;
    public float maxLaunchForce = 75f;
    public int arrowAmount = 30;
    public Vector2 mousePos;
    public Vector2 input;

    public SpriteRenderer pelayo;
    public Sprite[] sprites;
    public enum State {
        Moving,
        Shooting
    }
    public State state;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        switch (state) {
            case State.Moving:
                if (input.x < 0) {
                    pelayo.flipX = false;
                    bow.transform.position = bowPositions[0].position;
                    ChangeSprite(sprites[0]);
                } else {
                    bow.transform.position = bowPositions[1].position;
                    pelayo.flipX = true;
                    ChangeSprite(sprites[0]);
                }
                if (input.y < 0 && input.x == 0) {
                    bow.transform.position = bowPositions[2].position;
                    ChangeSprite(sprites[2]);
                } else if (input.y > 0 && input.x == 0){
                    bow.transform.position = bowPositions[3].position;
                    ChangeSprite(sprites[3]);
                }
                break;
            case State.Shooting:
                pelayo.flipX = false;
                bow.transform.position = bowPositions[0].position;
                ChangeSprite(sprites[1]);
                break;
        }
        ChangeArrows();
        Vector2 bowPosition = bow.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - bowPosition;
        bow.transform.right = -direction;
        
        
        if (Input.GetMouseButton(0)) {
            launchForce += Time.deltaTime * 30f;
            state = State.Shooting;
        }
        if (launchForce > maxLaunchForce) {
            launchForce = maxLaunchForce;
        }
        if (launchForce >= 0 && Input.GetMouseButtonUp(0) && !GameManager.sharedInstance.isCanvasActive) {
            Shoot(launchForce);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            GameManager.sharedInstance.arrows += 1000;
            arrowAmount += 1000;
            GameManager.sharedInstance.gold += 1000;
        }
        if (input != Vector2.zero) {
            state = State.Moving;
        }
        transform.Translate(input * speed * Time.deltaTime);
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

    void ChangeSprite(Sprite spriteTo) {
        pelayo.sprite = spriteTo;
    }

    private void OnTriggerEnter2D(Collider2D other) {
    
        if (other.gameObject.CompareTag("Quiver")) {
            Reload();
        }
    }


}
