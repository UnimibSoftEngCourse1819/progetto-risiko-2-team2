using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattle : MonoBehaviour
{
    private State[] stateList;


    public void setMap(Dictionary<string, Dictionary<string, string>> mapData)
    {

        foreach(var state in mapData.Values)
        {

            State newState = new State();
            

        }

    }
    
}
