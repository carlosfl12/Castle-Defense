using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
            Debug.Log("Muerto");
            Destroy(gameObject);
            GameManager.sharedInstance.gold += 3;
            GameManager.sharedInstance.SaveShot(mousePosition, other.gameObject.transform.position);
        }
        

    }
}
