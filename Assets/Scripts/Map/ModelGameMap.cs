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
    public MessageManager messageManager;
	private string firstland = "", secondland = "";
    private int tankAttacker;
    private string message = "";

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
            List<string> dataText = new List<string>();
            message = messageManager.messageDeploy(data.getPlayer(), int.Parse(tank), land);
            view.updateLogEvent(messageManager.readDeploy(message));
            view.updateTextPlayerData(data.getPlayerData());
            //un metodo che invii message agli altri utenti
        }
    }

    public void setTankAttacker(string tankAttacker)
    {
        this.tankAttacker = int.Parse(tankAttacker);
        message = messageManager.messageInitiateCombat(data.getPlayer(), data.getPlayerByLand(secondland), 
                                                        firstland, secondland,
                                                        int.Parse(tankAttacker));
        view.updateLogEvent (messageManager.readDeploy(message));
        //un metodo che invii message agli altri utenti
    }

    public void move(string tankDeploy)
    {
    	string error = data.moveTanks(firstland, secondland, int.Parse(tankDeploy));
    	if(error != null)
            view.showError(error);
        else
        {
           message = messageManager.messageMove(data.getPlayer(), firstland, secondland, int.Parse(tankDeploy));
           view.updateLogEvent(messageManager.readMove(message));
           view.updateTextPlayerData(data.getPlayerData());
           //un metodo che invii message agli altri utenti
        }
    }

    public void nextPhase()
    {
        data.nextPhase();
        message = messageManager.messagePhase(data.getPlayer(), data.getCurrentPhase());
        view.updatePhase(messageManager.readPhase(message));
        //un metodo che invii message agli altri utenti
    }

    public void startBattle(string tankDefender)
    {
        int startAttack = 0, startDefense = 0;
        string defenseOwner = ""; 
        if(firstland != null && secondland != null)
        {
            startAttack = data.getTankOfLand(firstland);
            startDefense = data.getTankOfLand(secondland);
            defenseOwner = data.getPlayerByLand(secondland);
        }

        string error = data.attack(firstland, secondland, tankAttacker, int.Parse(tankDefender));
        if(error != null)
            view.showError(error);
        else
        {
           string result = "";
           if(data.getPlayerByLand(firstland).Equals(data.getPlayerByLand(secondland)))
                result = "The Land has been conquered";
           else
                result = "The Land has not been conquered";
           message = messageManager.messageDefend(data.getPlayerByLand(secondland) , data.getPlayer(), 
                            secondland, firstland, startDefense - data.getTankOfLand(secondland), startAttack - data.getTankOfLand(firstland),
                            tankDefender, result);
           view.updateLogEvent(messageManager.readDefend(message));
           view.updateTextPlayerData(data.getPlayerData(defenseOwner));
           //un metodo che invii message agli altri utenti
        }
    }

    public void pass(){
        data.passTurn();
        message = messageManager.messagePhase(data.getPlayer(), data.getCurrentPhase());
        view.updatePhase(messageManager.readPhase(message));
        //un metodo che invii message agli altri utenti
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
        e aggiona l'interfaccia
		*/

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
        int startTank = data.getPlayerTanksReinforcement(data.getPlayer());
        string error = data.useCards(card1, card2, card3);
        if(error != null)
            view.showError(error);
        else
        {
         message = messageManager.messageCard( data.getPlayer(), data.getPlayerTanksReinforcement(data.getPlayer()) - startTank);
         view.updateLogEvent(messageManager.readCard(message));
         //un metodo che invii message agli altri utenti
        }
    }

    public void showCards()
    {
        List<string> cards = data.getListCard();
        view.showCards(cards);
    }
    public void AggiornaSpostamento(string s)
    {       
        view.updateLogEvent(messageManager.readDeploy(s));
    }
        
}