using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public BagMovement bagMovement;
    public ItemPlacement itemPlacement;
    public int points = 0;
    public bool local;
    public NetworkComponent networkComponent;

    public void Start()
    {
        networkComponent = FindObjectOfType<NetworkComponent>();
    }
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

        NetworkManager.instance.Send(new PointsPacket(NetworkManager.instance.playerData,
                      networkComponent.GameObjectID,
                     points).Serialize());
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

        NetworkManager.instance.Send(new PointsPacket(NetworkManager.instance.playerData,
                      networkComponent.GameObjectID,
                     points).Serialize());

    }

    private void ResetBag()
    {
        bagMovement.ResetToStart();
        itemPlacement.ClearItems();
        StartCoroutine(bagMovement.MoveToEnd());
    }
}