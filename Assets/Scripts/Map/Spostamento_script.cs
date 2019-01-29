using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spostamento_script : MonoBehaviour
{
    private InputField input;
    private Text testo;
    private Attack_script attacco;
    private State_script partenza; // da dove sposto
    private State_script arrivo; // verso dove sposto
    private int state;
    private int vicini;

    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        testo = GameObject.Find("Map_text").GetComponent<Text>();
        attacco = GameObject.Find("Attacca").GetComponent<Attack_script>();
        state = 0;
        input.DeactivateInputField(); // disabilito l'input
    }

    public void OnMouseDown()
    {
        
        if(state==0)
        {
            if (attacco.GetAtt() != 0) // controllo che il player non abbia gia premuto attacco
                attacco.SetAtt(0);
            testo.text = "scegli lo stato da cui spostare i carri "; // ovviamente andrà introdotto un controllo per chi possiede il territorio
            state = 1;
        }
    }

    public void GetPartenza(string codice) // salvo il territorio da cui spostare  (molto simile al codice dell'attacco)
    {
        partenza = GameObject.Find(codice).GetComponent<State_script>();
        state = 2;
        testo.text = "seleziona uno stato sul quale spostare i carri";
    }

    public void GetArrivo(string codice) // territorio su cui spostare e conttrollo
    {
        arrivo = GameObject.Find(codice).GetComponent<State_script>();
        vicini = arrivo.IslandNear(partenza.GetCodice()); // guardo se sono sue territori vicini
        if (vicini == -1)
        {
            state = 0;
            testo.text = "I territori selezionati non sono vicini scegliene altri";
        }
        else
        {
            input.ActivateInputField();
            testo.text = "inserisci il numero di carri che vuoi spostare";
            state = 3;
        }
    }
    public void Sposta(string n) // riceve in input il numero dicarri e fa lo spostamento
    {
        if (partenza.DimTanks(int.Parse(n)) != 1)
        {
            testo.text = "hai inserito un numero di carri troppo elevato";
        }
        else
        {
            arrivo.IncTanks(int.Parse(n));
            testo.text = "spostati "+int.Parse(n)+"carri da "+partenza.GetName() + " a " + arrivo.GetName();
        }
        state = 0;
        input.DeactivateInputField();
    }


    public int GetState()
    {
        return state;
    }
    public void SetSate(int a)
    {
        state = a;
    }

}
