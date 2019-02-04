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
            view.updateRemainTank(nTanksRemain);
            view.updateLogEvent(manageMessage.readDeploy());
            DataSender.sendPosizionamento(message);
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
                view.hideAllCanvasOption();
                view.updatePhase(data.getPlayer(), data.getCurrentPhase());
                nextPhase = new StateWait(controller, data);
                string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
                DataSender.sendNextPhase(message);
            }
            else
            {
                data.startGame();
                if(data.getPlayer().Equals(controller.getPlayer()))
                {
                    view.updatePhase(data.getPlayer(), data.getCurrentPhase());
                    data.giveTanks();
                    view.updateDeploySelected("Select a Land !!!");
                    view.updateRemainTank(data.getPlayerTanksReinforcement(controller.getPlayer()));
                    view.changeCanvasOption(VIEW_OPTION);
                    nextPhase = new StateDeploy(controller, data);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
                    DataSender.sendNextPhase(message);
                }
                else
                {
                    view.hideAllCanvasOption();
                    view.updatePhase(data.getPlayer(), data.getCurrentPhase());
                    nextPhase = new StateWait(controller, data);
                    string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
                    DataSender.sendNextPhase(message);
                }
            }
        }
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
}
