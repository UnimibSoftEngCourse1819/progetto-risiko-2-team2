using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]


public class state_script : MonoBehaviour
{

    public int a = 5;

    private void OnMouseDown()
    {
        Debug.Log("stato premuto");
        
    }


    public void stato()
    {
        Debug.Log("metodo stato");
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
