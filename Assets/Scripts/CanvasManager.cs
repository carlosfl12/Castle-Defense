using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas[] canvas;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Tab)) {
            isActive = false;
            foreach (Canvas thisCanvas in canvas) {
                thisCanvas.enabled = isActive;
            }
        }
    }

    public void ActiveCanvas(Canvas canvasType) {
        isActive = !isActive;
        foreach (Canvas thisCanvas in canvas) {
            if (thisCanvas == canvasType) {
                thisCanvas.enabled = isActive;
            }
        }
    }
}
