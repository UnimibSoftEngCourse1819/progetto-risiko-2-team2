using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Waiting_room : MonoBehaviour
{
    private Text text1;
    private Text text2;
    private int aggiorna=0;
    private int count=0;
    private int mappa;
    private bool play=false;

    private void Awake()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();

        text1.text = "premi questo play per vedere quanti player sono connessi";
        text2.text = "";
    }
    public void PlayGame()
    {
        count++;
        if (NetworkManager.GetState() == 0)
        {
            if (aggiorna == 0 && NetworkManager.GetMessaggio() == "ok") // controllo numero di player
            {
                aggiorna = 1;
                DataSender.AskNPlayer();
            }          
            else if (aggiorna == 2 & play) // faccio partire la partita
            {
                DataSender.StarGame(mappa);
                
            }
        }
        else
        {
            text2.text = "non sei ancora stato caricato aspetta un secondo";
        }

    }
    public void CreateNewMap()
    {
        // vai alla crea nuova mappa
    }
    public void GetMappaName(int s) // si è pronti a giocare e si è scelta la mappa
    {
        mappa = s;
        play = true;
        Debug.Log("mappa cambiata");
    }

    private void Update()
    {
      if(aggiorna == 1 )
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer()+" player";
            text1.text = "premi play per far partire la partita ()";
            if (NetworkManager.GetNPlayer() >=1 & NetworkManager.GetNPlayer()<=6)
                aggiorna = 2; // solo se il numero di player è accettabile
            else
            {
                text1.text = "premi play per ricaricare";
                text2.text = "il numero di player non è accettabile";
                aggiorna = 0; // così posso ripremere il tasto button 
            }
        }       
       else if(NetworkManager.GetMessaggio()=="NewPlayer")
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer() + " player";
       }
        else if (NetworkManager.GetMessaggio() == "PlayerQuit")
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer() + " player";
        }
      if(play  & NetworkManager.GetMappa()>=0)
        {
            switch (NetworkManager.GetMappa())
            {
                case 1:
                    {
                        Debug.Log(" mappa in partenza caso 1");
                        SceneManager.LoadScene("mappa_0");
                        break;
                    }
                case 0:
                    {
                        Debug.Log("mappa in partenza caso 0");
                        SceneManager.LoadScene("mappa_0");
                        break;
                    }
            }
        }
    }

    


}
