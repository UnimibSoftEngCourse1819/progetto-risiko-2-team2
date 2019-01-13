using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class land_installer: MonoBehaviour
{
   
    private Sprite land_sprite;
    private PolygonCollider2D collider3;



    void Start()
    {
        land_sprite = GameObject.Find("eu-1").GetComponent<Sprite>();
  //  distruggere l'event collider
    //     collider3 = GameObject.Find("eu-1").GetComponent<PolygonCollider2D>();
   //     Destroy(collider3);
        Debug.Log(land_sprite.ToString());
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
