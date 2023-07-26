using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string ID { get; private set; }
    public string Name { get; private set; }

    public PlayerData(string ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}