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

    private  const string VIEW_OPTION = "Deployment phase";

    public StateStartDeploy(ControllGameMap controller,  DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.view = view;
        this.manageMessage = manageMessage;
        land = null;
        nTanks = -1;
        nTanksRemain = -1;
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        error = data.addTanks(land, nTanks);
        if(error.Equals(""))
        {
            string message = manageMessage.messageDeploy(controller.getPlayer(), nTanks, land);
            view.updateDeploySelected("Select a Land !!!");
            view.updateDeployRemain(nTanksRemain);
            view.updateLogEvent(manageMessage.readDeploy(message));
            DataSender.SendPosizionamento(message);
            if(nTanksRemain == 0)
                error = "FORCE_NEXT_PHASE";
        }
        return error;
    }

    private void loadNecessaryData()
    {
        land = controller.getFirstLand();
        nTanks = controller.getDeployTank();
        nTanksRemain -= nTanks;
    }

    public override StateControl nextPhase()
    {
        return null;
    }

    public override StateControl nextPhaseForced()
    {
        StateControl nextPhase = null;
        if(nTanks > 0)
        {
            nextPhase = this;
            view.showMessage("You have to deploy ALL your tanks !!!");
        }
        else
        {
            if(!data.isAllPlayerRunOutOfTanks())
            {
                data.nextDeploy();
                view.updatePhase(data.getPlayer(), data.getPhase());
                if(!data.getPlayer().Equals(controller.getPlayer()) && !controller.isLocalMode())
                {
                    view.changeCanvasOption("Wait");
                    nextPhase = new StateWait(controller, data, manageMessage, view);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
                    DataSender.SendNextPhase(message);
                }
                else
                {
                    view.updateDeployRemain(nTanksRemain);
                }
            }
            else
            {
                data.startGame();
                if(data.getPlayer().Equals(controller.getPlayer()) || controller.isLocalMode())//cheking if the player the the one who start
                {
                    view.updatePhase(data.getPlayer(), data.getPhase());
                    data.giveTanks();
                    view.updateDeploySelected("Select a Land !!!");
                    view.updateDeployRemain(data.getPlayerTanksReinforcement(controller.getPlayer()));
                    view.changeCanvasOption(VIEW_OPTION);
                    nextPhase = new StateDeploy(controller, data, manageMessage, view);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
                    DataSender.SendNextPhase(message);

                }
                else
                {
                    view.changeCanvasOption("Wait");
                    view.updatePhase(data.getPlayer(), data.getPhase());
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
            field = "firstLand";
        return field;
    }
}
