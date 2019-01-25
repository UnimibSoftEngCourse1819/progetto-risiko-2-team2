using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class File_Creator_Controller : MonoBehaviour
{
    /*
        -Applicare il design Pattern Singleton siccome ne serve solo 1
    */
    private const string ENDCONTINENT = "END CONTINENT";
    private const string ENDLIST="START NEIGHBORS RELATION";
    private const string ENDFILE="END";
    private StreamWriter file;

    public bool CreateFileMap(string name,  List<Continent> continents)
    {
        bool result = true; //true è stato correttamente creato il file, false c'è stato un errore (ad esempio non è stato trovato il file)        
        file = new StreamWriter(name + ".txt");
        writeComponentList(continents);
        writeRelationList(continents);
        file.WriteLine(ENDFILE);
        file.Close();
        return result;
    }

    private void writeComponentList(List<Continent> continents)//scrivo la lista dei continenti e gli stati a lui appartenenti
    {
        foreach (Continent continent in continents)// scrivo i dati riguardanti 1 continente
        {
            file.WriteLine(continent.getName());
            List<Land> lands = continent.getLands();
            foreach(Land land in lands)//scrivo la lista degli stati che appartengono al continente
            {
                file.WriteLine(land.getName() + " " + land.getNameSprite());
            }
            file.WriteLine(ENDCONTINENT);
        }

        file.WriteLine(ENDLIST);
    }

    private void writeRelationList(List<Continent> continents)
    {
        foreach (Continent continent in continents)
        {
            List<Land> landsOfContinent = continent.getLands();
            foreach(Land landLeft in landsOfContinent)//prendo 1 stato (landLeft)
            {
                List<Land> landsOfLand = landLeft.getNeighbors();
                foreach(Land landRight in landsOfLand)// prendo 1 stato che è vicino di landLeft
                {
                   file.WriteLine(landLeft.getName() + " " + landRight.getName());
                } 
            }
        }
    }
}