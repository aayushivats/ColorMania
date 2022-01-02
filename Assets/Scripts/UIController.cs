using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    int buttonPressedCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReversingSteps()
    {
        if (buttonPressedCount < 5)
        {
            GameController.instance.canReverse = true;
            GameController.instance.UndoLastStep();
            buttonPressedCount++;
        }
    }

    public void OnResetLevel()
    {
        GameController.instance.Reset();
    }
}
