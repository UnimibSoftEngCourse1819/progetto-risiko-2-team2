using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // questa classe gestisce lo scamvbio di messasggio tra client e server

    public static NetworkManager istance;
    private static string message;  // la uso per comunicare con il server
    private static int nPlayers; // numero di player
    private static int state = 0; // stato generale per la lettura
    private static int map = -1; // mappa scelta
    private static int turnState = 0; // stato del turno
   private static ModelGameMap model;
   
    private void Awake()
    {
        istance = this;
        nPlayers = 0;
        model = GameObject.Find("CanvasMain").GetComponent<ModelGameMap>();
    }

    // Start is called before the first frame update
    public void initiate()
    {
        DontDestroyOnLoad(this);
        UnityThread.initUnityThread();

        ClientHandleData.InizializzaPacchetti();
        ClientTcp.InizializzaNetwork();
    }

    private void onApplicationQuit()
    { // disconnetti quando esci
        DataSender.SendGoodBye();
        ClientTcp.Disconnect();
    }

    public static int passTurn()
    {
        message = "";
        DataSender.SendPasso(); // mando il segnale di passo
        return 0;

    }

    //uso questa classe si aper il posizionamento che per la combo carte
    public static void refresh(string s, int mod)
    {
        switch (mod)
        {
            case 1:
                {
                    model.updateDeploy(s);
                    break;
                }
            case 2:
                {
                    model.updateCards(s);
                    break;
                }
            case 3:
                {
                    model.updateMove(s);
                    break;
                }
                case 4:
                {
                    model.updateAttack(s);
                    break;
                }
        }
     state = 0; // rimetto lo stato a 0
    }
    
    public static int getTurnState()
    {
        return turnState;
    }
    public static void setTurnState(int a)
    {
        turnState = a;
    }
    public static void setMap(int a)
    {
        map = a;
    }
    public static int getMap()
    {
        return map;
    }
    public static string getMessage()
    {
        return message;
    }
    public static void setMessage(string s)
    {
        message = s;
    }
    public static int getState()
    {
        return state;
    }
    public static void setState(int s)
    {
        state = s;
    }
    public static int getNPlayer()
    {
        return nPlayers;
    }
    public static void setNPlayer(int s)
    {
        nPlayers= s;
    }
    public static void dimNPlayer()
    {
        nPlayers--;
    }
    public static void addNPlayer()
    {
        nPlayers++;
    }

}