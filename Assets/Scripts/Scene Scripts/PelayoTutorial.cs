using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelayoTutorial : MonoBehaviour
{
    public bool hasCompletedStep;
    public bool goingToNextStep;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCompletedStep && !goingToNextStep) {
            if (!goingToNextStep) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().arrowAmount = 0;
            }
            goingToNextStep = true;
        }
    }
}
