/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public MovableState movableState;

    private string startPath = "C://";

    public MapData currentMap;

    
    public void SaveMap(List<string> continents, List<MovableState> currentStates)
    {
        // Save the path where to save the map in the user pc
        string savingPath = EditorUtility.SaveFolderPanel("Select a folder to save your map into.", startPath, "RiskMap");

        string texturePath = savingPath + "/textures";

        Directory.CreateDirectory(texturePath);

        // Create a MapData Object and several StateData Object to store the data of the map
        MapData map = new MapData();

        map.continents = continents;

        foreach (State state in currentStates)
        {
            StateData sd = new StateData(state);
            
            map.states.Add(JsonUtility.ToJson(sd));
            

            byte[] textureBytes = state.text.EncodeToPNG();

            File.WriteAllBytes(texturePath + "/" + sd.stateName + ".png", textureBytes);
        }

        // Create a Json File with the data of the map
        File.WriteAllText(savingPath + "/map.json", JsonUtility.ToJson(map));
    }

    public MapData LoadMap()
    {
        string loadPath = EditorUtility.OpenFolderPanel("Select a folder to load your map.", startPath, "json");

        if (File.Exists(loadPath + "/map.json"))
        {


            string fileContent = File.ReadAllText(loadPath + "/map.json");

            MapData md = JsonUtility.FromJson<MapData>(fileContent);

            foreach (string s in md.states)
            {
                StateData stateData = JsonUtility.FromJson<StateData>(s);

                WWW www = new WWW("file:///" + loadPath + "/textures/" + stateData.stateName + ".png");

                stateData.texture = www.texture;

                md.actualStates.Add(stateData);
            }


            currentMap = md;

            
        }
        return currentMap;
    }
}


public class MapData
{
    public List<string> states;
    public List<StateData> actualStates;

    public List<string> continents;

    public MapData()
    {
        states = new List<string>();
        continents = new List<string>();
        actualStates = new List<StateData>();
    }

}

public class StateData
{
    public string stateName;
    public Vector3 positionInMap;
    public string continentName;
    public string[] connection;
    public Texture2D texture;

    public StateData(State state)
    {
        this.stateName = state.idName;
        this.positionInMap = state.transform.position;
        this.continentName = state.continent;
        connection = state.connections.ToArray();
        texture = state.text;
    }
}

*/