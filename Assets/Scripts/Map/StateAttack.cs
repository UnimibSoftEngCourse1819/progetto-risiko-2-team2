using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Server;

public class StateAttack : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private MessageManager manageMessage;
    private ViewGameMap view;
    private string firstLand, secondLand;
    private int nTanks;
    private StateControl nextPhaseLoad;

    public StateAttack(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.view = view;
        this.manageMessage = manageMessage;
        nextPhaseLoad = this;
        firstLand = "";
        secondLand = "";
        nTanks = -1;
        controller.resetMemoryBuffer();
        view.updatePhase(data.getPlayer(), data.getPhase());
        view.changeCanvasOption("Attack phase");
        view.updateTwoSelected("", "");
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        if(!data.isValidAttack(firstLand, secondLand, nTanks))
            error = "some data are not correct";
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            data.setDefendPhase(secondLand);
            string message = manageMessage.messageInitiateCombat(firstLand, secondLand, nTanks);
            view.updateLogEvent(manageMessage.readInitiateCombat(message));
            DataSender.SendAttackDeclared(message);
            if(!controller.isLocalMode())
            {
                nextPhaseLoad = new StateWait(controller, data, manageMessage, view);
            }
            else
            {
                nextPhaseLoad = new StateDefend(controller, data, manageMessage, view);
                controller.setLocalMode();
                controller.setFirstLand(firstLand);
                controller.setSecondLand(secondLand);
                controller.setTank1(nTanks);
            }
        }
        return error;
    }

    private void loadNecessaryData()
    {
        firstLand = controller.getFirstLand();
        secondLand = controller.getSecondLand();
        nTanks = controller.getAttackTank();
    }

    public override StateControl nextPhase()
    {
        data.nextPhase();
        string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
        DataSender.SendNextPhase(message);
        return new StateMove(controller, data, manageMessage, view);
    }

    public override StateControl nextPhaseForced()
    {
        return nextPhaseLoad;
    } 

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land attacker ");
        if (controller.getSecondLand() == null)
            missingData.Add("Land defender ");
        if (controller.getDeployTank() <= 0)
            missingData.Add("Number tank");
        return missingData;
    }

    public override string needSaving(string land)
    {
        string field = "";
        if(data.getPlayerByLand(land).Equals(data.getPlayer()))
        {
            field = "firstLand";
            view.updateTwoSelected(land, controller.getSecondLand());
        }
        else
        {
            field = "secondLand";
            view.updateTwoSelected(controller.getSecondLand(), land);
        }
        return field;
    }
}
