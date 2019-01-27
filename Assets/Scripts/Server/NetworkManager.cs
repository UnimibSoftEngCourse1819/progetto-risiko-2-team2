using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // questa classe gestisce lo scamvbio di messasggio tra client e server

    public static NetworkManager istanza;
    private static string messaggio;  // la uso per comunicare con il server
    private static string N_player;
    private static int state = 0;
    

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

    public static int PassaTurno()
    {
        messaggio = "";
        DataSender.SendPasso(); // mando il segnale di passo
        return 0;

    }

    public static string GetMessaggio()
    {
        return messaggio;
    }
    public static void SetMessaggio(string s)
    {
        messaggio = s;
    }
    public static int GetState()
    {
        return state;
    }
    public static void SetState(int s)
    {
        state = s;
    }
    public static string GetNPlayer()
    {
        return N_player;
    }
    public static void SetNPlayer(string s)
    {
        N_player= s;
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
