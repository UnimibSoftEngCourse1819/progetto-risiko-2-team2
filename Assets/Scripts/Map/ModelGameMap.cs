using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelGameMap : MonoBehaviour
{
	
	private const string nameFile = "";
	
	public ViewGameMap view;
	public DataManager dataManager;
    public MessageManager messageManager;
	private string firstland = "", secondland = "";
    private int tankAttacker;
    private string message = "";
    private string player = "";

    public void deploy(string nTank)
    {
        deploy(firstland, nTank);
    }

	private void deploy(string land, string tank)
    {
    	string error = dataManager.addTanks(land, int.Parse(tank));
    	if(error != null)
            view.showError(error);
        else
        {
            List<string> dataText = new List<string>();
            message = messageManager.messageDeploy(dataManager.getPlayer(), int.Parse(tank), land);
            view.updateLogEvent(messageManager.readDeploy(message));
            view.updateTextPlayerData(dataManager.getPlayerData());
            //un metodo che invii message agli altri utenti
        }
    }

    public void setTankAttacker(string tankAttacker)
    {
        this.tankAttacker = int.Parse(tankAttacker);
        message = messageManager.messageInitiateCombat(dataManager.getPlayer(), dataManager.getPlayerByLand(secondland), 
                                                        firstland, secondland,
                                                        int.Parse(tankAttacker));
        view.updateLogEvent (messageManager.readDeploy(message));
        //un metodo che invii message agli altri utenti
    }

    public void move(string tankDeploy)
    {
    	string error = dataManager.moveTanks(firstland, secondland, int.Parse(tankDeploy));
    	if(error != null)
            view.showError(error);
        else
        {
           message = messageManager.messageMove(dataManager.getPlayer(), firstland, secondland, int.Parse(tankDeploy));
           view.updateLogEvent(messageManager.readMove(message));
           view.updateTextPlayerData(dataManager.getPlayerData());
           //un metodo che invii message agli altri utenti
        }
    }

    public void nextPhase()
    {
        dataManager.nextPhase();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
        view.updatePhase(messageManager.readPhase(message));
        //un metodo che invii message agli altri utenti
    }

    public void startBattle(string tankDefender)
    {
        int startAttack = 0, startDefense = 0;
        string defenseOwner = ""; 
        if(firstland != null && secondland != null)
        {
            startAttack = dataManager.getTankOfLand(firstland);
            startDefense = dataManager.getTankOfLand(secondland);
            defenseOwner = dataManager.getPlayerByLand(secondland);
        }

        string error = dataManager.attack(firstland, secondland, tankAttacker, int.Parse(tankDefender));
        if(error != null)
            view.showError(error);
        else
        {
           string result = "";
           if(dataManager.getPlayerByLand(firstland).Equals(dataManager.getPlayerByLand(secondland)))
                result = "The Land has been conquered";
           else
                result = "The Land has not been conquered";
           message = messageManager.messageDefend(dataManager.getPlayerByLand(secondland) , dataManager.getPlayer(), 
                            secondland, firstland, startDefense - dataManager.getTankOfLand(secondland), startAttack - dataManager.getTankOfLand(firstland),
                            tankDefender, result);
           view.updateLogEvent(messageManager.readDefend(message));
           view.updateTextPlayerData(dataManager.getPlayerData(defenseOwner));
           //un metodo che invii message agli altri utenti
        }
    }

    public void pass(){
        dataManager.passTurn();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
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
        int startTank = dataManager.getPlayerTanksReinforcement(dataManager.getPlayer());
        string error = dataManager.useCards(card1, card2, card3);
        if(error != null)
            view.showError(error);
        else
        {
         message = messageManager.messageCard( dataManager.getPlayer(), dataManager.getPlayerTanksReinforcement(dataManager.getPlayer()) - startTank);
         view.updateLogEvent(messageManager.readCard(message));
         //un metodo che invii message agli altri utenti
        }
    }

    public void showCards()
    {
        List<string> cards = dataManager.getListCard();
        view.showCards(cards);
    }

    public void updateDeploy(string data)
    {
        view.updateLogEvent(messageManager.readDeploy(data));
        dataManager.addTanks(messageManager.getPlayer1(), messageManager.getNTank1());
    }

    public void updateMove(string data)
    {       
        view.updateLogEvent(messageManager.readMove(data));
        dataManager.moveTanks(messageManager.getLandStart(), messageManager.getLandEnd(), messageManager.getNTank1());
    }

    public void updateAttack(string data)
    {       
        view.updateLogEvent(messageManager.readInitiateCombat(data));
        if(messageManager.getPlayer2().Equals(player))
        {
            view.changeCanvasOption("Defend phase");
            view.updateTextPlayerData(dataManager.getPlayerData(player));
        }
    }

    public void updateDefense(string data)
    {       
        view.updateLogEvent(messageManager.readDefend(data));
        dataManager.attack(messageManager.getPlayer1(), messageManager.getPlayer2(), messageManager.getNTank1(), messageManager.getNTank2());
        if(messageManager.getPlayer1().Equals(player))
        {
            view.changeCanvasOption("Attack phase");
            view.updateTextPlayerData(dataManager.getPlayerData(player));
        }
    }

    public void updateTurn(string data)
    {       
        view.updatePhase(messageManager.readPhase(data));
        if(dataManager.getPlayer().Equals(messageManager.getPlayer1()))
            dataManager.nextPhase();
        else
            dataManager.passTurn();
        if(player.Equals(dataManager.getPlayer()))
        {
            view.changeCanvasOption(dataManager.getCurrentPhase());
        }
    }

    public void updateCards(string data)
    {
        view.updateLogEvent(messageManager.readCard(data));
        
    }
        
}