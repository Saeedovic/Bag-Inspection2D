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
            itemPlacement.PlaceItems();
        }
        else
        {
            points += 10;
            ResetBag();
            itemPlacement.PlaceItems();
        }
    }

    public void OnRedButtonClicked()
    {
        if (itemPlacement.HasIllegalItems()) 
        {
            points += 10;
            ResetBag();
            itemPlacement.PlaceItems();
        }
        else
        {
            
            points -= 10; 
            ResetBag();
            itemPlacement.PlaceItems();
        }
    }

    private void ResetBag()
    {
        bagMovement.ResetToStart(); 
        itemPlacement.ClearItems(); 
    }
}