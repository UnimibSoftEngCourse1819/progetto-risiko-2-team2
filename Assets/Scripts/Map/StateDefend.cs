using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDefend : StateControl
{
    private ControllGameMap controller;
    private ModelGameMap model;
    private DataManager data;
    private string firstLand, secondLand, player;
    private int nTanks, nTanksRemain;

    public StateDefend(ControllGameMap controller, ModelGameMap model, DataManager data)
    {
        this.controller = controller;
        this.model = model;
        this.data = data;
        firstLand = null;
        secondLand = null;
        player = null;
        nTanks = -1;
        nTanksRemain = -1;
    }

    private void loadNecessaryData()
    {
        firstLand = model.getFirstLand();
        secondLand = model.getSecondLand();
        nTanks = controller.getAttackTank;
    }

    public override string action()
    {
        loadNecessaryData();

        return data.addTanks(land, nTanks);
    }

    public override StateControl nextPhase()
    {
        return (new StateDefend(controller, model, data));
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (model.getFirstLand() == null)
            missingData.Add("Land selected ");
        if (controller.getDeployTank() == -1)
            missingData.Add("Number of Tanks");

        return missingData;
    }
}
