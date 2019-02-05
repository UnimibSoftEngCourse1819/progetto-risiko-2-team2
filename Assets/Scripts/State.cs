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
    public List<string> connections;
    public string continent;
    public Texture2D text;

    protected void Awake()
    {
        Click = new Pressed();
    }

    public void SetState(Texture2D text)
    {
        SetStateTexture(text);
    }

    public void SetState(Texture2D text, string name)
    {
        SetState(text);
        idName = name;
    }

    public void SetStateTexture(Texture2D text)
    {
        this.text = text;
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        
        Sprite s = Sprite.Create(text, new Rect(0, 0, text.width, text.height), pivot, 320f);
        GetComponent<SpriteRenderer>().sprite = s;

        Destroy(GetComponent<PolygonCollider2D>());
        PolygonCollider2D coll = gameObject.AddComponent<PolygonCollider2D>();

        coll.isTrigger = true;

        Debug.Log("Valore " + s.pixelsPerUnit);
    }

    public void Erase()
    {
        Destroy(this.gameObject);
    }

    public Pressed Click;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Click.Invoke(this);
    }
}

public class Pressed : UnityEvent<State>
{

}
