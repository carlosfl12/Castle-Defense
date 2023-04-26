using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyTutorial : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public PelayoTutorial pelayoTutorial;
    public GameObject panelTutorial;
    public TMP_Text firstText;
    // Start is called before the first frame update
    void Start()
    {
        pelayoTutorial = GameObject.FindGameObjectWithTag("Player").GetComponent<PelayoTutorial>();
    }

    // Update is called once per frame
    void Update()
    {
      if (enemies.Count == 0) {
        pelayoTutorial.hasCompletedStep = true;
        firstText.enabled = false;
        panelTutorial.GetComponent<Image>().enabled = false;
      }
      foreach (GameObject enemy in enemies) {
        if (enemy == null) {
            enemies.Remove(enemy);
        }
      }  
    }

}
