using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string itemName; 
    public Vector3 position;
    

    public ItemData(string itemName, Vector3 position)
    {
        this.itemName = itemName;
        this.position = position;    
    }
}
