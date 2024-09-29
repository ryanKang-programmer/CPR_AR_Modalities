using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressDetect : MonoBehaviour
{
    private ButtonPressLogger logger;

    private void Start()
    {
        // Find the logger script in the scene
        logger = FindObjectOfType<ButtonPressLogger>();
    }

    private void OnMouseDown()
    {
        // Log the button press when this button is clicked
        logger.LogButtonPress(gameObject);
    }
}
