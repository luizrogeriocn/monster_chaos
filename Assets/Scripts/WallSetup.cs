using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSetup : MonoBehaviour
{
    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject wallDown;
    public GameObject wallTop;

    // Start is called before the first frame update
    void Start()
    {
        float wallThickness = 0.5f;
        Vector3 worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector3 wallRightPosition = wallRight.transform.position;
        wallRightPosition.x = worldDimensions.x + wallThickness;
        wallRight.transform.position = wallRightPosition;

        Vector3 wallLeftPosition = wallLeft.transform.position;
        wallLeftPosition.x = -worldDimensions.x - wallThickness;
        wallLeft.transform.position = wallLeftPosition;

        Vector3 wallTopPosition = wallTop.transform.position;
        wallTopPosition.y = worldDimensions.y + wallThickness;
        wallTop.transform.position = wallTopPosition;

        Vector3 wallDownPosition = wallDown.transform.position;
        wallDownPosition.y = -worldDimensions.y - wallThickness;
        wallDown.transform.position = wallDownPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
