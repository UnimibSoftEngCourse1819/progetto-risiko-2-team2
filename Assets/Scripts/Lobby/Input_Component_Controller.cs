using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Component_Controller : MonoBehaviour
{
    public View_UI UIScript;
    public void start_Button_Click()
    {

        Debug.Log("Start Button has been clicked");
    }
    
    public void exit_Button_Click()
    {
        Debug.Log("Exit Button has been clicked");
    }

    public void easy_Button_Click()
    {
        Debug.Log("Easy Button has been clicked");
    }

    public void medium_Button_Click()
    {
        Debug.Log("Medium Button has been clicked");
    }

    public void hard_Button_Click()
    {
        Debug.Log("Hard Button has been clicked");
    }

    public void next_Map_Button_Click()
    {
        Debug.Log("Next Button has been clicked");
        UIScript.changeImage(true);
    }
  
    public void previus_Map_Button_Click()
    {
        Debug.Log("Previous Button has been clicked");
        UIScript.changeImage(false);
    }
}
