using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponent : MonoBehaviour
{
    public string OwnerID;
    public string GameObjectID;

    public bool isLocal()
    {
        return NetworkManager.instance.playerData.ID == OwnerID;
    }

    public void GenerateGameObjectIDToSelf()
    {
        GameObjectID = Random.Range(0, 1000000).ToString();
    }
}
