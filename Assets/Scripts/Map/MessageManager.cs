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
    private int card1 = -1, card2 = -1, card3 = -1;
  

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

    public int getCard1()
    {
        return card1;
    }

    public int getCard2()
    {
        return card2;
    }

    public int getCard3()
    {
        return card3;
    }

    public string messageInitiateCombat(string landAttacker, string landDefender, int nTank)
    {
   	    message = "";
   	    message += landAttacker + " has engaged " + landDefender + " with " + nTank + "." + System.Environment.NewLine;
   	    message += landAttacker + System.Environment.NewLine;
        message += nTank + System.Environment.NewLine;
   	    message += landDefender;

   	    return message;
    }

    public List<string> readInitiateCombat(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    landStart = data[1];
   	    nTank1 = int.Parse(data[2]);
   	    landEnd = data[3];
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
   	    message += landAttacker + System.Environment.NewLine;
        message += nTankAttackerLost + System.Environment.NewLine;
   	    message += landDefender + System.Environment.NewLine;
        message += nTankDenderLost + System.Environment.NewLine;
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

   	    landStart = data[4];
   	    nTank1 = int.Parse(data[5]);
   	    landEnd = data[6];
   	    nTank2 = int.Parse(data[7]);
   	    resultBattle = data[3];

   	    return messageLog;
    }

    public string messageDeploy(string player, int nTank, string land)
    {
   	    message = "";
   	    message += player + ": " + land + " received " + nTank + " tanks as reinforcements." + System.Environment.NewLine;
   	    message += player + System.Environment.NewLine;
        message += land + System.Environment.NewLine;
        message += nTank + System.Environment.NewLine;

   	    return message;
    }

    public List<string> readDeploy(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    player1 = data[1];
   	    landStart = data[2];
   	    nTank1 = int.Parse(data[3]);

   	    return messageLog;
    }

    public string messageMove(string landStart, string landEnd, int nTank)
    {
        message = "";
        message += nTank + " tanks has moved from " + landStart + " to " + landEnd + System.Environment.NewLine;
        message += nTank  + System.Environment.NewLine;
        message += landStart  + System.Environment.NewLine;
        message += landEnd + System.Environment.NewLine;
        return message;
    }

    public List<string> readMove(string message)
    {
   	    string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	    List<string> messageLog = new List<string>();
   	    messageLog.Add(data[0]);

   	    nTank1 = int.Parse(data[1]);
   	    landStart = data[2];
   	    landEnd = data[3];

   	    return messageLog;
    }

    public string messagePhase(string player, string phase)
    {
        message = "";
        message += player + System.Environment.NewLine;
        message += phase;

        return message;
    }

    public string readPhase(string message)
    {
        string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
        player1 = data[0];
        return data[0] + System.Environment.NewLine + data[1];
    }

    public string messageUsedCards(string player, int card1, int card2, int card3)
    {
        message = "";
        message += player + " has used cards." + System.Environment.NewLine;
        message += player + System.Environment.NewLine;
        message += card1 + System.Environment.NewLine;
        message += card2 + System.Environment.NewLine;
        message += card3;

        return message;
    }

    public List<string> readUsedCards(string message)
    {
        List<string> eventLog = new List<string>();
        string[] data = message.Split(new[] { System.Environment.NewLine }, System.StringSplitOptions.None);

        player1 = data[1];
        card1 = int.Parse(data[2]);
        card2 = int.Parse(data[3]);
        card3 = int.Parse(data[4]);
        eventLog.Add(data[0]);

        return eventLog;
    }
}
