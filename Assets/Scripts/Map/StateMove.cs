using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private string firstLand, secondLand;
    private int nTanks;

    public StateMove(ControllGameMap controller, DataManager data)
    {
        this.controller = controller;
        this.data = data;
        firstLand = null;
        secondLand = null;
        nTanks = -1;
    }

    public override string action()
    {
        loadNecessaryData();
        string error = "";
        error = data.moveTanks(firstLand, secondLand, nTanks);
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            next();
            string message = manageMessage.messageMove(firstLand, secondLand, nTanks);
            view.updateEventLog(message.readMove);
            DataSender.SendSpostamento(message);
        }
        return error;
    }

    private void next()
    {
        view.hideAllCanvasOption();
        view.updatePhase(data.getPlayer(), data.getCurrentPhase());
    }

    private void notifyPassTurn()
    {
        string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
        DataSender.sendNextPhase(message);
    }

    private void loadNecessaryData()
    {
        firstLand = controller.getFirstLand();
        secondLand = controller.getSecondLand();
        nTanks = controller.getTankMove();
    }

    public override StateControl nextPhase()
    {   
        data.passTurn();
        next();
        notifyPassTurn();
        return (new StateWait(controller, data));
    }

    public override StateControl nextPhaseForced()
    {
        notifyPassTurn();
        return (new StateWait(controller, data));
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land start ");
        if (controller.getSecondLand() == null)
            missingData.Add("Land end ");
        if (controller.getTankMove() <= 0)
            missingData.Add("Number tank");
        return missingData;
    }
}
