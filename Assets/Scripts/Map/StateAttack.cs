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

    public StateAttack(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
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
        string error = "";
        loadNecessaryData();
        if(!data.isValidAttack(firstLand, secondLand, nTanks))
            error = "some data are not corrected";
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            view.changeCanvasOption("Wait");
            data.setDefendPhase(secondLand);
            view.updatePhase(data.getPlayer(), data.getPhase());
            string message = manageMessage.messageInitiateCombat(firstLand, secondLand, nTanks);
            view.updateLogEvent(manageMessage.readInitiateCombat(message));
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
        string message = manageMessage.messagePhase(data.getPlayer(), data.getPhase());
        DataSender.SendNextPhase(message);
        return (new StateMove(controller, data, manageMessage, view));
    }

    public override StateControl nextPhaseForced()
    {
        return (new StateWait(controller, data, manageMessage, view));
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
