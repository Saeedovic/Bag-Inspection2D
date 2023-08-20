using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    ////// These are the Bag variables 
    Socket socket;
    public static NetworkManager instance;
    public GameObject bagPrefab;
    public PlayerData playerData;
    public ButtonActions buttonActions;
    public ItemPlacement itemPlacement;
    public BagMovement bagMovement;
    public delegate void RecievedBagMovementPacketEvent(Vector3 Pos);
    public RecievedBagMovementPacketEvent OnRecievedBagMovementPacket;
    
    /////These are the NPC variables
    public GameObject npcPrefab;
    public HumanButtonActions humanButtonActions;
    public HumanItemPlacement humanItemPlacement;
    public HumanMovement humanMovement;
    public delegate void RecievedHumanMovementPacketEvent(Vector3 Pos);
    public RecievedHumanMovementPacketEvent OnRecievedHumanMovementPacketEvent;



    void Awake()
    {
        buttonActions = FindObjectOfType<ButtonActions>();
        bagMovement = FindObjectOfType<BagMovement>();
        itemPlacement = FindObjectOfType<ItemPlacement>();

        humanButtonActions = FindObjectOfType<HumanButtonActions>();
        humanItemPlacement = FindObjectOfType<HumanItemPlacement>();
        humanMovement = FindObjectOfType<HumanMovement>();

        Server server = FindObjectOfType<Server>();
        if (server != null)
        {
            server.OnClientConnected += () =>
            {
                if (server.clients.Count == 2)
                {
                    SpawnBag();
                    SpawnNpc();
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

    public void SpawnNpc()
    {
        GameObject npc = Instantiate(npcPrefab);
        HumanMovement npcMovement = npc.GetComponent<HumanMovement>();
        StartCoroutine(npcMovement.MoveToStop());
        npcMovement.SendHumanMovementPacket();

    }
    void Update()
    {
        // checking if theres any data to be received from the socket 
        if (socket.Available > 0)
        {
            byte[] buffer = new byte[256];
            socket.Receive(buffer);
            BasePacket bp = new BasePacket().Deserialize(buffer);


            if (bp.packType == BasePacket.PackType.BagMovement)
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
                itemPlacement.ClearItems();
            }
            else if (bp.packType == BasePacket.PackType.Random)
            {
                RandomPacket rp = new RandomPacket().Deserialize(buffer);
                //  bagMovement.random = rp.random;
                itemPlacement.islocal = false;
                itemPlacement.PlaceItems(rp.random);
                Debug.Log("Recieved" + rp.random);



            }
            else if (bp.packType == BasePacket.PackType.NpcRandom)
            {
                NpcRandomPacket nrp = new NpcRandomPacket().Deserialize(buffer);
                humanItemPlacement.islocal = false;
                humanItemPlacement.PlaceItems(nrp.random);
                Debug.Log("Recieved" + nrp.random);



            }
            else if (bp.packType == BasePacket.PackType.HumanMovement)
            {
                HumanMovementPacket hmp = new HumanMovementPacket().Deserialize(buffer);

                foreach (NetworkComponent nc in FindObjectsOfType<NetworkComponent>())
                {
                    if (nc.GameObjectID == hmp.GameObjectID)
                    {
                        nc.transform.position = hmp.Pos;
                        break;
                    }
                }
                humanItemPlacement.ClearItems();

            }
            else if(bp.packType == BasePacket.PackType.Points)
            {
                PointsPacket pp = new PointsPacket().Deserialize(buffer);
                buttonActions.points = pp.points;
            }
        }
    }

    public void Send(byte[] buffer)
    {
        socket.Send(buffer);
    }

}

