using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public int buttonPressedCount = 5;
    public Text undoCountText;

    // Start is called before the first frame update
    void Start()
    {
        undoCountText.text = buttonPressedCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReversingSteps()
    {
        if (buttonPressedCount > 0)
        {
            GameController.instance.canReverse = true;
            GameController.instance.UndoLastStep();
            buttonPressedCount--;
            undoCountText.text = buttonPressedCount.ToString();
        }
    }

    public void OnResetLevel()
    {
        GameController.instance.Reset();
    }
}
