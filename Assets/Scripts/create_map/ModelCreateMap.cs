using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelCreateMap : MonoBehaviour
{
	/*
	MODIFICHE DA EFFETTUARE:
		->APPLICARE IL SINGLETON :
		 https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
		 dopo di chè aggiornare i file che la usano 
	*/

	//constant
	private const int MAX_NEIGHBORS = 10;
	//data
	private List<Land> lands = new List<Land>();//questo dato è ridodante su continents bisognerebbe toglierlo 
	private List<Continent> continents = new List<Continent>();

	//da cancellare/modificare quando viene applicato i singleton delle classi
	
	public ViewCreateMap view;
	public File_Creator_Controller file_controll;

	public bool createContinent(string name)
	{
		bool result = true;
		//manca il controllo
		continents.Add(new Continent(name));
		view.addContinentView(name);
		return result;
	}

	public bool createLand(string name, string continentOwner, string nameSprite)
	{
		bool result = true;
		//manca il controllo
		Land newLand = new Land(name, nameSprite);
		lands.Add(newLand);
		FindContinentByName(continentOwner).addLand(newLand);
		view.addLandView(newLand);
		return result;
	}

	public bool createRelation(string land1, string land2){
		bool result = true;
		//manca il controllo
		Land landLeft = FindLandByName(land1);
		Land landRight = FindLandByName(land2);
		landLeft.addNeighbor(landRight);
		landRight.addNeighbor(landLeft);
		return result;
	}

	public void loadLandInfo(string nameLand){//crea una stringa text con scritto tutte le caratteristica dello stato
		string text = "";
		Land land = FindLandByName(nameLand);

		// manca la contruzione del testo
		/*
			un esempio può essere(Francia):
			Nome : Francia
			Continente : Europa
			Stati confinanti :
			Spagna, Regno Unito, Paesi Bassi, Germania-Svizzera, Germania, Italia
		*/
		view.changeLandInfo(text);
	}

	public void createFile(string name)
	{
		if(file_controll.CreateFileMap(name, continents))
		{
			//view.popupSuccefull();
		}
	}

	//metodi ausiliari

	private Continent FindContinentByName(string name)
	{
		Continent result = null;
		foreach (Continent continent in continents)
		{
			if(continent.getName() == name)
			{
				result = continent;
			}
		}
		return result;
	}

	private Land FindLandByName(string name)
	{
		Land result = null;
		foreach (Land land in lands)
		{
			if(land.getName() == name)
			{
				result = land;
			}
		}
		return result;
	}

}
