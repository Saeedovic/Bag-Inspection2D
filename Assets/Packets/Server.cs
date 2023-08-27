using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Collections.Generic;


public class Server : MonoBehaviour
{
    Socket serverSocket;
    public List<Socket> clients;
    public event Action OnClientConnected;
    NetworkComponent NetworkComponent;

    void Start()
    {
        
        serverSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        serverSocket.Listen(10);
        serverSocket.Blocking = false;

        print("Waiting for clients to connect...");
        clients = new List<Socket>();
        InvokeRepeating("IsAlive", 1, 1);
    }

    void Update()
    {
        try
        {
            clients.Add(serverSocket.Accept());
            OnClientConnected?.Invoke();

            Debug.LogError("Client Connected");

            if (clients.Count >= 2)
            {
                Debug.LogError("2 Clients Connected");

            
          

            }
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode == SocketError.WouldBlock)
            {
                Debug.Log(e);
            }
        }

        for (int i = 0; i < clients.Count; i++)
        {

            try
            {

                if (clients[i].Available > 0)
                {
                    byte[] buffer = new byte[clients[i].Available];
                    clients[i].Receive(buffer);

                    print("Recieved packet");

                    for (int j = 0; j < clients.Count; j++)
                    {
                        if (i == j)
                            continue;

                        clients[j].Send(buffer);
                        print("Sent packet");

                    }
                }

            }
            catch (SocketException e)
            {

                if (e.SocketErrorCode == SocketError.WouldBlock)
                {
                     Debug.Log(e);
                    
                }

            }
        }
    }

    void IsAlive()
    {
        print("is alibe");
        for (int i = 0; i < clients.Count; i++)
        {
            try
            {
                clients[i].Send(new byte[1]);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode != SocketError.WouldBlock)
                {
                    if (e.SocketErrorCode == SocketError.ConnectionAborted || e.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        Debug.Log("Disconnected");
                        clients[i].Close();
                        clients.RemoveAt(i);
                    }
                    else
                    {
                        Debug.Log(e);
                    }
                }

            }
        }
    }
}