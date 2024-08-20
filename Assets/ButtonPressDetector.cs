using UnityEngine;

public class ButtonPressDetector : MonoBehaviour
{
    public bool isPressed = false;

    public void OnButtonPressed()
    {
        isPressed = true;
    }

    public bool IsPressed()
    {
        if (isPressed)
        {
            isPressed = false; // Reset after detection
            return true;
        }
        return false;
    }
}
