using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class StateEditor : MonoBehaviour
{
    // NOTE FOR THE READER: Public variables are used to select external object in the editor bby drag and dropping them on the script component

    // Holds the prefab reference used to instatiate the State in the editor
    public MovableState movableState;

    // Holds the center of where the MovableState Prefab can move avoiding the draggable MovableState goes too far from the Map of the Game
    public SpriteRenderer backGroundHolder;

    // Holds a reference to all State instatiate
    private List<MovableState> currentStates;
    // Holds a reference to the last clicked State in the Editor
    private MovableState selectedState;

    // Starting directory for the load and save Panels
    private string startPath = "C://";
   

    private void Awake()
    {
        currentStates = new List<MovableState>();
    }

    // Methods use to get a single png file and create a MovableState prefab in the editor     
    public void OnAddStateClick()
    {
        string path = EditorUtility.OpenFilePanel("Select new Texture", startPath, "png");
        GetImage(path);
    }
    private void GetImage(string path)
    {
        if (path != null)
        {
            WWW www = new WWW("file:///" + path);

            string temporaryStateName = CreateName(path);

            CreateNewState(www.texture, backGroundHolder.transform.position, temporaryStateName);
        }
    }

    // Method use to create a unique name for the new Craete state and avoiding that there are two state with the same name
    private string CreateName(string p)
    {
        string[] pathSplit = p.Split('/');
        string temporaryStateName = pathSplit[pathSplit.Length - 1];
        temporaryStateName = temporaryStateName.Remove(Mathf.Min(temporaryStateName.Length - 4, 18), 4).ToLower();

        bool nameAccepted = false;

        while(!nameAccepted)
        {
            bool isThereEqualsName = false;
            foreach(State s in currentStates)
            {
                if (s.idName == temporaryStateName)
                   {
                    temporaryStateName += "1";
                    isThereEqualsName = true;
                    break;
                   }
            }
            if (!isThereEqualsName)
                nameAccepted = true;
        }
        return temporaryStateName;
    }

    void CreateNewState(Texture2D stateTexture, Vector3 position, string stateName)
    {
        MovableState newMovableState = Instantiate(movableState, new Vector3(position.x, position.y, position.z - 1), Quaternion.identity) as MovableState;
        newMovableState.transform.SetParent(this.transform);
        
        newMovableState.setState(stateTexture, stateName);

        Vector2 pos = new Vector2(-1.2f, -0.8f);
        Vector2 size = new Vector2(1.2f, 0.8f);
        Rect r = new Rect(pos, size);
        newMovableState.setBoundraries(r);

        Debug.Log(newMovableState);

        currentStates.Add(newMovableState);
        SelectState(newMovableState);
        
        newMovableState.click = SelectState;
    }

    public void DestroyCurrentState()
    {
        selectedState.Erase();
        selectedState = null;
    }

    /*
     * UI Manipulation
     */

    public InputField nameInput;
    public ScrollView possibleConnections;

    private void SelectState(State state)
    {
        selectedState = null;
        foreach (MovableState mv in currentStates)
        {
            if (mv.Equals(state))
            {
                selectedState = mv;
                nameInput.text = mv.idName;
                break;
            }
        }
    }


    public void SaveMap()
    {
        string savingPath = EditorUtility.SaveFolderPanel("Select a folder to save your map into.", startPath, "RiskMap");
        
    }
}
