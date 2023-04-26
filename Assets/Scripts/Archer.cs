using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject quiver;
    public float speed = 5f;
    public float timer;
    public float launchForce = 15f;
    public int arrowAmount = 10;
    public Vector2 initialPosition;
    public bool hasArrows;
    public bool hasEnemies;
    public GameObject currentTarget;
    public GameObject arrowsCanvas;
    public bool needArrows = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Arreglar los disparos
        hasEnemies = GameManager.sharedInstance.enemiesList.Count > 0;
        if (hasEnemies) {
            GetTarget();
        } else {
            return;
        }

        timer += Time.deltaTime;

        if (timer > 2 && arrowAmount > 0) {
            hasArrows = true;
            launchForce += Time.deltaTime * 70f;
            if ((Vector2)transform.position == initialPosition && launchForce > 35 && hasEnemies) {
                Shoot(currentTarget);
                timer = 0;
            }
        }
        if (arrowAmount <= 0) {
            ReloadPosition();
            hasArrows = false;
            needArrows = true;
        }

        if (hasArrows) {
            InitialPosition();
            needArrows = false;
        }
        arrowsCanvas.SetActive(needArrows);
    }

    public GameObject GetTarget() {
        foreach (GameObject target in GameManager.sharedInstance.enemiesList) {
            if (currentTarget == null) {
                currentTarget = GameManager.sharedInstance.enemiesList[Random.Range(0, GameManager.sharedInstance.enemiesList.Count - 1)];
            }
        }
        return currentTarget;
    }

    public void Shoot(GameObject target) {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Vector2 shootTarget = (Vector2)target.transform.position;
        Vector2 direction = shootTarget - (Vector2)transform.position;

        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * launchForce;
        arrow.GetComponent<Arrow>().launchForce = launchForce;

        arrowAmount--;
        launchForce = 0;
    }

    public void InitialPosition() {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);   
    }

    public void ReloadPosition() {
        transform.position = Vector3.MoveTowards(transform.position, quiver.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, quiver.transform.position) < 1f) {
            if (GameManager.sharedInstance.arrows <= 0) {
                return;
            }
            GameManager.sharedInstance.arrows -= 10;
            arrowAmount = 10;
        }
    }
}
