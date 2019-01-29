using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // questa classe gestisce lo scamvbio di messasggio tra client e server

    public static NetworkManager istanza;
    private static string messaggio;  // la uso per comunicare con il server
    private static int N_player; // numero di player
    private static int state = 0; // stato generale per la lettura
    private static int mappa=-1; // mappa scelta
    private static int statoTurno = 0; // stato del turno
   private static ModelGameMap model;
   
    private void Awake()
    {
        istanza = this;
        N_player = 0;
        model = GameObject.Find("CanvasMain").GetComponent<ModelGameMap>();
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

    //uso questa classe si aper il posizionamento che per la combo carte
    public static void Aggiorna(string s, int mod)
    {
        switch (mod)
        {
            case 1:
                {
                    model.AggiornaPosizionamento(s);
                    break;
                }
            case 2:
                {
                    model.AggiornaComboCarte(s);
                    break;
                }
            case 3:
                {
                    model.AggiornaSpostamento(s);
                    break;
                }
                case 4:
                {
                    model.AggiornaAttacco(s);
                    break;
                }
        }
     state = 0; // rimetto lo stato a 0
    }
    
    public static int GetStatoTurno()
    {
        return statoTurno;
    }
    public static void SetstatoTurno(int a)
    {
        statoTurno = a;
    }
    public static void SetMappa(int a)
    {
        mappa = a;
    }
    public static int GetMappa()
    {
        return mappa;
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
    public static int GetNPlayer()
    {
        return N_player;
    }
    public static void SetNPlayer(int s)
    {
        N_player= s;
    }
    public static void DimNPlayer()
    {
        N_player--;
    }
    public static void AddNPlayer()
    {
        N_player++;
    }

}