using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Server;

public class StateMove : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private MessageManager manageMessage;
    private ViewGameMap view;
    private string firstLand, secondLand;
    private int nTanks;

    public StateMove(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.view = view;
        this.manageMessage = manageMessage;
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
            view.updateLogEvent(manageMessage.readMove(message));
            DataSender.SendSpostamento(message);

        }
        return error;
    }

    private void next()
    {
        view.updatePhase(data.getPlayer(), data.getPhase());
        
        if(!controller.isLocalMode())
            view.changeCanvasOption("Wait");
        else
        {
            controller.setLocalMode();
            view.changeCanvasOption("Deployment phase");
        }
                
        
    }

    private void notifyPassTurn()
    {
        string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
        DataSender.SendNextPhase(message);
    }

    private void loadNecessaryData()
    {
        firstLand = controller.getFirstLand();
        secondLand = controller.getSecondLand();
        nTanks = controller.getMoveTank();
    }

    public override StateControl nextPhase()
    {   
        data.passTurn();
        next();
        notifyPassTurn();
        StateControl stateReturn = null;
        if(controller.isLocalMode())
        {
            stateReturn = new StateDeploy(controller, data, manageMessage, view);
            view.changeCanvasOption("Deployment phase");
        }
        else
        {
            stateReturn = new StateWait(controller, data, manageMessage, view);
            view.changeCanvasOption("Wait");
        }
        return stateReturn;
    }

    public override StateControl nextPhaseForced()
    {
        notifyPassTurn();
        StateControl stateReturn = null;
        if(controller.isLocalMode())
            stateReturn = new StateDeploy(controller, data, manageMessage, view);
        else
            stateReturn = new StateWait(controller, data, manageMessage, view);
        return stateReturn;
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land start ");
        if (controller.getSecondLand() == null)
            missingData.Add("Land end ");
        if (controller.getMoveTank() <= 0)
            missingData.Add("Number tank");
        return missingData;
    }
}
