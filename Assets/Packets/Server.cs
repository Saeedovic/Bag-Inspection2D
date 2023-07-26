using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Collections.Generic;

public class Server : MonoBehaviour
{
    Socket serverSocket;
    List<Socket> clients;

    void Start()
    {
        serverSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        serverSocket.Listen(10);
        serverSocket.Blocking = false;

        print("Waiting for client to connect...");
        clients = new List<Socket>();
    }

    void Update()
    {
        try
        {
            clients.Add(serverSocket.Accept());
            
            Debug.LogError("Client Connected");
        }
        catch
        {
            //Console.WriteLine("No client connected");
        }

        try
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Available > 0)
                {
                    byte[] buffer = new byte[clients[i].Available];
                    clients[i].Receive(buffer);

                    for (int j = 0; j < clients.Count; j++)
                    {
                        if (i == j)
                            continue;

                        clients[j].Send(buffer);
                    }
                }
            }
        }
        catch
        {
            //Console.WriteLine("No client connected");
        }
    }
}