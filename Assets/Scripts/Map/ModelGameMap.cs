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
	
	public ViewGameMap view;
	public DataManager dataManager;
    public MessageManager messageManager;
    public MapLoader loader;
	private string firstland = "", secondland = "";
    private int tankAttacker;
    private string message = "";
    private string player = "";
    private bool started = true;
    private int countStartDeploy;

    public bool getStarted()
    {
        return started;
    }

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
            if(!started)
            {
                message = messageManager.messageDeploy(dataManager.getPlayer(), int.Parse(tank), land);
                view.updateLogEvent(messageManager.readDeploy(message));
                view.updateTextPlayerData(dataManager.getPlayerData());
                DataSender.SendPosizionamento(message);
            }
            else
            {
                countStartDeploy -= int.Parse(tank);
                if (countStartDeploy != 0)
                {
                    view.updateDeployRemain("" + countStartDeploy);
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
        dataManager.startGame();
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
        message = messageManager.messageInitiateCombat(dataManager.getPlayer(), dataManager.getPlayerByLand(secondland), 
                                                        firstland, secondland,
                                                        int.Parse(tankAttacker));
        view.updateLogEvent (messageManager.readDeploy(message));
        DataSender.SendAttackDeclared(message);
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
            DataSender.SendSpostamento(message);
        }
    }

    public void nextPhase()
    {
        dataManager.nextPhase();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
        view.updatePhase(messageManager.readPhase(message));
        //un metodo che invii message agli altri utenti
        firstland = null;
        secondland = null;
    }

    public void NomiPlayer()  // prende  i nomi e i colori
    {


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
            DataSender.SendAttacco(message);
        }
    }

    public void pass(){
        dataManager.passTurn();
        message = messageManager.messagePhase(dataManager.getPlayer(), dataManager.getCurrentPhase());
        view.updatePhase(messageManager.readPhase(message));
        DataSender.SendPasso(message);
    }

	private void Awake()
	{
        view.prepareView();
		loadData();
        NetworkManager.istance.InizializzaModel();        
	}

	private void loadData()//inizializza i dei dati su cui ci si può testare la parte logica
	{
        Debug.Log("CIAONE");

        MapData data = loader.loadMap();
        view.drawMap(data.actualStates);

        List<Player> players = new List<Player>();

        players.Add(new Player("Pippo"));

        players.Add(new Player("Paperino"));

        players.Add(new Player("Topolino"));

        List<Continent> world = loader.getWorld(data);

        Debug.Log("Nea and the pussycats");

        dataManager = new DataManager(players, world, loader.getAllLands(world));

        view.updateTextPlayerData(dataManager.getPlayer());

        string phase = dataManager.getCurrentPhase() + System.Environment.NewLine + dataManager.getPlayer();
        countStartDeploy = INITIAL_TANKS_DEPLOY;

        Debug.Log("Initiate cloack mode");

        view.changeCanvasOption(phase);
        view.updatePhase(phase + System.Environment.NewLine + dataManager.getPlayer());
        view.updateDeployRemain( "" + countStartDeploy);
        view.updateSingleSelected("Select a State !!!");
        localMode();

        Debug.Log("The cool killers' club");
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
            DataSender.SendComboCarte(message);
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

    public void setClicked(string continent)
    {
        if(dataManager.getPlayer().Equals(dataManager.getPlayerByLand(continent)))
            firstland = continent;
        else 
            secondland = continent;
        view.updateLandText(dataManager.getLandData(continent));
        view.updateTwoSelected(firstland, secondland);
    }

    private void prepareViewDeployStart()
    {
        view.updateSingleSelected("Select a State !!");
        int remainTanks = INITIAL_TANKS_DEPLOY;
        if(dataManager.getPlayerTanksReinforcement(dataManager.getPlayer()) < INITIAL_TANKS_DEPLOY)
            remainTanks = dataManager.getPlayerTanksReinforcement(player);
        view.updateDeployRemain("" + remainTanks);
    }

    private void prepareViewDeploy()
    {
        view.updateSingleSelected("Select a State !!");
        view.updateDeployRemain("" + dataManager.getPlayerTanksReinforcement(player));
    }

    private void prepareViewAttack()
    {
        view.updateTwoSelected("", "");
    }

    private void prepareViewDefense()
    {

    }

    private void prepareViewMove()
    {
        view.updateTwoSelected("", "");
    }

    private void prepareViewWaiting()
    {
        
    }
 
}