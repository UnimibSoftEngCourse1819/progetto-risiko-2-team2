using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Server;

public class StateDeploy : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private MessageManager manageMessage;
    private ViewGameMap view;
    private string land;
    private int nTanks;

    public StateDeploy(ControllGameMap controller,  DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.manageMessage = manageMessage;
        this.view = view;
        land = null;
        nTanks = -1;
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
            view.updateDeployRemain(data.getPlayerTanksReinforcement(controller.getPlayer()));
            view.updateLogEvent(manageMessage.readDeploy(message));
            DataSender.SendPosizionamento(message);
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
        data.nextPhase();
        string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
        DataSender.SendNextPhase(message);
        return (new StateAttack(controller,data, manageMessage, view));
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

    public override StateControl nextPhaseForced()
    {
        return null;
    }

    public override string needSaving(string land)
    {
        string field = "";
        if(data.getPlayerByLand(land).Equals(data.getPlayer()))
            field = "firstLand";
        return field;
    }

}
