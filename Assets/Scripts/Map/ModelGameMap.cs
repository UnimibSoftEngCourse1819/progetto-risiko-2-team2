using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelGameMap : MonoBehaviour
{
	
	private const string nameFile = "";
	
	public ViewGameMap view;
	public GameManager manager;
	private string lastAction ="Start";
	private string firstland = "", secondland = "";

	public void deploy(string land, string tank)
    {
    	manager.addTroup(land, int.Parse(tank));
    	lastAction = "Deployed";
    	updateView();
    }

    public void attack(string landAttacker , string landDefender, string tankAttacker, string tankDefender)
    {
    	manager.attack(landAttacker, landDefender, int.Parse(tankAttacker), int.Parse(tankDefender));
    	lastAction = "Attacked";
    	updateView();
    }

    public void move(string landStart, string landEnd, string tank_deploy)
    {
    	manager.move(landStart, landEnd, int.Parse(tank_deploy));
    	lastAction = "Moved";
    	updateView();
    }

    public void pass(){
        manager.passTurn();
        lastAction = "Passed";
        updateView();
    }

    private void updateView()
    {
    	List<string> data =	manager.getDataForView();
    	data.Add(lastAction);
    	view.updateText(data);
    }


	private void Awake()
	{
		loadData();
	}

	private void loadData()//inizializza i dei dati su cui ci si può testare la parte logica
	{
		/*
		//carica i dati
		FileLoader file;
		file.read(nameFile);
		manager = new GameManager(players, file.getWorldData());
		*/

		updateView();
	}

	private List<string> getOptions(List<Continent> World)
	{
		List<string> options = new List<string>();
		foreach(Continent continent in World)
		{
			List<Land> lands = continent.getLands();
			foreach(Land land in lands)
			{
				options.Add(land.getName());
			}
		}

		return options;
	}

	private void createRelation(Land landLeft, Land landRight){
		//manca il controllo
		landLeft.addNeighbor(landRight);
		landRight.addNeighbor(landLeft);
	}

}
