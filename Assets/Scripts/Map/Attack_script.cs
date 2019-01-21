using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_script : MonoBehaviour
{
    private Text testo;
    private State_script attaccante;
    private State_script attaccato;
    private int att = 0;
    private int vicini;

    private void Awake()
    {
        testo =  GameObject.Find("Map_text").GetComponent<Text>();
        testo.text = "Benvenuto nella mappa";
    }

    public void OnMouseDown() // solo quando è il turno di un giocatore
    {
        testo.text = "scegli uno stato da cui attaccare";
        att=1;
        // ovviamente lo stato da cui attaccare deve appartenere al giocatore
        Debug.Log("pronto per attaccare");
    }

    public int GetAtt()
    {
        return att;
    }

    public void GetAttaccante(string codice) // salvo il territorio da cui attacacre
    {
        attaccante = GameObject.Find(codice).GetComponent<State_script>();
        att=2;
        testo.text = "seleziona uno stato da attaccare";
    }

    public void GetAttaccato(string codice) // territorio da attacare e conttrollo
    {
        attaccato = GameObject.Find(codice).GetComponent<State_script>();
        vicini=attaccato.IslandNear(attaccante.GetCodice()); // guardo se sono sue territori vicini
        if(vicini==-1)
        {
            att = 0;
            testo.text = "I territori selezionati non sono vicini scegliene altri";
        }
    }

    private void Update()
    {
        if ((vicini == 1) && (att == 2))  // eseguo un attacco
        {
            att = 0;
            vicini = 0;
            testo.text = "attacco da "+attaccante.GetName()+" a "+ attaccato.GetName()+" eseguito";
        }
    }



}
