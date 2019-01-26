using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class State : MonoBehaviour, IPointerClickHandler
{
    public float unitPerPixels = 320f;
    
    public string idName;
    string[] connections;

    public void setState(Texture2D text)
    {
        setStateTexture(text);
    }

    public void setState(Texture2D text, string name)
    {
        setState(text);
        idName = name;
    }

    public void setStateTexture(Texture2D text)
    {

        Vector2 pivot = new Vector2(0.5f, 0.5f);
        
        Sprite s = Sprite.Create(text, new Rect(0, 0, text.width, text.height), pivot, unitPerPixels);
        GetComponent<SpriteRenderer>().sprite = s;

        Destroy(GetComponent<PolygonCollider2D>());
        PolygonCollider2D coll = gameObject.AddComponent<PolygonCollider2D>();

        coll.isTrigger = true;
    }

    public void Erase()
    {
        Destroy(this.gameObject);
    }

    public delegate void StatePressedDelegate(State buttonState);
    public StatePressedDelegate click;

    public UnityEvent statePressed;
    public void OnPointerClick(PointerEventData eventData)
    {
        click(this);
    }
}
