using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllGameMap : MonoBehaviour
{
    
    private InputField attackTank, defendTank, moveTank, deployTank;
    private Dropdown card1, card2, card3;

    public MessageManager messageManager;
    public DataManager model;
    public ViewGameMap view;
    public MapLoader loader;

    private StateControl state;
    private string firstLand, secondLand, player;
    private int tank1, tank2;
    private string message, typeMessage;
    private bool localMode = true;

    private void Awake()//prepara i componenti da cui prendere gli input
    {

        InizializeComponents();
        loadData();
        NetworkManager.istance.InizializeController();
    }

    //Private Methods for inizializing

    private void InizializeComponents()
    {
        //InputField
        attackTank = GameObject.Find("InputFieldAttackTank").GetComponent<InputField>();
        defendTank = GameObject.Find("InputFieldDefendTank").GetComponent<InputField>();
        moveTank = GameObject.Find("InputFieldMoveTank").GetComponent<InputField>();
        deployTank = GameObject.Find("InputFieldDeployTank").GetComponent<InputField>();

        //Dropdown
        card1 = GameObject.Find("DropdownCard1").GetComponent<Dropdown>();
        card2 = GameObject.Find("DropdownCard2").GetComponent<Dropdown>();
        card3 = GameObject.Find("DropdownCard3").GetComponent<Dropdown>();
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

        model = new DataManager(players, world, loader.getAllLands(world));

        view.updateTextPlayerData(model.getPlayer());

        string phase = model.getPhase();

        Debug.Log("Initiate cloack mode");

        view.changeCanvasOption(phase);
        view.updatePhase(phase + System.Environment.NewLine + model.getPlayer());
        view.updateDeploySelected("Select a Land !!!");
        view.updateDeployRemain(3);
        setLocalMode();    

        Debug.Log("The cool killers' club");
    }

    //Methods that return value from UI components

    public int getDeployTank()
    {
        return int.Parse(deployTank.text);
    }

    public int getAttackTank()
    {
        return int.Parse(attackTank.text);
    }

    public int getMoveTank()
    {
        return int.Parse(moveTank.text);
    }

    public int getDefendTank()
    {
        return int.Parse(defendTank.text);
    }

    //Methods get

    public string getFirstLand()
    {
        return firstLand;
    }

    public string getSecondLand()
    {
        return secondLand;
    }

    public int getTank1()
    {
        return tank1;
    }

    public int getTank2()
    {
        return tank2;
    }

    public string getPlayer()
    {
        return player;
    }

    public string getMessage()
    {
        return message;
    }

    public string getTypeMessage()
    {
        return typeMessage;
    }

    // Button methods

    public void onClickAction()
    {
        List<string> error = state.getMissingData();
        if (error.Count != 0)
        {
            int count = 0;
            string message = "You can't do that action because are missing some data : ";
            foreach(string missing in error)
            {
                message += missing;
                if(count != error.Count)
                    message += ", " ;
            }
            view.showMessage(message);
        }
        else
        {
            string errorAction = state.action();
            if(errorAction.Equals("FORCE_NEXT_PHASE"))
                state = state.nextPhaseForced();
            else
                view.showMessage(errorAction);
        }
    }

    public void onClickUseCard()
    {
        model.useCards(card1.options[card1.value].text,
                        card2.options[card2.value].text,
                        card3.options[card3.value].text);
    }

    public void onClickExit()
    {
        //esce dal game
    }

    public void onClickQuit()
    {
         view.showConfirmQuit();
    }

    public void onClickShowCards()
    {
        //showCards(List<string> options)
    }

    public void onClickClosePopup()
    {
        view.closePopup();
    }

    public void onClickCloseCard()
    {
        view.closeCard();
    }

    //local mode methods

    public void setLocalMode()
    {
        player = model.getPlayer();
    }

    public bool isLocalMode()
    {
        return localMode;
    }

    public void setFirstLand(string land)
    {
        firstLand = land;
    }

    public void setSecondLand(string land)
    {
        secondLand = land;
    }

    public void setTank1(int n)
    {
        tank1 = n;
    }

    public void setTank2(int n)
    {
        tank2 = n;
    }

    // methods for networks
    public void receiveMessage(string type, string message)
    {
        typeMessage = type;
        this.message = message;
        onClickAction();
    }
}