using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{

    
    public Sprite[] mapList;
    public Image map_Image;
    private int current_Map;
    public Sprite europe;
    public Sprite other;
    // Start is called before the first frame update
    void Start()
    {
        current_Map = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImage(bool next)
    {
        Debug.Log("Entered on changeImage()");
        if (next)
        {
            Debug.Log("next veryfied");
            if (current_Map == mapList.Length - 1)
            {
                Debug.Log("last index veryfied");
                current_Map = 0;
            }
            else
            {
                current_Map = current_Map + 1;
            }
        }
        else
        {
            Debug.Log("previous veryfied");
            if (current_Map == 0)
            {
                Debug.Log("index 0 veryfied");
                current_Map = mapList.Length - 1;
            }
            else
            {
                current_Map = current_Map - 1;
            }
        }
        Debug.Log("exit from if(next)");
        setSpriteMap(current_Map);
    }

    private void setSpriteMap(int id)
    {
        Debug.Log("index to " + id);
        map_Image.sprite = mapList[id];
    }
}
