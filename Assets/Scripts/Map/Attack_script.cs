using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_script : MonoBehaviour
{
    private Text testo;
    private State_script attaccante;
    private State_script attaccato;
    private Spostamento_script spostamento;
    private int att = 0;
    private int vicini;

    private void Awake()  // carica il testo nella map_text
    {
        spostamento= GameObject.Find("Spostamento").GetComponent<Spostamento_script>();
        testo =  GameObject.Find("Map_text").GetComponent<Text>();
        testo.text = "Benvenuto nella mappa";
    }

    public void OnMouseDown() // solo quando è il turno di un giocatore
    {
        if(att == 0)
        {
            if (spostamento.GetState() != 0) // controllo che non abbia gia premuto su state
                spostamento.SetSate(0);
            testo.text = "scegli uno stato da cui attaccare";
            att = 1;
        }
        
       
        // ovviamente lo stato da cui attaccare deve appartenere al giocatore
        Debug.Log("pronto per attaccare");
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
        else // eseguo l'attacco
        {
            att = 0;
            EseguiAttcco();
        }
    }

   private void EseguiAttcco() // esegue l'attacco con tutti i carri
    {
        int [] invasore =new int[attaccante.GetTanks()];
        int[] difensore = new int[attaccato.GetTanks()];
       int [] perdite ={0,0 }; // numero di perdite al posto 0 le perdite dell'attacante al posto 1 le perdite del difensore
        if (attaccante.GetTanks()<=1 || attaccante.GetTanks()<attaccato.GetTanks()) // non puoi attaccare con 1 armata e non puoi attaccare con meno armate del difensore
        {
            testo.text = "non puoi attaccare perchè hai troppi pochi carri";
        }
        else
        {
            testo.text = "attacco da"+attaccante.GetName()+" a "+attaccato.GetName()+"in esecuzione" ;
            new WaitForSeconds(5); // aspetta 5 secondi 
            for (int i=0;i<attaccante.GetTanks();i++)
            {
                invasore[i] = UnityEngine.Random.Range(1, 7);//lancia un dado per l'attacco
            }
            for (int i = 0; i < attaccato.GetTanks(); i++)
            {
                difensore[i] = UnityEngine.Random.Range(1, 7);//lancia un dado per la difesa
            }
            Array.Sort(invasore); // ordina
            Array.Sort(difensore);
            for (int i =0;i<attaccato.GetTanks();i++) 
            {
                if (invasore[i] > difensore[i])
                    perdite[1]++;
                else
                    perdite[0]++;
            }
            attaccante.DimTanks(perdite[0]); // sottraggo i carri persi
            attaccato.DimTanks(perdite[1]);
            testo.text = "l'attaccante perde "+perdite[0]+" carri il difensore perde "+perdite[1]+" carri";
            if(attaccato.GetTanks()==0)
            {
                 new WaitForSeconds(5); // aspetta 5 secondi 
                testo.text = "territorio conquistato"; // inserire la conquista
            }


        }
       

    }

    public int GetAtt()
    {
        return att;
    }
    public void SetAtt(int a)
    {
        att = a;
    }

}
