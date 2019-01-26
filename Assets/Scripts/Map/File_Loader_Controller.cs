using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class File_Loader_Controller : MonoBehaviour
{
   
    /*
        questo file si occupa di caricare i SOLAMENTE I COMPONENTI della mappa
        quindi se bisogna modificare/aggiungere/togliere dovremmo toccare 
        SOLO QUESTO FILE 
    */
    private const string ENDCONTINENT = "END CONTINENT";
    private const string ENDLIST="START NEIGHBORS RELATION";
    private const string ENDFILE="END";
    private StreamReader reader;
    private List<Continent> data = new List<Continent>();

    public List<Continent> LoadDataMap(string name)
    {
        readFile(name);
        return data;
    }

    private void readFile(string name)
    {
        if (File.Exists(name)) // file trovato ?
        {
            Debug.Log("file trovato");
            reader = new StreamReader(name);
            readComponentList();
            readRelationList();

            reader.Close(); // chiudo il file
        }
        else  // file non trovato
        {
            Debug.Log("file non trovato");
        }
    }

    private void readComponentList()
    {
        string buffer = reader.ReadLine();//prendo la prima riga (che è sicuramente il nome di un continente)
        do
        {   
            //lettura 1 continente
            Continent continent = new Continent(buffer);
            buffer = reader.ReadLine();
            while(!buffer.Equals(ENDCONTINENT))//lettura di tutti gli stati del continente
            {
                string nameLand = buffer.Split(' ')[0];//nome della land
                string sprite  = buffer.Split(' ')[1];//nome dello sprite
                Land land = new Land(nameLand, sprite);
                continent.addLand(land);
                buffer = reader.ReadLine();
            }
            data.Add(continent);
        }while(!buffer.Equals(ENDLIST));
    }
    
    private void readRelationList()
    {
        string buffer = reader.ReadLine();
        while(!buffer.Equals(ENDFILE))
        {
            Land landLeft = getLand(buffer.Split(' ')[0]);
            Land landRight = getLand(buffer.Split(' ')[1]);
            landLeft.addNeighbor(landRight);
            landRight.addNeighbor(landLeft);
        }
    }

    private Land getLand(string name)
    {
        Land result = null;
        foreach(Continent continent in data)
        {
            List<Land> lands = continent.getLands();
            foreach(Land land in lands)
            {
                if(name.Equals(land.getName()))
                    result = land;
            }
        }
        return result;
    }
}