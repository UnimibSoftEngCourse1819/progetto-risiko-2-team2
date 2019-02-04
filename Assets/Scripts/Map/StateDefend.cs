using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Server;

public class StateDefend : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private MessageManager manageMessage;
    private ViewGameMap view;
    private string firstLand, secondLand;
    private int nTanksAttacker, nTanksDefender;

    public StateDefend(ControllGameMap controller,  DataManager data, MessageManager manageMessage, ViewGameMap view)
    {
        this.controller = controller;
        this.data = data;
        this.manageMessage = manageMessage;
        this.view = view;
        firstLand = null;
        secondLand = null;
        nTanksAttacker = -1;
        nTanksDefender = -1;
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        int lossTanksAttacker = data.getTankOfLand(firstLand);
        int lossTanksDefender = data.getTankOfLand(secondLand);
        error = data.attack(firstLand, secondLand, nTanksAttacker, nTanksDefender);
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            view.changeCanvasOption("Wait");
            data.setAttackPhase(firstLand);
            lossTanksAttacker -= data.getTankOfLand(firstLand);
            lossTanksDefender -= data.getTankOfLand(secondLand);
            view.updatePhase(data.getPlayer(), data.getPhase());
            string result = "";
            if(data.getPlayerByLand(secondLand).Equals(data.getPlayerByLand(firstLand)))
                result = "The land has been conquered";
            else
                result = "The land has not been conquered";
            string message = manageMessage.messageDefend(secondLand,  firstLand, lossTanksAttacker, lossTanksDefender, "" + nTanksDefender, result);
            DataSender.SendAttacco(message);
        }
        return error;
    }

    private void loadNecessaryData()
    {
        firstLand = controller.getFirstLand();
        secondLand = controller.getSecondLand();
        nTanksAttacker = controller.getAttackTank();
        nTanksDefender = controller.getTank1();
    }

    public override StateControl nextPhaseForced()
    {
        return (new StateWait(controller, data, manageMessage, view));
    }

    public override StateControl nextPhase()
    {
        return null;
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land attacker ");
        if (controller.getSecondLand() == null)
            missingData.Add("Land defender ");
        if (controller.getTank1() <= 0)
            missingData.Add("Number tank of attacker");
        if (controller.getDefendTank() <= 0)
            missingData.Add("Numer tank of defender");
        return missingData;
    }
}
