using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Server;

public class StateStartDeploy : StateControl
{
    private ControllGameMap controller;
    private MessageManager manageMessage;
    private ViewGameMap view;
    private DataManager data;
    private string land;
    private int nTanks, nTanksRemain;

    private const string VIEW_OPTION = "Deployment phase";
    private const int MAX_TANKS_PER_TIME = 3;

    public StateStartDeploy(ControllGameMap controller,  DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.view = view;
        this.manageMessage = manageMessage;
        land = null;
        nTanks = -1;
        if(data.getPlayerTanksReinforcement(data.getPlayer()) < MAX_TANKS_PER_TIME)
        	nTanksRemain = data.getPlayerTanksReinforcement(data.getPlayer());
        else
        	nTanksRemain = MAX_TANKS_PER_TIME;
        view.updatePhase(data.getPlayer(), data.getPhase());
        view.changeCanvasOption("Initial Deploy phase");
        view.updateTanksRemain(nTanksRemain);
        view.updateDeploySelected("Select a state !!!");
        Debug.Log("Data phase: " + data.getPhase() + " - State : Intial Deploy");
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        if(nTanks > nTanksRemain)
        	error = "Insufficient tanks : you have " + nTanksRemain + " but you want to deploy " + nTanks;
       	else
        	error = data.addTanks(land, nTanks);
        if(error.Equals(""))
        {
            string message = manageMessage.messageDeploy(controller.getPlayer(), nTanks, land);
            view.updateLogEvent(manageMessage.readDeploy(message));
            DataSender.SendPosizionamento(message);
            nTanksRemain -= nTanks;
            if(nTanksRemain == 0)
                error = "FORCE_NEXT_PHASE";
            else
            {
                view.updateDeploySelected("Select a Land !!!");
                view.updateTanksRemain(nTanksRemain);
            }
        }
        return error;
    }

    private void loadNecessaryData()
    {
        land = controller.getFirstLand();
        nTanks = controller.getDeployTank();
    }

    public override StateControl nextPhase()
    {
        return null;
    }

    public override StateControl nextPhaseForced()
    {
        StateControl nextPhase = null;
        if(nTanksRemain > 0)
        {
            nextPhase = this;
            view.showMessage("You have to deploy ALL your tanks !!!");
        }
        else
        {
            if(!data.isAllPlayerRunOutOfTanks())
            {
                data.nextDeploy();
                if(!data.getPlayer().Equals(controller.getPlayer()) && !controller.isLocalMode())
                {
                    nextPhase = new StateWait(controller, data, manageMessage, view);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
                    DataSender.SendNextPhase(message);
                }
                else
                {
                    nextPhase = new StateStartDeploy(controller, data, manageMessage, view);
                }
            }
            else
            {
                data.startGame();
                if(data.getPlayer().Equals(controller.getPlayer()) || controller.isLocalMode())//cheking if the player the the one who start
                {
                    data.giveTanks();
                    Debug.Log("STATE CONFIRMED");
                    nextPhase = new StateDeploy(controller, data, manageMessage, view);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
                    DataSender.SendNextPhase(message);

                }
                else
                {
                    nextPhase = new StateWait(controller, data, manageMessage, view);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
                    DataSender.SendNextPhase(message);
                }
            }
        }
        if(controller.isLocalMode())
            controller.setLocalMode();
        return nextPhase;
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land selected ");
        if (controller.getDeployTank() <= 0)
            missingData.Add("Valid Number of Tanks");
        return missingData;
    }

    public override string needSaving(string land)
    {
        string field = "";
        if(data.getPlayerByLand(land).Equals(data.getPlayer()))
        {
            field = "firstLand";
            view.updateDeploySelected(land);
        }
        return field;
    }
}
