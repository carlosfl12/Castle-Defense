using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public bool isActive = true;
    public List<Transform> buttons = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            if (child.gameObject.CompareTag("Button")) {
                buttons.Add(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            foreach (Transform button in buttons) {
                isActive = !isActive;
                button.gameObject.SetActive(isActive);
            }
        }
    }
}
