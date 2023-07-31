using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public BagMovement bagMovement;
    public ItemPlacement itemPlacement;
    public int points = 0;

    public void OnGreenButtonClicked()
    {
        if (itemPlacement.HasIllegalItems())
        {
            points -= 10;
            ResetBag();
        }
        else
        {
            points += 10;
            StartCoroutine(bagMovement.MoveToLast());
        }
    }

    public void OnRedButtonClicked()
    {
        if (itemPlacement.HasIllegalItems())
        {
            points += 10;
        }
        else
        {
            points -= 10;
        }
            ResetBag();
    }

    private void ResetBag()
    {
        bagMovement.ResetToStart();
        itemPlacement.ClearItems();
        StartCoroutine(bagMovement.MoveToEnd());
    }
}