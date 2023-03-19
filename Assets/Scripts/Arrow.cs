using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector2 mousePosition;
    Player player;
    public enum ArrowType {
        Normal,
        Fire
    }
    public static ArrowType arrowType;
    public int arrowDamage = 3;
    public bool fireArrow = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (arrowType) {
            case ArrowType.Fire:
                fireArrow = true;
                break;
            case ArrowType.Normal:
                fireArrow = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.name.Contains("2")) {
            if (other.gameObject.CompareTag("Castle")) {
                Destroy(gameObject);
            }
            return;
        }

        if (other.gameObject.CompareTag("Enemy")) {
            if (other.GetComponent<Enemy>().oiled && fireArrow) {
                other.GetComponent<Enemy>().RecieveDamage(45);
            } else {
                if ((arrowDamage * (int)player.launchForce) / 10 < 1) {
                    other.gameObject.GetComponent<Enemy>().RecieveDamage(1);
                } else {
                    other.gameObject.GetComponent<Enemy>().RecieveDamage((arrowDamage * (int)player.launchForce) / 10);
                }
            }
            Destroy(gameObject);
            GameManager.sharedInstance.SaveShot(mousePosition, other.gameObject.transform.position);
        }
        

    }
}
