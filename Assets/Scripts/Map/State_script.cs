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

    private void OnMouseDown()
    {
        Debug.Log(land_name);
        
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
        for (int i = 0; i < MAX_BORDERS; i++)
        {
            if (near_lands[i] == "")
            {
                near_lands[i] = neighbour;
                break;
            }
        }
    }




}
