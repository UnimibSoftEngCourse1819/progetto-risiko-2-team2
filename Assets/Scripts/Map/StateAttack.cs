﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private string firstLand, secondLand;
    private int nTanks;

    public StateAttack(ControllGameMap controller, DataManager data)
    {
        this.controller = controller;
        this.data = data;
        firstLand = null;
        secondLand = null;
        nTanks = -1;
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        error = data.isValidAttack(firstLand, secondLand, nTanks);
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            view.hideAllCanvasOption();
            data.setDefendPhase(secondLand);
            view.updatePhase(data.getPlayer(), data.getCurrentPhase());
            string message = manageMessage.messageInitiateCombat(landAttacker, landDefender, nTank);
            view.updateEventLog(manageMessage.readInitiateCombat(message));
            DataSender.SendAttackDeclared(message);
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
        string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
        DataSender.sendNextPhase(message);
        return (new StateMove(controller, data));
    }

    public override StateControl nextPhaseForced()
    {
        return (new StateWait(controller, data));
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
}
