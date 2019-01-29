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

   public string messageInitiateCombat(string playerAttacker, string playerDefender, string landAttacker, string landDefender, int nTank)
   {
   	message = "";
   	message += playerAttacker + ": " + landAttacker + " has engaged " + landDefender + " of " + playerDefender + " with " + nTank + "." + System.Environment.NewLine;
   	message += playerAttacker + " " + landAttacker + " " + nTank + System.Environment.NewLine;
   	message += playerDefender + " " + landDefender;
   	return message;
   }

   public List<string> readInitiateCombat(string message)
   {
   	string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	List<string> messageLog = new List<string>();
   	messageLog.Add(data[0]);

   	string[] dataLogic = data[1].Split(' ');
   	player1 = dataLogic[0];
   	landStart = dataLogic[1];
   	nTank1 = int.Parse(dataLogic[2]);

   	dataLogic = data[2].Split(' ');
   	player2 = dataLogic[0];
   	landEnd = dataLogic[1];
   	return messageLog;
   }

   public string messageDefend(string playerDefender, string playerAttacker,
   								string landDefender,  string landAttacker,
   								int nTankDenderLost, int nTankAttackerLost , 
   								string nTankDefend, string result)
   {
   	message = "";
   	message += playerDefender + ": " + landDefender + " defended with "+ nTankDefend + System.Environment.NewLine;
   	message += "Result battle : " + playerAttacker + " has lost " + nTankAttackerLost + System.Environment.NewLine;
   	message += "Result battle : " + playerDefender + " has lost" + nTankDenderLost + System.Environment.NewLine;
   	message += result + System.Environment.NewLine;
   	message += playerAttacker + " " + landAttacker + " " + nTankAttackerLost + System.Environment.NewLine;
   	message += playerDefender + " " + landDefender + " " + nTankDenderLost + System.Environment.NewLine;
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
   	player1 = dataLogic[0];
   	landStart = dataLogic[1];
   	nTank1 = int.Parse(dataLogic[2]);

   	dataLogic = data[5].Split(' ');
   	player2 = dataLogic[0];
   	landEnd = dataLogic[1];
   	nTank2 = int.Parse(dataLogic[2]);
   	resultBattle = data[3];
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

   public string messageMove(string player, string landStart, string landEnd, int nTank)
   {
    message = "";
    message += player + ": " + nTank + " tanks has moved from " + landStart + " to " + landEnd + System.Environment.NewLine;
    message += player + " " + nTank + " " + landStart + " " + landEnd;
    return message;
   }

   public List<string> readMove(string message)
   {
   	string[] data =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
   	List<string> messageLog = new List<string>();
   	messageLog.Add(data[0]);

   	string[] dataLogic = data[1].Split(' ');
   	player1 = dataLogic[0];
   	nTank1 = int.Parse(dataLogic[1]);
   	landStart = dataLogic[2];
   	landEnd = dataLogic[3];
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
    message += player + ": has used cards and get " + nTank + " as bonus reinforcements";
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


}
