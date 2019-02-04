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
	private Text eventLog, phase, selectedData, playerData, attackSelected, moveSelected, deployRemain, deploySelected, messagePopup, goal, cardList;
    private Dropdown card1, card2, card3;
    public State statePrefab;

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

        Debug.Log("Tacatà");

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

        clearOptions();
        hideAllCanvasOption();
        closePopup();
        closeCard();
    }

    //****METHODS THAT CHANGE A TEXT

    public void showMessage(string messageString)
    {
        showPopup();
        message.alpha = 1f;
        message.interactable = true;
        messagePopup.text = messageString;
    }

    public void updateTextPlayerData(string data)
    {
        Debug.Log(data);
        Debug.Log(playerData);
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

    public void updateDeployRemain(int n)
    {
        deployRemain.text = "Tank remaining : " + n;
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

    //METHODS THAT SHOW A UI COMPONENTS

    public void drawMap(List<StateData> stateData)
    {
        List<StateData>  banana = stateData;
        foreach(StateData state in banana)
        {
            State newState = Instantiate(statePrefab, state.getVector(), Quaternion.identity) as State;
            newState.SetState(state.texture, state.stateName);
            newState.Click.AddListener((State land) =>
                {
                    controller.onClickLand(land.idName);
                    //other thing to add when clicked;
                }
            );
        }
    }

    public void changeCanvasOption(string phase)
    {
        hideAllCanvasOption();

        Debug.Log(phase);

        Debug.Log(PHASE[STARTDEPLOY]);

        if(phase.Equals(PHASE[STARTDEPLOY], StringComparison.InvariantCultureIgnoreCase))
        {
            deploy.alpha = 1f;
            deploy.interactable = true;
        }
        if(phase.Equals(PHASE[ATTACK], StringComparison.InvariantCultureIgnoreCase))
        {
            attack.alpha = 1f;
            attack.interactable = true;
        }
        if(phase.Equals(PHASE[DEFEND], StringComparison.InvariantCultureIgnoreCase))
        {
            defense.alpha = 1f;
            defense.interactable = true;
        }
        if(phase.Equals(PHASE[MOVE], StringComparison.InvariantCultureIgnoreCase))
        {
            move.alpha = 1f;
            move.interactable = true;
        }
        if(phase.Equals(PHASE[DEPLOY], StringComparison.InvariantCultureIgnoreCase))
        {
            deploy.alpha = 1f;
            deploy.interactable = true;
            deployGaming.alpha = 1f;
            deployGaming.interactable = true;
        }
    }

    public void showConfirmQuit()
    {
        showPopup();
        quit.alpha = 1f;
        quit.interactable = true;
    }
    

    private void showPopup()
    {
        popup.alpha = 1f;
        popup.interactable = true;
    }

    public void showCards(List<string> options)
    {
        cards.alpha = 0f;
        cards.interactable = true;
        clearOptions();
        foreach(string option in options)
        {
            addOption(option);
        }
    }

    //METHODS THAT CLOSE A UI COMPONENTS

    public void closeCard()
    {
        cards.alpha = 0f;
        cards.interactable = false;
    }

    public void closePopup()
    {
        popup.alpha = 0f;
        message.alpha = 0f;
        quit.alpha = 0f;
        popup.interactable = false;
        message.interactable = false;
        quit.interactable = false;
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
        defense.alpha = 0f;
        defense.interactable = false;
        move.alpha = 0f;
        move.interactable = false;
        deploy.alpha = 0f;
        deploy.interactable = false;
        deployGaming.alpha = 0f;
        deployGaming.interactable = false;
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
