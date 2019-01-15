using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent_Script : MonoBehaviour
{

    
    private string code;
    private int value;
    private int n_states;
    

    public void Inizializza (string code, int value, int n_states)
    {
        this.code = code;
        this.value = value;
        this.n_states = n_states;
        
    }

    public int Get_value()
    {
        return value;
    }



}
