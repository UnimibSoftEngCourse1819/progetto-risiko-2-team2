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

    private void Awake()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();

        text1.text = "premi il bottone quando tu e i tuoi amici siete tutti connessi";
        text2.text = "";
    }
    public void OnMouseDown()
    {
        count++;
        if (NetworkManager.GetState() == 0)
        {
            if (aggiorna == 0 && NetworkManager.GetMessaggio() == "ok") // controllo numero di player
            {
                aggiorna = 1;
                DataSender.AskNPlayer();
            }          
            else if (aggiorna == 2) // faccio partire la partita
            {
                DataSender.StarGame();
            }
        }
        else
        {
            text2.text = "non sei ancora stato caricato aspetta un secondo";
        }
    }
    private void Update()
    {
      if(aggiorna == 1 )
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer()+" player";
            text1.text = "premi il bottone per far partire la partita";
            if (NetworkManager.GetNPlayer() == "1" || NetworkManager.GetNPlayer() == "2" || NetworkManager.GetNPlayer() == "3" || NetworkManager.GetNPlayer() == "4" || NetworkManager.GetNPlayer() == "5" || NetworkManager.GetNPlayer() == "6")
                aggiorna = 2; // solo se il numero di player è accettabile
            else
            {
                text2.text = "il numero di player non è accettabile";
                aggiorna = 0; // così posso ripremere il tasto button 
            }
        }
       else if(NetworkManager.GetMessaggio() == "start") // entra in game
        {
            SceneManager.LoadScene("mappa_0");
        }
       else if(NetworkManager.GetMessaggio()=="NewPlayer")
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer() + " player";
       }
        else if (NetworkManager.GetMessaggio() == "PlayerQuit")
        {
            text2.text = "ci sono, al momento sono connessi " + NetworkManager.GetNPlayer() + " player";
        }
    }

    


}
