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
            nextPhaseLoad = new StateWait(controller, data, manageMessage, view);
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
            if(controller.getFirstLand().Equals("") || controller.getFirstLand().Equals(land) || controller.getSecondLand().Equals(land))
            {
                /* from left to right check
                -the first field is empty
                -the player already selected the state as start
                -the player already seleceted the state as end
                */
                field = "firstLand";
                view.updateTwoSelected(land, controller.getSecondLand());
            }
            else
            {
                if(controller.getSecondLand().Equals("") || data.areNeighbor(controller.getFirstLand(), land))
                {
                    field = "secondLand";
                    view.updateTwoSelected(controller.getFirstLand(), land);
                }
            }
        }
        return field;
    }
}
