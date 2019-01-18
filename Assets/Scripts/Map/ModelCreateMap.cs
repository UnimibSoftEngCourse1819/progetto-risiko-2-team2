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
		 http://csharpindepth.com/articles/general/singleton.aspx (implementate la quarta versione che è il più semplice)
		 dopo di chè aggiornare i file che la usano 
	*/

	//constant
	private const int MAX_NEIGHBORS = 10;
	//data
	private List<Land> lands = new List<Land>();
	private List<Continent> continents = new List<Continent>();

	//da cancellare/modificare quando viene applicato i singleton delle classi
	public ControllCreateMap controll;
	public ViewCreateMap view;

	public bool createContinent(string name)
	{
		bool result = true;
		//manca il controllo
		continents.Add(new Continent(name));
		view.addContinentView(name);
		return result;
	}

	public bool createLand(string name, string continentOwner)
	{
		bool result = true;
		//manca il controllo
		Land newLand = new Land(name);
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

	public void loadLandInfo(string nameLand){
		string text = "";
		Land land = FindLandByName(nameLand);

		// manca la contruzione del testo (sarebbe bello che viene fatta dal metodo toString del Land)

		view.changeLandInfo(text);
	}


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
