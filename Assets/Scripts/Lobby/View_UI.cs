using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_UI : MonoBehaviour
{

    
    
    private int current_Map;
    private Map_Data[] maps; 

    //view component 
    public Image map_Image;
    public Text text_view;
    public InputField input_lands; 
    
    //need to make it automatically (no drag and drop)
    public int[] minEasyList;
    public int[] minMediumList;
    public int[] minHardList;
    public int[] maxEasyList;
    public int[] maxMediumList;
    public int[] maxHardList;
    public string[] nameList;
    public Sprite[] map_Image_List;

    private void Start()
    {
        loadMap();
    }


    public void changeImage(bool next)
    {
        Debug.Log("Entered on changeImage()");
        if (next)
        {
            Debug.Log("next veryfied");
            if (current_Map == map_Image_List.Length - 1)
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
                current_Map = map_Image_List.Length - 1;
            }
            else
            {
                current_Map = current_Map - 1;
            }
        }
        Debug.Log("exit from if(next)");
        set_Default_Settings(current_Map);
    }

    private void set_Default_Settings(int id)
    {
        Debug.Log("index to " + id);
        map_Image.sprite = maps[id].getimage();
        text_view.text = maps[id].getName();
        input_lands.text = "" + ( maps[id].getmaxNormal() + maps[id].getminNormal() ) / 2;
    }

    private void loadMap()
    {
        maps = new Map_Data[map_Image_List.Length];
        for(int i = 0; i<map_Image_List.Length; i++)
        {
            maps[i] = new Map_Data(nameList[i], maxEasyList[i], maxMediumList[i], maxHardList[i], minEasyList[i], minMediumList[i], minHardList[i], map_Image_List[i]);
        }
    }
}
