using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    private Camera mainCamera;
    private Quaternion initialCameraRotation;
    private float initialCameraYRotation;

    //vector of all button rotations from initial position
    private Vector3[] button_rotations = new Vector3[]
    {
        new Vector3(0, -90, 0), //N --> not needed
        new Vector3(0, -55, 0), //NE
        new Vector3(0, -150, 0), //NW
        new Vector3(0, 90, 0), //S
        new Vector3(0, 50, 0), //SE
        new Vector3(0, 110, 0), //SW
        new Vector3(0, 0, 0), //E
        new Vector3(0, 180, 0) //W
    };

    // Start is called before the first frame update
    void Start()
    {
        //Record the initial camera rotation
        initialCameraRotation = mainCamera.transform.rotation;
        Debug.Log("Initial Camera Rotation: " + initialCameraRotation.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
