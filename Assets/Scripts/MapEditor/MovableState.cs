using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableState : State, IDragHandler
{
    private Rect boundraries;

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spriteSize = Camera.main.ScreenToWorldPoint(GetComponent<SpriteRenderer>().sprite.rect.size);        

        newPosition.x = Mathf.Clamp(newPosition.x, boundraries.x - spriteSize.x / 4, boundraries.width + spriteSize.x / 4);
        newPosition.y = Mathf.Clamp(newPosition.y, boundraries.y - spriteSize.y / 4, boundraries.height + spriteSize.y / 4);

        gameObject.transform.localPosition = new Vector3(newPosition.x, newPosition.y, -2);
    }

    public void setBoundraries(Rect r)
    {        
        boundraries = r;
    }
}
