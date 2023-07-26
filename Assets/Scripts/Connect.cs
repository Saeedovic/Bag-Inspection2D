using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{
    public NetworkManager NetworkManager;
    
    public void OnClickConnect()
    {
        NetworkManager.Connect();
    }
}
