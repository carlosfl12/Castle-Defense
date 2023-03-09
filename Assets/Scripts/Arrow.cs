using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector2 mousePosition;
    public int arrowDamage = 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (gameObject.name.Contains("2")) {
            if (other.gameObject.CompareTag("Castle")) {
                Destroy(gameObject);
            }
            return;
        }

        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().RecieveDamage(arrowDamage);
            Destroy(gameObject);
            GameManager.sharedInstance.SaveShot(mousePosition, other.gameObject.transform.position);
        }
        

    }
}
