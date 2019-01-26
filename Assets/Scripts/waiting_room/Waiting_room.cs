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

    private void Awake()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();

        text1.text = "premi il bottone quando tu e i tuoi amici siete tutti connessi";
        text2.text = "";
    }
    public void OnMouseDown()
    {
        if (aggiorna == 0) // controllo numero di player
        {
            aggiorna = 1;
            DataSender.AskNPlayer(); 
        }    
        else if(aggiorna ==1) // faccio partire la partita
        {
            DataSender.StarGame();
        }
    }
    private void Update()
    {
        if(aggiorna == 1 && NetworkManager.messaggio == "no")
        {
            text2.text = "non ci sono abbastanza giocatori";
        }
        else if(aggiorna == 1 && NetworkManager.messaggio == "si")
        {
            text2.text = "ci sono giocatori a sufficienza";
            text1.text = "premi il bottone per far partire la partita";
        }
        if(NetworkManager.messaggio == "start") // entra in game
        {
            SceneManager.LoadScene("mappa_0");
        }
    }

    


}
