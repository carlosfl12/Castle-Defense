using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    public Vector2 mousePos;
    GameObject currentTrap;
    public int spikeCost = 25;
    public int oilCost = 50;
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
            currentTrap.GetComponent<BoxCollider2D>().enabled = false;
            if (Input.GetMouseButtonDown(0)) {
                currentTrap.GetComponent<Trap>().placed = true;
                currentTrap.GetComponent<BoxCollider2D>().enabled = true;

                if (currentTrap.GetComponent<Trap>().hasChildren) {
                    currentTrap.GetComponent<Trap>().StartCoroutine(currentTrap.GetComponent<Trap>().WaitForOilUse(2f));
                }
            }
        }
    }



    public void PlaceTrap(GameObject trap) {
        if (trap.GetComponent<Trap>().goldCost <= GameManager.sharedInstance.gold) {
            GameObject trapGO = Instantiate(trap, mousePos, transform.rotation);
            currentTrap = trapGO;
            GameManager.sharedInstance.gold -= trap.GetComponent<Trap>().goldCost;
        }
    }

}
