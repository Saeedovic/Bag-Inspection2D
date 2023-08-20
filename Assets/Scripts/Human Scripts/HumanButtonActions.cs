using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HumanButtonActions : MonoBehaviour
{
    public HumanMovement humanMovement;
    public HumanItemPlacement humanItemPlacement;
    public int humanPoints = 0;
    public bool local;
    public void OnGreenButtonClicked()
    {
        if (humanItemPlacement.HasIllegalItems())
        {
            humanPoints -= 10;
            ResetBag();
        }
        else
        {
            humanPoints += 10;
            StartCoroutine(humanMovement.MoveToLast());
        }

    }

    public void OnRedButtonClicked()
    {
        if (humanItemPlacement.HasIllegalItems())
        {
            humanPoints += 10;
        }
        else
        {
            humanPoints -= 10;
        }
        ResetBag();

    }

    private void ResetBag()
    {
        humanMovement.ResetToStart();
        humanItemPlacement.ClearItems();
        StartCoroutine(humanMovement.MoveToStop());
    }
}