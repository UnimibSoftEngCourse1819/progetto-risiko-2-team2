using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDefend : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private string firstLand, secondLand;
    private int nTanksAttacker, nTanksDefender;

    public StateDefend(ControllGameMap controller, DataManager data)
    {
        this.controller = controller;
        this.data = data;
        firstLand = null;
        secondLand = null;
        nTanksAttacker = -1;
        nTanksDefender = -1;
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        int lossTanksAttacker = data.getTanks(firstLand);
        int lossTanksDefender = data.getTanks(secondLand);
        error = data.attack(firstLand, secondLand, nTanksAttacker, nTanksDefender);
        if(error.Equals(""))
        {
            error = "FORCE_NEXT_PHASE";
            view.hideAllCanvasOption();
            data.setAttackPhase(firstLand);
            lossTanksAttacker -= data.getTanks(firstLand);
            lossTanksDefender -= data.getTanks(secondLand);
            view.updatePhase(data.getPlayer(), data.getCurrentPhase());
            string message = manageMessage.messageDefend(secondLand,  firstLand, lossTanksAttacker, lossTanksDefender, nTanksDefender, result);
            DataSender.SendAttacco(message);
        }
        return error;
    }

    private void loadNecessaryData()
    {
        firstLand = controller.getFirstLand();
        secondLand = controller.getSecondLand();
        nTanksAttacker = controller.getTank1();
        nTanksDefender = controller.getDefendTank();
    }

    public override StateControl nextPhaseForced()
    {
        return (new StateWait(controller, data));
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
