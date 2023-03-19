using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    public Vector2 mousePos;
    GameObject currentTrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentTrap == null) {
            return;
        }
        if (!currentTrap.GetComponent<Trap>().placed) {
            currentTrap.transform.position = new Vector2((int)mousePos.x, (int)mousePos.y);
            if (Input.GetMouseButtonDown(0)) {
                currentTrap.GetComponent<Trap>().placed = true;
                if (currentTrap.GetComponent<Trap>().hasChildren) {
                    currentTrap.GetComponent<Trap>().StartCoroutine(currentTrap.GetComponent<Trap>().WaitForOilUse(2f));
                }
            }
        }
    }



    public void PlaceTrap(GameObject trap) {
        GameObject trapGO = Instantiate(trap, mousePos, transform.rotation);
        currentTrap = trapGO;
    }

}
