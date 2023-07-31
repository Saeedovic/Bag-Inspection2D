using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanButtonActions : MonoBehaviour
{
    public HumanMovement humanMovement; 
    public HumanItemPlacement humanItemPlacement; 
    public int points = 0;

    public void OnGreenButtonClicked()
    {
        if (humanItemPlacement.HasIllegalItems())
        {
            points -= 10;
            StartCoroutine(humanMovement.MoveToLast());
        }
        else
        {
            points += 10;
            StartCoroutine(humanMovement.MoveToLast());
        }
    }

    public void OnRedButtonClicked()
    {
        if (humanItemPlacement.HasIllegalItems())
        {
            points += 10;    
        }
        else
        {
            points -= 10;         
        }

        StartCoroutine(humanMovement.MoveToStart());
    }

    private void ResetHuman()
    {
        humanMovement.ResetToStart();
        humanItemPlacement.ClearItems();
        StartCoroutine(humanMovement.MoveToStop());
    }
}
