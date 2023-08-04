using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    Socket socket;
    public static NetworkManager instance;
    public GameObject bagPrefab;
    public PlayerData playerData;
    public ButtonActions buttonActions;
    public delegate void RecievedBagMovementPacketEvent(string GameObjctID, Vector3 Pos);
    public RecievedBagMovementPacketEvent OnRecievedBagMovementPacket;

    void Awake()
    {
        buttonActions = FindObjectOfType<ButtonActions>();

        Server server = FindObjectOfType<Server>();
        if (server != null)
        {
            server.OnClientConnected += () =>
            {
                if (server.clients.Count == 2)
                {
                    SpawnBag();
                }
            };
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        print(" Network manager running");
    }

    void Start()
    {
        string id = Random.Range(0, 100000).ToString();
        playerData = new PlayerData(id, $"player{id}");

        socket = new Socket(
           AddressFamily.InterNetwork,
           SocketType.Stream,
           ProtocolType.Tcp);

        socket.Blocking = false;
    }

    public void Connect()
    {
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
    }

    public void SpawnBag()
    {
       
        GameObject bagObject = Instantiate(bagPrefab);
        BagMovement bagMovement = bagObject.GetComponent<BagMovement>();
        StartCoroutine(bagMovement.MoveToEnd());
        bagMovement.SendBagMovementPacket();
    }
    void Update()
    {
        if (socket.Available > 0)
        {
            byte[] buffer = new byte[256];
            socket.Receive(buffer);
            BasePacket bp = new BasePacket().Deserialize(buffer);

            print("Recieved packet" + bp.packType);
            if (bp.packType == BasePacket.PackType.ItemPlacement)
            {
                ItemPacket ip = new ItemPacket().Deserialize(buffer);

                Debug.Log(ip.items.Count);
                /*    ItemPlacement itemPlacement = GetComponent<ItemPlacement>();
                    if (itemPlacement != null)
                    {
                        foreach (var item in ip.items)
                        {
                            itemPlacement.PlaceItem(item.position, item.rotation);
                        }
                    }*/
            }
            else if (bp.packType == BasePacket.PackType.BagMovement)
            {
                BagMovementPacket bmp = new BagMovementPacket().Deserialize(buffer);

                foreach (NetworkComponent nc in FindObjectsOfType<NetworkComponent>())
                {
                    if (nc.GameObjectID == bmp.GameObjectID)
                    {
                        nc.transform.position = bmp.Pos;
                        break;
                    }
                }
            }

        }
    }

        public void Send(byte[] buffer)
        {
            socket.Send(buffer);
        }

    }

