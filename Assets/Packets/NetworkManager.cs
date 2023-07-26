using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    Socket socket;
    public static NetworkManager instance;
    public PlayerData playerData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    void Update()
    {
        if (socket.Available > 0)
        {
            byte[] buffer = new byte[256];
            socket.Receive(buffer);
            BasePacket bp = new BasePacket().Deserialize(buffer);

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
        }
        
    }

    public void SendPacket(ItemPacket itemPacket)
    {
        byte[] buffer = itemPacket.Serialize();
        socket.Send(buffer);
    }
}

