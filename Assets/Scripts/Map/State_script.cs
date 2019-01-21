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
    private int N_vicini=0;

    private void Awake()
    {
        attacco =  GameObject.Find("Attacca").GetComponent<Attack_script>();
    }

    private void OnMouseDown() // passo allo script attack_script i codici degli stati
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


}
