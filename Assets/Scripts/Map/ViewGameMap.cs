using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewGameMap : MonoBehaviour
{

    private const string INITIAL_TEXT = "BUG";
    private const int MAX_LINES_ON_EVENT_LOG = 15;
    private readonly string[] PHASE = {"Initial Deploy phase", "Attack phase", "Deployment phase", "Move phase", "Defend phase", "Wait"};
    private const int STARTDEPLOY = 0, ATTACK = 1, DEPLOY = 2, MOVE = 3, DEFEND = 4, WAIT = 5;
    private readonly List<string> logEvent = new List<string>();
    public ControllGameMap controller;

    private CanvasGroup attack, defense, move, deploy, deployGaming, popup, message, quit, cards;
    private Text eventLog, phase, selectedData, playerData, attackSelected, moveSelected, deployRemain, deploySelected, messagePopup, goal, cardList, defenseTanks;
    private Dropdown card1, card2, card3;
    private Image panelCard;
    private List<StateBattle> stateUI;
    public StateBattle statePrefab;
    public GameObject stateHolder;
    public Camera mainCamera;
    public BattleStateUI stateUIPrefab;
    public GameObject battleUI;

    public void prepareView()//prepara l'interfaccia
    {
    	//Text
        eventLog = GameObject.Find("TextEventLog").GetComponent<Text>(); 
        phase = GameObject.Find("TextPhase").GetComponent<Text>(); 
        selectedData = GameObject.Find("TextSelectedData").GetComponent<Text>(); 
        playerData = GameObject.Find("TextPlayerData").GetComponent<Text>(); 
        attackSelected = GameObject.Find("TextAttackSelected").GetComponent<Text>(); 
        moveSelected = GameObject.Find("TextMoveSelected").GetComponent<Text>(); 
        deployRemain = GameObject.Find("TextDeployRemain").GetComponent<Text>(); 
        deploySelected = GameObject.Find("TextDeploySelected").GetComponent<Text>();
        messagePopup =  GameObject.Find("TextMessagePopup").GetComponent<Text>();
        goal = GameObject.Find("TextGoal").GetComponent<Text>();
        cardList = GameObject.Find("TextCardList").GetComponent<Text>();
        defenseTanks = GameObject.Find("TextDefenseTank").GetComponent<Text>();


        eventLog.text = INITIAL_TEXT;
        phase.text = INITIAL_TEXT;
        selectedData.text = INITIAL_TEXT;
        playerData.text = INITIAL_TEXT;
        attackSelected.text = INITIAL_TEXT;
        moveSelected.text = INITIAL_TEXT;
        deployRemain.text = INITIAL_TEXT;
        deploySelected.text = INITIAL_TEXT;
        messagePopup.text = INITIAL_TEXT;
        goal.text = INITIAL_TEXT;
        cardList.text = INITIAL_TEXT;
        defenseTanks.text = INITIAL_TEXT;

        //CanvasGroup
        attack = GameObject.Find("CanvasAttack").GetComponent<CanvasGroup>();
        defense = GameObject.Find("CanvasDefense").GetComponent<CanvasGroup>(); 
        move = GameObject.Find("CanvasMove").GetComponent<CanvasGroup>(); 
        deploy = GameObject.Find("CanvasDeploy").GetComponent<CanvasGroup>();
        deployGaming = GameObject.Find("CanvasDeployGaming").GetComponent<CanvasGroup>();
        popup = GameObject.Find("CanvasPopup").GetComponent<CanvasGroup>();
        message = GameObject.Find("CanvasMessage").GetComponent<CanvasGroup>();
        quit = GameObject.Find("CanvasQuit").GetComponent<CanvasGroup>();
        cards = GameObject.Find("CanvasCards").GetComponent<CanvasGroup>();

        //Dropdown
        card1 = GameObject.Find("DropdownCard1").GetComponent<Dropdown>();
        card2 = GameObject.Find("DropdownCard2").GetComponent<Dropdown>();
        card3 = GameObject.Find("DropdownCard3").GetComponent<Dropdown>();

        panelCard = GameObject.Find("PanelCards").GetComponent<Image>();
        panelCard.raycastTarget = false;

        clearOptions();
        hideAllCanvasOption();
        closePopup();
        closeCard();
        stateUI = new List<StateBattle>();
    }

    //****METHODS THAT CHANGE A TEXT

    public void showMessage(string messageString)
    {
        showPopup();
        message.alpha = 1f;
        message.interactable = true;
        message.blocksRaycasts = true;
        messagePopup.text = messageString;
    }

    public void updateTextPlayerData(string data)
    {
        playerData.text = data;
    }

    public void updateLogEvent(List<string> messages)
    {
        foreach(string message in messages)
        {
            updateLogEvent(message);
        }
    }

    public void updateLogEvent(string message)
    {
        if(logEvent.Count == MAX_LINES_ON_EVENT_LOG)
            logEvent.RemoveAt(0);
        logEvent.Add(message);
        eventLog.text = "";
        foreach(string lineEvent in logEvent)
        {
            eventLog.text += lineEvent + System.Environment.NewLine; 
        }
    }

    public void updatePhase(string message)
    {
        phase.text = message;
    }

    public void updatePhase(string namePlayer, string phase)
    {
         updatePhase ("" + namePlayer + System.Environment.NewLine + phase);
    }

    public void updateTwoSelected(string landStart, string landEnd)
    {
        if (landStart == null)
            landStart = "";
        if (landEnd == null)
            landEnd = "";
        attackSelected.text = landStart + System.Environment.NewLine + "VS" + System.Environment.NewLine + landEnd; 
        moveSelected.text = landStart + System.Environment.NewLine + "to" + System.Environment.NewLine + landEnd; 
    }

    public void updateDeploySelected(string land)
    {
        deploySelected.text = land;
    }

    public void updateTanksRemain(int n)
    {
        deployRemain.text = "Tank remaining : " + n;
        defenseTanks.text = "You have: "+ n + " tanks";
    }

    public void updateText(List<string> data)
    {
        updatePhase(data[0], INITIAL_TEXT);
        playerData.text = data[1];
        updateLogEvent(data[2]);
    }

    public void updateLandText(string text)
    {
        selectedData.text = text;
    }

    public void updateGoal(string textGoal)
    {
        goal.text = textGoal;
    }

    public void updateCardList(string textCards)
    {
        cardList.text = textCards;
    }

    public void setColorState(string land, string color)
    {
        foreach(StateBattle state in stateUI)
        {
            if(land.Equals(state.idName))
                state.ChangePlayerColor(color);
        }

    }

    public void setNumberLands(string land, int tank)
    {
        foreach(StateBattle state in stateUI)
        {
            if(land.Equals(state.idName))
                state.SetNumberOfTanks(tank);
        }
    }

    //METHODS THAT SHOW A UI COMPONENTS

    public void drawMap(List<StateData> stateData)
    {
        foreach(StateData state in stateData)
        {
            StateBattle newState = Instantiate(statePrefab, state.getVector(), Quaternion.identity) as StateBattle;

            newState.SetState(state.texture, state.stateName);
            newState.transform.SetParent(stateHolder.transform);
            newState.Click.AddListener((State land) =>
                {
                    controller.onClickLand(land.idName);
                    //other thing to add when clicked;
                }
            );

            BattleStateUI uiState = Instantiate(stateUIPrefab, state.getVector(), Quaternion.identity) as BattleStateUI;
            uiState.transform.SetParent(battleUI.transform);
            stateUI.Add(newState);
        }
    }

    public void changeCanvasOption(string phase)
    {
        hideAllCanvasOption();

        Debug.Log(phase);
        if(phase.Equals(PHASE[STARTDEPLOY], StringComparison.InvariantCultureIgnoreCase))
        {
            deploy.alpha = 1f;
            deploy.interactable = true;
            deploy.blocksRaycasts = true;
        }
        if(phase.Equals(PHASE[ATTACK], StringComparison.InvariantCultureIgnoreCase))
        {
            attack.alpha = 1f;
            attack.interactable = true;
            attack.blocksRaycasts = true;
        }
        if(phase.Equals(PHASE[DEFEND], StringComparison.InvariantCultureIgnoreCase))
        {
            defense.alpha = 1f;
            defense.interactable = true;
            defense.blocksRaycasts = true;
        }
        if(phase.Equals(PHASE[MOVE], StringComparison.InvariantCultureIgnoreCase))
        {
            move.alpha = 1f;
            move.interactable = true;
            move.blocksRaycasts = true;
        }
        if(phase.Equals(PHASE[DEPLOY], StringComparison.InvariantCultureIgnoreCase))
        {
            deploy.alpha = 1f;
            deploy.interactable = true;
            deploy.blocksRaycasts = true;
            deployGaming.alpha = 1f;
            deployGaming.interactable = true;
            deployGaming.blocksRaycasts = true;
        }
    }

    public void showConfirmQuit()
    {
        showPopup();
        quit.alpha = 1f;
        quit.interactable = true;
        quit.blocksRaycasts = true;
    }
    

    private void showPopup()
    {
        popup.alpha = 1f;
        popup.interactable = true;
        popup.blocksRaycasts = true;
    }

    public void showCards(List<string> options)
    {
        cards.alpha = 1f;
        cards.interactable = true;
        cards.blocksRaycasts = true;
        clearOptions();
        foreach(string option in options)
        {
            addOption(option);
        }
        card1.RefreshShownValue();
        card2.RefreshShownValue();
        card3.RefreshShownValue();
    }

    //METHODS THAT CLOSE A UI COMPONENTS

    public void closeCard()
    {
        cards.alpha = 0f;
        cards.interactable = false;
        cards.blocksRaycasts = false;
    }

    public void closePopup()
    {
        popup.alpha = 0f;
        message.alpha = 0f;
        quit.alpha = 0f;
        popup.interactable = false;
        message.interactable = false;
        quit.interactable = false;     
        popup.blocksRaycasts = false;
        message.blocksRaycasts = false;
        quit.blocksRaycasts = false;
    }

    //PRIVATE METHODS

    private void clearOptions()
    {
        card1.ClearOptions(); 
        card2.ClearOptions(); 
        card3.ClearOptions(); 
    }

    private void hideAllCanvasOption()
    {
        attack.alpha = 0f;
        attack.interactable = false;
        attack.blocksRaycasts = false;
        defense.alpha = 0f;
        defense.interactable = false;
        defense.blocksRaycasts = false;
        move.alpha = 0f;
        move.interactable = false;
        move.blocksRaycasts = false;
        deploy.alpha = 0f;
        deploy.interactable = false;
        deploy.blocksRaycasts = false;
        deployGaming.alpha = 0f;
        deployGaming.interactable = false;
        deployGaming.blocksRaycasts = false;
    } 

    private void addOption(string option)
    {
        Dropdown.OptionData temp = new Dropdown.OptionData();
        temp.text = option;
        card1.options.Add(temp);
        card2.options.Add(temp);
        card3.options.Add(temp);
    }

}
