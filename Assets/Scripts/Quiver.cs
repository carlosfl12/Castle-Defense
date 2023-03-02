using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public GameObject[] arrows;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ArrowAmount();   
    }

    void ArrowAmount() {
        if (GameManager.sharedInstance.arrows < 30) {
            arrows[arrows.Length - 1].SetActive(false);
        } else {
            arrows[arrows.Length - 1].SetActive(true);
        }

        if (GameManager.sharedInstance.arrows < 20) {
            arrows[1].SetActive(false);
        } else {
            arrows[1].SetActive(true);
        }

        if (GameManager.sharedInstance.arrows < 10) {
            arrows[0].SetActive(false);
        } else {
            arrows[0].SetActive(true);
        }
    }
}
