using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]


public class State_script : MonoBehaviour
{

    private int MAX_BORDERS = 10;

    private string land_name;
    private string Continent_name;
    private string land_code;
    private string[] near_lands;
    private int Player_ID;
    private int N_tank;
    private Attack_script attacco;
    private Spostamento_script spostamento;
    private int N_vicini=0;

    private void Awake()
    {
        attacco =  GameObject.Find("Attacca").GetComponent<Attack_script>();
        spostamento = GameObject.Find("Spostamento").GetComponent<Spostamento_script>();
    }

    private void OnMouseDown() // passo allo script attack_script o allo script spostamento i codici degli stati
    {
        if (attacco.GetAtt() == 1)
        {
            attacco.GetAttaccante(land_code);
            Debug.Log("primo");
        }
        else if (attacco.GetAtt() == 2)
        {
            attacco.GetAttaccato(land_code);
            Debug.Log("secondo");
        }
        else if (spostamento.GetState() == 1)
            spostamento.GetPartenza(land_code);
        else if (spostamento.GetState() == 2)
            spostamento.GetArrivo(land_code);

    }

    public int IslandNear(string codice)  // controlla se un dato territorio è un vicino
    {
        int r=-1;
        for (int i =0; i< N_vicini;i++)
        {
            if (near_lands[i] == codice)
                r = 1;
        }
        return r;
    } 

    public void Inizializza(string name, string c_name, string code)
    {
        land_name = name;
        Continent_name = c_name;
        land_code = code;
        near_lands = new string[MAX_BORDERS];
        for (int i=0;i<MAX_BORDERS;i++)
        {
            near_lands[i] = "";
        }
    }

    public void Inser_new_neighbour(string neighbour) // salvo il nuovo vicino
    {
        near_lands[N_vicini] = neighbour;
        N_vicini++;
    }

    public string GetName()
    {
        return land_name;
    }
    public string GetCodice()
    {
        return land_code;
    }

    public int DimTanks(int c)  // controlla se posso diminuire il numero di carri e ritorna il risultato
    {
        if (c > N_tank)
            return -1;
        else
            N_tank -= c;
        return 1;
    }

    public void IncTanks(int c) // aumenta il numero di carri
    {
        N_tank += c;
    }
   
    public int GetTanks() // ritorna il numero di carri
    {
        return N_tank;
    }


}
