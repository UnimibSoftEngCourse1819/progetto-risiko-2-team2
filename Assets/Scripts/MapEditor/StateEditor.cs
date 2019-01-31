using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using PopupWindow = UnityEditor.PopupWindow;
using Toggle = UnityEngine.UI.Toggle;

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
    private State selectedState;

    // Starting directory for the load and save Panels
    private string startPath = "C://";
    
    public GameObject possibleConnections;

    public GameObject continentsHolder;
    private List<string> continents;

    MapLoader mapLoader;

    private void Awake()
    {
        continents = new List<string>();

        int i = 0;

        foreach (RectTransform gb in continentsHolder.GetComponentInChildren<RectTransform>())
        {
            InputField contText = gb.GetChild(0).GetComponent<InputField>();
            Debug.Log(contText);

            contText.text = i.ToString();
            continents.Add(i.ToString());

            contText.onEndEdit.AddListener((string name) =>
            {
                ChangeContinentName(contText.text, int.Parse(gb.name));
            });

            Toggle contToggle = gb.GetChild(1).GetComponent<Toggle>();

            contToggle.onValueChanged.AddListener((bool value) =>
            {
                AssignCurrentStateToContinent(contText.text, value, int.Parse(gb.name));
            });

            i++;
        }
        
        foreach (RectTransform connector in possibleConnections.GetComponentInChildren<RectTransform>())
        {
            InputField connText = connector.GetComponent<InputField>();
            

            connText.onEndEdit.AddListener((string name) =>
            {
                CreateNewStateConnection(name);
                UpdateConnection();
            });
        }

        currentStates = new List<MovableState>();
        mapLoader = new MapLoader();
    }

    private void UpdateConnection()
    {
        bool changeConnection = true;

        for (int i = 0; i < possibleConnections.transform.GetChildCount(); i++)
        {
            InputField connectionField = possibleConnections.transform.GetChild(i).GetComponent<InputField>();
            if (connectionField.text != "" && GetState(connectionField.text) == null)
                changeConnection = false;
        }
        if (changeConnection)
        {
            selectedState.connections = new List<string>();
            for (int i = 0; i < possibleConnections.transform.GetChildCount(); i++)
            {
                InputField connectionField = possibleConnections.transform.GetChild(i).GetComponent<InputField>();
                if (connectionField.text != "")
                    selectedState.connections.Add(connectionField.text);

            }
        }
        else
        {
            int i = 0;
            foreach (string conn in selectedState.connections)
            {
                if (i < possibleConnections.transform.GetChildCount())
                {
                    if (conn != null || conn != "")
                        possibleConnections.transform.GetChild(i).GetComponent<InputField>().text = conn;
                }
                i++;
            }
        }
    }

    private void ChangeContinentName(string continentName, int index)
    {

        bool canChangeName = true;

        if (continentName == "")
            canChangeName = false;

        foreach (string s in continents)
        {
            if (continentName == s)
                canChangeName = false;
        }
        if (canChangeName)
        {
            foreach (State s in currentStates)
            {
                if (s.continent == continents[index])
                {
                    s.continent = continentName;
                }
            }
            continents[index] = continentName;
        }
        else
        {
            int i = 0;

            foreach(RectTransform gb in continentsHolder.GetComponentInChildren<RectTransform>())
            {
                gb.GetChild(0).GetComponent<InputField>().text = continents[i];
                i++;
            }
        }
    }

    private void AssignCurrentStateToContinent(string id, bool value, int index)
    {
        if (value)
        {
            foreach (RectTransform gb in continentsHolder.GetComponentInChildren<RectTransform>())
            {
                if (gb.GetChild(0).GetComponent<InputField>().text != id)
                {
                    Toggle t = gb.GetChild(1).GetComponent<Toggle>();
                    t.isOn = false;
                }
            }
            if (selectedState != null)
                selectedState.continent = continents[index];
        }
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

    MovableState CreateNewState(Texture2D stateTexture, Vector3 position, string stateName)
    {
        MovableState newMovableState = Instantiate(movableState, new Vector3(position.x, position.y, position.z - 1), Quaternion.identity) as MovableState;
        newMovableState.transform.SetParent(this.transform);

        newMovableState.SetState(stateTexture, stateName);

        Vector2 pos = new Vector2(-6.5f, -4.5f);
        Vector2 size = new Vector2(8f, 7.5f);
        Rect r = new Rect(pos, size);
        newMovableState.setBoundraries(r);
        

        currentStates.Add(newMovableState);
        SelectState(newMovableState);

        newMovableState.Click.AddListener((State state) =>
        {
            SelectState(state);
        });

        return newMovableState;
    }

    public void DestroyCurrentState()
    {
        string connectionToErase = selectedState.idName;
        selectedState.Erase();
        selectedState = null;

        foreach(MovableState ms in currentStates)
        {
            ms.connections.Remove(connectionToErase);
        }
    }

    /*
     * UI Manipulation
     */

    public InputField nameInput;

    public GameObject connectionButton;

    public void EraseCurrentMap()
    {
        foreach(MovableState ms in currentStates)
        {
            ms.Erase();
        }

        selectedState = null;
        currentStates.Clear();
    }

    public void CreateNewStateConnection(string conn)
    {
        MovableState stateToConnect = GetState(conn);

        if (selectedState != null && stateToConnect != null && stateToConnect != selectedState)
        {
            selectedState.connections.Add(stateToConnect.idName);
            stateToConnect.connections.Add(selectedState.idName);
        }


        showConnection();
    }

    public MovableState GetState(string stateName)
    {
        MovableState stateToReturn = null;

        foreach(MovableState s in currentStates)
        {
            if (s.idName == stateName)
            {
                stateToReturn = s;
                break;
            }
        }

        return stateToReturn;
    }

    public void SelectState(State state)
    {
        selectedState = state;

        nameInput.text = state.idName;

        showConnection();

        ShowContinentOfSelectedState();
    }

    private void ShowContinentOfSelectedState()
    {
        foreach (RectTransform gb in continentsHolder.GetComponentInChildren<RectTransform>())
        {
            

            if (gb.GetChild(0).GetComponent<InputField>().text == selectedState.continent)
            {
                Toggle t = gb.GetChild(1).GetComponent<Toggle>();
                t.isOn = true;
            }
            else
            {
                Toggle t = gb.GetChild(1).GetComponent<Toggle>();
                t.isOn = false;
            }
        }
    }

    public void ChangeNameCurrentState()
    {
        if (selectedState != null)
        {
            foreach(State s in currentStates)
            {
                if(s.idName == nameInput.text)
                {
                    nameInput.text = selectedState.idName;
                    return;
                }
            }

            foreach(State s in currentStates)
            {
                if (s.connections.Contains(selectedState.idName))
                {
                    s.connections[s.connections.IndexOf(selectedState.idName)] = nameInput.text;
                }
            }
            selectedState.idName = nameInput.text;
        }
    }

    public void showConnection()
    {
        int i = 0;
        for(i = 0; i < possibleConnections.transform.GetChildCount(); i++)
            possibleConnections.transform.GetChild(i).GetComponent<InputField>().text = "";

        i = 0;
        foreach (string connection in selectedState.connections)
        {
            Debug.Log(connection);
            if(i < 4)
            {
                possibleConnections.transform.GetChild(i).GetComponent<InputField>().text = connection;
            }
            i++;
        }
    }

    public Text errorLog;
    
    public void SaveNewMap()
    {
        List<string> continentCheck = new List<string>();

        foreach(State s in currentStates)
        {
            if (s.connections.Count() == 0)
            {
                errorLog.text = "ERROR: State " + s.idName + " isn't connected to anything!";
                return;
            }

            if (s.continent == null || s.continent == "")
            {
                errorLog.text = "ERROR: State " + s.idName + " isn't assigned to a continent!";
                return;
            }

            if (!continentCheck.Contains(s.continent))
                continentCheck.Add(s.continent);
        }

        if (continentCheck.Count < 6)
        {
            errorLog.text = "ERROR: A continent isn't used!";
            return;
        }

        errorLog.text = "All good! Saving...";

        mapLoader.SaveMap(continents, currentStates);
    }

    public void LoadNewMap()
    {
        MapData mapToLoad = mapLoader.LoadMap();

        if (mapToLoad != null)
        {
            EraseCurrentMap();

            foreach(StateData sd in mapToLoad.actualStates)
            {
                MovableState ms = CreateNewState(sd.texture, sd.positionInMap, sd.stateName);
                ms.continent = sd.continentName;

                foreach(string conn in sd.connection)
                {
                    ms.connections.Add(conn);
                }
            }
        }
    }

}
