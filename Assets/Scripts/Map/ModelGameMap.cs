using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelGameMap : MonoBehaviour
{
	
	private const string nameFile = "";
	
	public ViewGameMap view;
	public DataManager data;
	private string lastAction ="Start";
	private string firstland = "", secondland = "";
    private int tankAttacker;

    public void deploy(string nTank)
    {
        deploy(firstland, nTank);
    }

	private void deploy(string land, string tank)
    {
    	string error = data.addTanks(land, int.Parse(tank));
    	if(error != null)
            view.showError(error);
        else
        {
            lastAction = "Deployed";
            updateView();
        }
    }

    public void setTankAttacker(string tankAttacker)
    {
        this.tankAttacker = int.Parse(tankAttacker);
    }

    public void move(string tankDeploy)
    {
    	string error = data.moveTanks(firstland, secondland, int.Parse(tankDeploy));
    	if(error != null)
            view.showError(error);
        else
        {
           lastAction = "Moved";
    	   updateView();
        }
    }

    public void nextPhase()
    {
        data.nextPhase();
        lastAction = "Phase skipped";
        updateView();
    }

    public void startBattle(string tankDefender)
    {
        string error = data.attack(firstland, secondland, tankAttacker, int.Parse(tankDefender));
        if(error != null)
            view.showError(error);
        lastAction = "Attacked";
        updateView();
    }

    public void pass(){
        data.passTurn();
        lastAction = "Passed";
        updateView();
    }

    private void updateView()
    {
     	List<string> data =	data.getDataForView();
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

    public void quit()
    {
        view.showConfirmQuit();
    }

    public void exit()
    {
        //chiude il game
    }

    public void closePopup()
    {
        view.closePopup();
    }

    public void closeCard()
    {
        view.closeCard();
    }

    public void useCards(string card1, string card2, string card3)
    {
        string error = data.useCards(card1, card2, card3);
        if(error != null)
            view.showError(error);
        lastAction = "useCards";
        updateView();
    }

    public void showCards()
    {
        List<string> cards = data.getListCard();
        view.showCards(cards);
    }

}