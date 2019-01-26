using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager istanza;
    public static string messaggio;  // la uso per comunicare con il server
    public static string N_player;
    public static int state = 0;

    private void Awake()
    {
        istanza = this;
        N_player = "0";
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
        DataSender.SendGoodBye();
        ClientTcp.Disconnect();
    }

    public static void DimNPlayer()
    {
        switch(N_player)
        {
            case "2":
                {
                    N_player = "1";
                    break;
                }
            case "3":
                {
                    N_player = "2";
                    break;
                }
            case "4":
                {
                    N_player = "3";
                    break;
                }
            case "5":
                {
                    N_player = "4";
                    break;
                }
            case "6":
                {
                    N_player = "5";
                    break;
                }
        }
    }
    public static void AddNPlayer()
    {
        switch (N_player)
        {
            case "1":
                {
                    N_player = "2";
                    break;
                }
            case "2":
                {
                    N_player = "3";
                    break;
                }
            case "3":
                {
                    N_player = "4";
                    break;
                }
            case "4":
                {
                    N_player = "5";
                    break;
                }
            case "5":
                {
                    N_player = "6";
                    break;
                }
            case "6":
                {
                    N_player = "7";
                    break;
                }
        }
    }

}
