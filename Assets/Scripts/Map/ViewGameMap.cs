using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ViewGameMap : MonoBehaviour
{

    private const string INITIAL_TEXT = "BUG";
    private List<string> logEvent = new List<string>();

	private Text eventLog, phase, selectedData, playerData, attackSelected, moveSelected, deployRemain, deploySelected;
    

    private void Awake()//prepara l'interfaccia
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

        eventLog.text = INITIAL_TEXT;
        phase.text = INITIAL_TEXT;
        selectedData.text = INITIAL_TEXT;
        playerData.text = INITIAL_TEXT;
        attackSelected.text = INITIAL_TEXT;
        moveSelected.text = INITIAL_TEXT;
        deployRemain.text = INITIAL_TEXT;
        deploySelected.text = INITIAL_TEXT;
    }

    public void updatelogEvent(string message)
    {
        if(logEvent.Count == 5)
            logEvent.RemoveAt(0);
        logEvent.Add(message);
        eventLog.text = "";
        foreach(string lineEvent in logEvent)
        {
            eventLog.text += lineEvent + Environment.NewLine; 
        }
    }

    public void updatePhase(string namePlayer, string phase)
    {
        phase.text = namePlayer + Environment.NewLine + phase;
    }

    public void updateTwoSelected(string landStart, string landEnd)
    {
        attackSelected.text = landStart + Environment.NewLine + VS + Environment.NewLine + landEnd; 
        moveSelected.text = landStart + Environment.NewLine + to + Environment.NewLine + landEnd; 
    }

    public void updateSingleSelected(string land)
    {
        deploySelected.text = land;
    }

    public void updateDeployRemain(string n)
    {
        deployRemain.text = "Tank remaining : " + n;
    }
}
