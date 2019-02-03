using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts.Server;

public class ModelGameMap : MonoBehaviour
{
	
	private const string nameFile = "";
    private const int INITIAL_TANKS_DEPLOY = 3;

    public ControllGameMap controller;
	public DataManager dataManager;
    public MessageManager messageManager;
    public MapLoader loader;
	private string firstLand = "", secondLand = "";
    private int tankAttacker;
    private string message = "";
    private string player = "";
    private bool started = true;
    private int countStartDeploy;

    public string getFirstLand()
    {
        return firstLand;
    }

    public string getSecondLand()
    {
        return secondLand;
    }

    public string getPlayer()
    {
        return player;
    }

    public bool getStarted()
    {
        return started;
    }

    public void deploy(string nTank)
    {
        deploy(firstLand, nTank);
    }

	private void deploy(string land, string tank)
    {
    	string error = dataManager.addTanks(land, int.Parse(tank));
    	if(error != null)
            controller.showError(error);
        else
        {
            if(!started)
            {
                message = messageManager.messageDeploy(dataManager.getPlayer(), int.Parse(tank), land);
                controller.updateLogEvent(messageManager.readDeploy(message));
                controller.updateTextPlayerData(dataManager.getPlayerData());
                DataSender.SendPosizionamento(message);
            }
            else
            {
                countStartDeploy -= int.Parse(tank);
                if (countStartDeploy != 0)
                {
                    controller.updateDeployRemain(countStartDeploy);
                }
                else
                {
                    if(dataManager.isAllPlayerRunOutOfTanks())
                    {
                        started = false;
                        startRealGame();
                    }
                    else
                    {
                        nextDeploy();
                    }
                }
            }
        }
    }

    private void localMode()
    {
        player = dataManager.getPlayer();
    }

    private void startRealGame()
    {
        started = false;
        //dataManager.startGame();
        localMode();
        prepareViewDeploy();
    }

    private void nextDeploy()
    {
        dataManager.nextPlayer();
        while(dataManager.getPlayerTanksReinforcement(dataManager.getPlayer()) == 0)
            dataManager.nextPlayer();
        localMode();
    } 

    public void setTankAttacker(string tankAttacker)
    {
        this.tankAttacker = int.Parse(tankAttacker);
        message = messageManager.messageInitiateCombat(dataManager.getPlayer(), dataManager.getPlayerByLand(secondLand), 
                                                        firstLand, secondLand,
                                                        int.Parse(tankAttacker));
        controller.updateLogEvent(messageManager.readDeploy(message));
        DataSender.SendAttackDeclared(message);
    }

    public void move(string tankDeploy)
    {
    	string error = dataManager.moveTanks(firstLand, secondLand, int.Parse(tankDeploy));
    	if(error != null)
            controller.showError(error);
        else
        {
            message = messageManager.messageMove(dataManager.getPlayer(), firstLand, secondLand, int.Parse(tankDeploy));
            controller.updateLogEvent(messageManager.readMove(message));
            controller.updateTextPlayerData(dataManager.getPlayerData());
            DataSender.SendSpostamento(message);
        }
    }

    public void nextPhase()
    {
        dataManager.nextPhase();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
        controller.updatePhase(messageManager.readPhase(message));
        //un metodo che invii message agli altri utenti
        firstLand = null;
        secondLand = null;
    }

    public void nomiPlayer()  // prende  i nomi e i colori
    {


    }

    public void startBattle(string tankDefender)
    {
        int startAttack = 0, startDefense = 0;
        string defenseOwner = ""; 
        if(firstLand != null && secondLand != null)
        {
            startAttack = dataManager.getTankOfLand(firstLand);
            startDefense = dataManager.getTankOfLand(secondLand);
            defenseOwner = dataManager.getPlayerByLand(secondLand);
        }

        string error = dataManager.attack(firstLand, secondLand, tankAttacker, int.Parse(tankDefender));
        if(error != null)
            controller.showError(error);
        else
        {
            string result = "";
            if(dataManager.getPlayerByLand(firstLand).Equals(dataManager.getPlayerByLand(secondLand)))
                result = "The Land has been conquered";
            else
                result = "The Land has not been conquered";
            message = messageManager.messageDefend(dataManager.getPlayerByLand(secondLand) , dataManager.getPlayer(), 
                            secondLand, firstLand, startDefense - dataManager.getTankOfLand(secondLand), startAttack - dataManager.getTankOfLand(firstLand),
                            tankDefender, result);

            controller.updateLogEvent(messageManager.readDefend(message));
            controller.updateTextPlayerData(dataManager.getPlayerData(defenseOwner));
            DataSender.SendAttacco(message);
        }
    }

    public void pass(){
        dataManager.passTurn();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
        controller.updatePhase(messageManager.readPhase(message));
        DataSender.SendPasso(message);
    }

	private void Awake()
	{
        controller.prepareView();
		loadData();
        NetworkManager.istance.InizializzaModel();        
	}

