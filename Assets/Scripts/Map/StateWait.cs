using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWait : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private string typeMessage, messageData;

    public StateWait(ControllGameMap controller, DataManager data)
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
        return data.moveTanks(firstLand, secondLand, nTanks);
    }

    private void loadNecessaryData()
    {
        controller.getMessage();
    }

    public override StateControl nextPhase()
    {
        StateControl nextPhase = null;
        switch(typeMessage)
        {
            case "Attack" :
                if (data.getDefender().Equals(controller.getPlayer()))
                    nextPhase = new StateDefend(controller, data);
                else
                    nextPhase = this;
                break;
            case "Defend" :
                if (controller.getAttacker().Equals(controller.getPlayer()))
                    nextPhase = new StateAttack(controller, data);
                else
                    nextPhase = this;
                break;
            case "PassTurn" :
                if (!controller.getActualPlayer().Equals(controller.getPlayer()))
                    nextPhase = this;
                else
                    if(controller.getTypeDeploy().Equals("Start"))
                        nextPhase = new StateStartDeploy(controller, data);
                    else
                        nextPhase = new StateDeploy(controller, data);
                break;
            case "Move" :
            case "Deploy" :
                nextPhase = this;
                break;
        }
        return nextPhase;
    }

    public override List<string> getMissingData()
    {
        return new List<string>();
    }

    public override StateControl nextPhaseForced()
    {
        return null;
    }
}
