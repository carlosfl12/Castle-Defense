using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BuyTutorial : MonoBehaviour
{
    public PelayoTutorial pelayo;
    public Player player;
    public TMP_Text secondText;
    public TMP_Text thirdText;
    public bool hasCompletedStep;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.arrowAmount <= 0) {
            secondText.enabled = true;
        }
        if (pelayo.goingToNextStep && !hasCompletedStep && player.arrowAmount > 0) {
            hasCompletedStep = true;
            secondText.enabled = false;
            thirdText.enabled = true;
        }
        if (pelayo.hasCompletedStep && hasCompletedStep) {
            Debug.Log("Carga la partida");
        }
    }
}