	private void loadData()//inizializza i dei dati su cui ci si può testare la parte logica
	{
        Debug.Log("CIAONE");

        MapData data = loader.loadMap();
        controller.drawMap(data.actualStates);

        List<Player> players = new List<Player>();

        players.Add(new Player("Pippo"));

        players.Add(new Player("Paperino"));

        players.Add(new Player("Topolino"));

        List<Continent> world = loader.getWorld(data);

        Debug.Log("Nea and the pussycats");

        dataManager = new DataManager(players, world, loader.getAllLands(world));

        controller.updateTextPlayerData(dataManager.getPlayer());

        string phase = dataManager.getCurrentPhase();
        countStartDeploy = INITIAL_TANKS_DEPLOY;

        Debug.Log("Initiate cloack mode");

        controller.changeCanvasOption(phase);
        controller.updatePhase(phase + System.Environment.NewLine + dataManager.getPlayer());
        controller.updateDeployRemain(countStartDeploy);
        controller.updateSelected(1, "Select a State !!!", null);
        localMode();

        Debug.Log("The cool killers' club");
    }

    public void quit()
    {
        controller.handleButtonClicked("Quit");
    }

    public void exit()
    {
        controller.handleButtonClicked("Exit");
    }

    public void closePopup()
    {
        controller.handleButtonClicked("Popup");
    }

    public void closeCard()
    {
        controller.handleButtonClicked("Card");
    }

    public void useCards(string card1, string card2, string card3)
    {
        int startTank = dataManager.getPlayerTanksReinforcement(dataManager.getPlayer());
        string error = dataManager.useCards(card1, card2, card3);
        if(error != null)
            controller.showError(error);
        else
        {
         message = messageManager.messageCard( dataManager.getPlayer(), dataManager.getPlayerTanksReinforcement(dataManager.getPlayer()) - startTank);
         controller.updateLogEvent(messageManager.readCard(message));
            DataSender.SendComboCarte(message);
        }
    }

    public void showCards()
    {
        List<string> cards = dataManager.getListCard();
        controller.showCards(cards);
    }

    public void updateDeploy(string data)
    {
        controller.updateLogEvent(messageManager.readDeploy(data));
        dataManager.addTanks(messageManager.getPlayer1(), messageManager.getNTank1());
    }

    public void updateMove(string data)
    {       
        controller.updateLogEvent(messageManager.readMove(data));
        dataManager.moveTanks(messageManager.getLandStart(), messageManager.getLandEnd(), messageManager.getNTank1());
    }

    public void updateAttack(string data)
    {       
        controller.updateLogEvent(messageManager.readInitiateCombat(data));
        if(messageManager.getPlayer2().Equals(player))
        {
            controller.changeCanvasOption("Defend phase");
            controller.updateTextPlayerData(dataManager.getPlayerData(player));
        }
    }

    public void updateDefense(string data)
    {       
        controller.updateLogEvent(messageManager.readDefend(data));
        dataManager.attack(messageManager.getPlayer1(), messageManager.getPlayer2(), messageManager.getNTank1(), messageManager.getNTank2());
        if(messageManager.getPlayer1().Equals(player))
        {
            controller.changeCanvasOption("Attack phase");
            controller.updateTextPlayerData(dataManager.getPlayerData(player));
        }
    }

    public void updateTurn(string data)
    {       
        controller.updatePhase(messageManager.readPhase(data));
        if(dataManager.getPlayer().Equals(messageManager.getPlayer1()))
            dataManager.nextPhase();
        else
            dataManager.passTurn();
        if(player.Equals(dataManager.getPlayer()))
        {
            controller.changeCanvasOption(dataManager.getCurrentPhase());
        }
    }

    public void updateCards(string data)
    {
        controller.updateLogEvent(messageManager.readCard(data));
        
    }

    public void setClicked(string continent)
    {
        if(dataManager.getPlayer().Equals(dataManager.getPlayerByLand(continent)))
            firstLand = continent;
        else 
            secondLand = continent;
        controller.updateLandText(dataManager.getLandData(continent));
        controller.updateSelected(2, firstLand, secondLand);
    }

    private void prepareViewDeployStart()
    {
        controller.updateSelected(1, "Select a State !!", null);
        int remainTanks = INITIAL_TANKS_DEPLOY;
        if(dataManager.getPlayerTanksReinforcement(dataManager.getPlayer()) < INITIAL_TANKS_DEPLOY)
            remainTanks = dataManager.getPlayerTanksReinforcement(player);
        controller.updateDeployRemain(remainTanks);
    }

    private void prepareViewDeploy()
    {
        controller.updateSelected(1, "Select a State !!", null);
        controller.updateDeployRemain(dataManager.getPlayerTanksReinforcement(player));
    }

    private void prepareViewAttack()
    {
        controller.updateSelected(2, "", "");
    }

    private void prepareViewDefense()
    {

    }

    private void prepareViewMove()
    {
        controller.updateSelected(2, "", "");
    }

    private void prepareViewWaiting()
    {
        
    }
}