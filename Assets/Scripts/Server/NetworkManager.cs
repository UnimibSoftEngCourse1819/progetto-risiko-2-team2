using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager istanza;
    public static string messaggio;  // la uso per comunicare con il server

    private void Awake()
    {
        istanza = this;
    }

    // Start is called before the first frame update
    public void Partenza()
    {
        DontDestroyOnLoad(this);
        UnityThread.initUnityThread();

        ClientHandleData.InizializzaPacchetti();
        ClientTcp.InizializzaNetwork();
    }

    private void OnApplicationQuit()
    { // disconnetti quando esci
        ClientTcp.Disconnect();
    }

}
