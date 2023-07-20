using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    public ButtonActions buttonHandler;
    private Text pointsText;

    private void Start()
    {
        pointsText = GetComponent<Text>();
    }

    private void Update()
    {
        pointsText.text = "Points: " + buttonHandler.points;
    }
}
