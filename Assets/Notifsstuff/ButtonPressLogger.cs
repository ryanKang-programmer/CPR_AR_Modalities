using UnityEngine;

public class ButtonPressLogger : MonoBehaviour
{
    public GameObject[] buttons;  // Assign your buttons (cubes) in the Inspector

    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            button.AddComponent<ButtonPressDetect>();  // Add a script to detect the button press
        }
    }

    public void LogButtonPress(GameObject button)
    {
        float timeSinceStart = Time.time;
        Debug.Log($"Button '{button.name}' pressed at {timeSinceStart} seconds.");
    }
}