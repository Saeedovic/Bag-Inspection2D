using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NetworkManager;

public class HumanMovement : MonoBehaviour
{
    public float speed = 1f;
    public Transform startPosObj;
    public Transform stopPosObj;
    public Transform endPosObj;
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 endPos;
    public int random;
    public bool ran;

    public HumanButtonActions humanButtonActions;
    public HumanItemPlacement humanItemPlacement;

    NetworkComponent networkComponent;

    public void Start()
    {
        humanButtonActions = FindObjectOfType<HumanButtonActions>();
        networkComponent = GetComponent<NetworkComponent>();
        humanItemPlacement = FindObjectOfType<HumanItemPlacement>();
        humanButtonActions.local = true;

        //networkComponent.GenerateGameObjectIDToSelf();
        networkComponent.GameObjectID = "1";
        networkComponent.OwnerID = "2";


        if (networkComponent == null)
        {
            return;
        }

        NetworkManager.instance.OnRecievedHumanMovementPacketEvent += RecievedHumanMovementPacketEvent;

        startPos = startPosObj.position;
        stopPos = stopPosObj.position;
        endPos = endPosObj.position;
        StartCoroutine(MoveToStop());

    }

    void RecievedHumanMovementPacketEvent(Vector3 Pos)
    {
        transform.position = Pos;
    }

    public void SendHumanMovementPacket()
    {
        if (NetworkManager.instance != null)
        {
            NetworkManager.instance.Send(new HumanMovementPacket(
                NetworkManager.instance.playerData,
                networkComponent.GameObjectID,
                transform.position).Serialize());

            print(networkComponent.GameObjectID);
        }
    }

    public IEnumerator MoveToStop()
    {
        while (Vector3.Distance(transform.position, stopPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPos, speed * Time.deltaTime);
            SendHumanMovementPacket();
            yield return null;
        }

        humanItemPlacement.PlaceItems(random);
      
    }

    public IEnumerator MoveToLast()
    {
        while (Vector3.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            SendHumanMovementPacket();
            yield return null;
        }

        ResetToStart();
        humanItemPlacement.ClearItems();
        StartCoroutine(MoveToStop());

    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
