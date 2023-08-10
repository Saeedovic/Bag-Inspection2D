using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NetworkManager;

public class BagMovement : MonoBehaviour
{
    public float speed = 1f;
    public Transform startPosObj;
    public Transform stopPosObj;
    public Transform endPosObj;
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 endPos;
    public bool ran;

    public ButtonActions buttonActions;
    public ItemPlacement itemPlacement;

    NetworkComponent networkComponent;

    public void Start()
    {
        buttonActions = FindObjectOfType<ButtonActions>();
        networkComponent = GetComponent<NetworkComponent>();
        buttonActions.local = true;


        if (networkComponent == null)
        {
            Debug.LogError("Networkcomp is nulll");
            return;
        }

        NetworkManager.instance.OnRecievedBagMovementPacket += RecievedBagMovementPacketEvent;

        startPos = startPosObj.position;
        stopPos = stopPosObj.position;
        endPos = endPosObj.position;
        StartCoroutine(MoveToEnd());

        /* NetworkManager.instance.Send(new BagMovementPacket(
                 NetworkManager.instance.playerData,
                 networkComponent.GameObjectID,
                transform.position).Serialize());*/
    }

    void RecievedBagMovementPacketEvent(string GameObjectID, Vector3 Pos)
    {
        transform.position = Pos;
    }

    public void SendBagMovementPacket()
    {
        if (NetworkManager.instance != null)
        {
            NetworkManager.instance.Send(new BagMovementPacket(
                NetworkManager.instance.playerData,
                networkComponent.GameObjectID,
                transform.position).Serialize());
        }
    }

    public IEnumerator MoveToEnd()
    {
        while (Vector3.Distance(transform.position, stopPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPos, speed * Time.deltaTime);
            SendBagMovementPacket();
            yield return null;
        }

        itemPlacement.PlaceItems();
        print("Movement packet");

        if (buttonActions.local)
        {

            NetworkManager.instance.Send(new BagMovementPacket(
                    NetworkManager.instance.playerData,
                    networkComponent.GameObjectID,
                   transform.position).Serialize());
        }

        buttonActions.local = true;
    }

    public IEnumerator MoveToLast()
    {
        while (Vector3.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            SendBagMovementPacket();
            yield return null;
        }

        ResetToStart();
        itemPlacement.ClearItems();
        StartCoroutine(MoveToEnd());

        NetworkManager.instance.Send(new BagMovementPacket(
               NetworkManager.instance.playerData,
               networkComponent.GameObjectID,
              transform.position).Serialize());
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
