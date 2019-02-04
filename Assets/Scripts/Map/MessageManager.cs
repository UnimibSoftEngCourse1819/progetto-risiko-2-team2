using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    private string message = "";

    private string player1 = "", player2 = "";
    private string landStart = "", landEnd = "";
    private int nTank1 = 0, nTank2 = 0; 
    private string resultBattle = "";
    private string card1 = "", card2 = "", card3 = "";
  

    /*
    1) messaggio da scrivere sul log
    2 or more) istruzioni per aggiornare i dati 
    */

    public string getPlayer1()
    {
        return player1;
    }

    public string getPlayer2()
    {
   	    return player2;
    }

    public string getLandStart()
    {
   	    return landStart;
    }

    public string getLandEnd()
    {
   	    return landEnd;
    }

    public int getNTank1()
    {
   	    return nTank1;
    }

    public int getNTank2()
    {
   	    return nTank2;
    }

    public string getResultBattle()
    {
   	    return resultBattle;
    }

    public string getCard1()
    {
        return card1;
    }

    public string getCard2()
    {
        return card2;
    }

    public string getCard3()
    {
        return card3;
    }

    public string messageInitiateCombat(string landAttacker, string landDefender, int nTank)
    {
   	    message = "";
   	    message += landAttacker + " has engaged " + landDefender + " with " + nTank + "." + System.Environment.NewLine;
   	    message += landAttacker + " " + nTank + System.Environment.NewLine;
   	    message += landDefender;

   	    return message;
    }

    public List<string> readInitiateCombat(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    string[] dataLogic = data[1].Split(' ');
   	    landStart = dataLogic[0];
   	    nTank1 = int.Parse(dataLogic[1]);

   	    dataLogic = data[2].Split(' ');
   	    landEnd = dataLogic[0];

   	    return messageLog;
    }

    public string messageDefend(string landDefender,  string landAttacker,
   							    int nTankDenderLost, int nTankAttackerLost , 
   							    string nTankDefend, string result)
    {
   	    message = "";
   	    message += landDefender + " defended with "+ nTankDefend + System.Environment.NewLine;
   	    message += "Result battle : " + landAttacker + " has lost " + nTankAttackerLost + System.Environment.NewLine;
   	    message += "Result battle : " + landDefender + " has lost" + nTankDenderLost + System.Environment.NewLine;
   	    message += result + System.Environment.NewLine;
   	    message += landAttacker + " " + nTankAttackerLost + System.Environment.NewLine;
   	    message += landDefender + " " + nTankDenderLost + System.Environment.NewLine;

   	    return message;
    }

    public List<string> readDefend(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);
   	    messageLog.Add(data[1]);
   	    messageLog.Add(data[2]);
   	    messageLog.Add(data[3]);

   	    string[] dataLogic = data[4].Split(' ');
   	    landStart = dataLogic[0];
   	    nTank1 = int.Parse(dataLogic[1]);

   	    dataLogic = data[5].Split(' ');
   	    landEnd = dataLogic[0];
   	    nTank2 = int.Parse(dataLogic[1]);
   	    resultBattle = data[2];

   	    return messageLog;
    }

    public string messageDeploy(string player, int nTank, string land)
    {
   	    message = "";
   	    message += player + ": " + land + "received " + nTank + "tanks as reinforcements." + System.Environment.NewLine;
   	    message += player + " " + land + " " + nTank;

   	    return message;
    }

    public List<string> readDeploy(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    string[] dataLogic = data[1].Split(' ');
   	    player1 = dataLogic[0];
   	    landStart = dataLogic[1];
   	    nTank1 = int.Parse(dataLogic[2]);

   	    return messageLog;
    }

    public string messageMove(string landStart, string landEnd, int nTank)
    {
        message = "";
        message += nTank + " tanks has moved from " + landStart + " to " + landEnd + System.Environment.NewLine;
        message += nTank + " " + landStart + " " + landEnd;

        return message;
    }

    public List<string> readMove(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    string[] dataLogic = data[1].Split(' ');
   	    nTank1 = int.Parse(dataLogic[0]);
   	    landStart = dataLogic[1];
   	    landEnd = dataLogic[2];

   	    return messageLog;
    }

    public string messagePhase(string player, string phase)
    {
        message = "";
        message += player + " " + phase;

        return message;
    }

    public string readPhase(string message)
    {
        string[] data =  message.Split(' ');

        return data[0] + System.Environment.NewLine + data[1];
    }

    public string messageCard(string player, int nTank)
    {
        message = "";
        message += player + ": has used cards and get " + nTank + " as bonus reinforcements" + System.Environment.NewLine;
        message += player + " " + nTank;

        return message;
    }

    public List<string> readCard(string message)
    {
        string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
        string[] dataLogic = data[1].Split(' ');
        player1 = dataLogic[0];
        nTank1 = int.Parse(dataLogic[1]);
        List<string> eventLog = new List<string>();
        eventLog.Add(data[0]);

        return eventLog;
    }

    public string messageUsedCards(string player, string card1, string card2, string card3)
    {
        message = "";
        message += player + ": has used " + card1 + ", " + card2 + " and " + card3 + System.Environment.NewLine;
        message += player + " " + card1 + " " + card2 + " " + card3;

        return message;
    }

    public List<string> readUsedCards(string message)
    {
        List<string> eventLog = new List<string>();
        string[] data = message.Split(new[] { System.Environment.NewLine }, System.StringSplitOptions.None);
        string[] dataLogic = data[1].Split(' ');

        player1 = dataLogic[0];
        card1 = dataLogic[1];
        card2 = dataLogic[2];
        card3 = dataLogic[3];
        eventLog.Add(data[0]);

        return eventLog;
    }
}
