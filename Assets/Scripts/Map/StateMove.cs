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
    private StateControl nextPhaseLoad;

    public StateMove(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.view = view;
        this.manageMessage = manageMessage;
        nextPhaseLoad = this;
        firstLand = null;
        secondLand = null;
        nTanks = -1;
        controller.resetMemoryBuffer();
        view.updatePhase(data.getPlayer(), data.getPhase());
        view.changeCanvasOption("Move phase");
        view.updateTwoSelected("", "");
        Debug.Log("Data phase: " + data.getPhase() + " - State : Move");
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
        if(!controller.isLocalMode())
        {
            view.updatePhase(data.getPlayer(), data.getPhase());
            nextPhaseLoad = new StateWait(controller, data, manageMessage, view);
        }
            
        else
        {
            controller.setLocalMode();
            nextPhaseLoad = new StateDeploy(controller, data, manageMessage, view);
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
        return nextPhaseLoad;
    }

    public override StateControl nextPhaseForced()
    {
        notifyPassTurn();
        return nextPhaseLoad;
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

    public override string needSaving(string land)
    {
        string field = "";
        if(data.getPlayerByLand(land).Equals(data.getPlayer()))
        {
            if(controller.getFirstLand() == null || //there is no land selected 
               controller.getFirstLand().Equals(land) || //the first land is the same selected
               controller.getSecondLand() != null && controller.getSecondLand().Equals(land))//the second land is the same selected
            {
                controller.resetMemoryBuffer();
                field = "firstLand";
                view.updateTwoSelected(land, "Selected a state !!!");
            }
            else if(data.areNeighbor(controller.getFirstLand(), land))
            {
                field = "secondLand";
                view.updateTwoSelected(controller.getFirstLand(), land);
            }
        }
        return field;
    }
}
