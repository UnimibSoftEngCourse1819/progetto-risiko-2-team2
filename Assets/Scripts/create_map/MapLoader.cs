using System.Collections;
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

    public MapData loadMap()
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

    public List<Continent> getWorld(MapData data)
	{
		List<StateData> statesData = data.getWorldData();
		List<Continent> realWorld = new List<Continent>();
		List<string> continentData = data.getContinents();

        //creating continents
        foreach (string continentName in continentData)
		{
			Continent continentBuffer = new Continent(continentName);
			realWorld.Add(continentBuffer);
            Debug.Log("Continent added " + continentName);
        }

		//creating lands
		foreach(StateData stateData in statesData)
		{
			Land landBuffer = new Land(stateData.getName(), 1);
			foreach(Continent continent in realWorld)
			{
                if (continent.getName().Equals(stateData.getContinent()))
                {
                    continent.addLand(landBuffer);
                    Debug.Log("Land added " + continent.getName());
                }
            } 
		}

		//making the connection
		foreach(StateData stateData in statesData)
		{
			Land landBuffer = getLandFromWorld(realWorld, stateData.getName());
			string[] neighbors = stateData.getConnection();
			for(int i = 0; i < neighbors.Length; i++)
			{
				Land neighborBuffer = getLandFromWorld(realWorld, neighbors[i]);
                Debug.Log("Test " + neighborBuffer.getName() + "-" + landBuffer.getName());
                landBuffer.addNeighbor(neighborBuffer);
			}
		}
		return realWorld;
	}

	private Land getLandFromWorld(List<Continent> world, string nameLand)
	{
		Land result = null;
		foreach(Continent continent in world)
		{
			Land landBuffer = null;
			landBuffer = continent.getLand(nameLand);
			if(landBuffer != null)
				result = landBuffer;
		}
		return result;
	}

    public List<Land> getAllLands(List<Continent> world)
    {
        List<Land> lands = new List<Land>();
        foreach (Continent continent in world)
        {
            foreach (Land l in continent.getLands())
                lands.Add(l);
        }
        return lands;
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

    public List<StateData> getWorldData()
    {
    	return actualStates;
    }

    public List<string> getContinents()
    {
    	return continents;
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
        this.connection = state.connections.ToArray();
        this.texture = state.text;
    }

    public string getName()
    {
    	return stateName;
    }

    public string[] getConnection()
    {
    	return connection;
    }

    public string getContinent()
    {
    	return continentName;
    }

    public Texture2D getTexture()
    {
    	return texture;
    }

    public Vector3 getVector()
    {
    	return positionInMap;
    }
}