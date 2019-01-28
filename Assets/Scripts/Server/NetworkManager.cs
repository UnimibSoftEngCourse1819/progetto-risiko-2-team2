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
    private static string[] stati = new string[2];
    private static string[] risultatoAttacco = new string[2];
    private static string nomePlayer; // indica il nome del player che ha fatto qualcosa

    private void Awake()
    {
        istanza = this;
        N_player = 0;
      
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

    public static void AggiornaAfterAttacco(string s)
    {
        
        switch (statoTurno) // inizializzo le 4 variabili e poi le invio 
        {
            case 0:
                {
                    nomePlayer = s;
                    break;
                }
            case 1:
                {
                    stati[0] = s;
                    break;
                }

            case 2:
                {
                    stati[1] = s;
                    break;
                }

            case 3:
                {
                    risultatoAttacco[0] = s;
                    break;
                }

            case 4:
                {
                    risultatoAttacco[1] = s;
                    statoTurno = 0;
                    state = 0; // rimetto lo stato a 0
                    // gestico i dati ricevuti magari impostando uno stato "attacco"
                    break;
                }
                
        }
        statoTurno++;
    }
    public static void AggiornaAfterSpostamento(string s)
    {
       
        switch (statoTurno) // inizializzo le 3 variabili e poi le invio 
        {
            case 0:
                {
                    nomePlayer = s;
                    break;
                }
            case 1:
                {
                    stati[0] = s;
                    break;
                }

            case 2:
                {
                    stati[1] = s;
                    break;
                }

            case 3:
                {
                    risultatoAttacco[0] = s;
                    statoTurno = 0;
                    state = 0; // rimetto lo stato a 0
                               // gestico i dati ricevuti magari impostando uno stato "attacco"
                    break;
                }  
        }
        statoTurno++;
    }
    public static string GetNomePlayer()
    {
        return nomePlayer;
    }
    public static void SetNomePlayer(string s)
    {
        nomePlayer = s;
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
