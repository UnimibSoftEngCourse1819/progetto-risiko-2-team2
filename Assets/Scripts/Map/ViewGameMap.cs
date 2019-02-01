using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewGameMap : MonoBehaviour
{

    private const string INITIAL_TEXT = "BUG";
    private const int MAX_LINES_ON_EVENT_LOG = 17;
    private readonly string[] PHASE = {"Attack phase", "Deployment phase", "Move phase", "Defend phase"};
    private const int ATTACK = 0, DEPLOY = 1, MOVE = 2, DEFEND = 3;
    private readonly List<string> logEvent = new List<string>();

    private CanvasGroup attack, defense, move, deploy, deployGaming, popup, error, quit, cards;
	private Text eventLog, phase, selectedData, playerData, attackSelected, moveSelected, deployRemain, deploySelected, errorT;
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
        errorT =  GameObject.Find("TextError").GetComponent<Text>();

        Debug.Log("Tacatà");

        eventLog.text = INITIAL_TEXT;
        phase.text = INITIAL_TEXT;
        selectedData.text = INITIAL_TEXT;
        playerData.text = INITIAL_TEXT;
        attackSelected.text = INITIAL_TEXT;
        moveSelected.text = INITIAL_TEXT;
        deployRemain.text = INITIAL_TEXT;
        deploySelected.text = INITIAL_TEXT;
        errorT.text = INITIAL_TEXT;

        //CanvasGroup
        attack = GameObject.Find("CanvasAttack").GetComponent<CanvasGroup>();
        defense = GameObject.Find("CanvasDefense").GetComponent<CanvasGroup>(); 
        move = GameObject.Find("CanvasMove").GetComponent<CanvasGroup>(); 
        deploy = GameObject.Find("CanvasDeploy").GetComponent<CanvasGroup>();
        deployGaming = GameObject.Find("CanvasDeployGaming").GetComponent<CanvasGroup>();
        popup = GameObject.Find("CanvasPopup").GetComponent<CanvasGroup>();
        error = GameObject.Find("CanvasError").GetComponent<CanvasGroup>();
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

    private void clearOptions()
    {
        card1.ClearOptions(); 
        card2.ClearOptions(); 
        card3.ClearOptions(); 
    }

    public void showError(string message)
    {
        showPopup();
        error.alpha = 1f;
        error.interactable = true;
        errorT.text = message;
    }

    public void closeCard()
    {
        cards.alpha = 0f;
        cards.interactable = false;
    }

    private void showPopup()
    {
        popup.alpha = 1f;
        popup.interactable = true;
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

    public void updateTextPlayerData(string data)
    {
        Debug.Log(data);
        Debug.Log(playerData);
        playerData.text = data;
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

    public void updateLogEvent(List<string> messages)
    {
        foreach(string message in messages)
        {
            updateLogEvent(message);
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
        attackSelected.text = landStart + System.Environment.NewLine + "VS" + System.Environment.NewLine + landEnd; 
        moveSelected.text = landStart + System.Environment.NewLine + "to" + System.Environment.NewLine + landEnd; 
    }

    public void updateSingleSelected(string land)
    {
        deploySelected.text = land;
    }

    public void updateDeployRemain(string n)
    {
        deployRemain.text = "Tank remaining : " + n;
    }

    public void updateText(List<string> data)
    {
        updatePhase(data[0], INITIAL_TEXT);
        playerData.text = data[1];
        updateLogEvent(data[2]);
    }

    public void changeCanvasOption(string phase)
    {
        hideAllCanvasOption();
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

    public void errorPopup()
    {
        showPopup();
        error.alpha = 1f;
        error.interactable = true;
    }

    public void closePopup()
    {
        popup.alpha = 0f;
        error.alpha = 0f;
        quit.alpha = 0f;
        popup.interactable = false;
        error.interactable = false;
        quit.interactable = false;
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

    private void addOption(string option)
    {
        Dropdown.OptionData temp = new Dropdown.OptionData();
        temp.text = option;
        card1.options.Add(temp);
        card2.options.Add(temp);
        card3.options.Add(temp);
    }

    public void drawMap(List<StateData> stateData)
    {
        foreach(StateData state in stateData)
        {
            State newState = Instantiate(statePrefab);

            newState.SetState(state.texture, state.stateName);
            newState.transform.position = state.getVector();
        }
    }

    private void setupButton(GameObject button, string nameLand)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate { onClickedState(nameLand); });
    }
       
    public void onClickedState(string nameLand)
    {
        Debug.Log(nameLand);
    }

}
