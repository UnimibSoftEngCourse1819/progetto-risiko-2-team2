using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeploy : StateControl
{
    private ControllGameMap controller;
    private ModelGameMap model;
    private DataManager data;
    private string land, player;
    private int nTanks, nTanksRemain;

    public StateDeploy(ControllGameMap controller, ModelGameMap model, DataManager data)
    {
        this.controller = controller;
        this.data = data;
        this.model = model;
        land = null;
        player = null;
        nTanks = -1;
        nTanksRemain = -1;
    }

    private void loadNecessaryData()
    {
        land = model.getFirstLand();
        player = model.getPlayer();
        nTanks = controller.getDeployTank();
        nTanksRemain = data.getPlayerTanksReinforcement(player);
    }

    public override string action()
    {
        loadNecessaryData();

        return data.addTanks(land, nTanks);
    }

    public override StateControl nextPhase()
    {
        return (new StateAttack(controller, model, data));
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
