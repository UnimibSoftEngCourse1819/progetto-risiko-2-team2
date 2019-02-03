using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStartDeploy : StateControl
{
    private ControllGameMap controller;
    private ModelGameMap model;
    private DataManager data;
    private string land;
    private int nTanks, nTanksRemain;

    public StateStartDeploy(ControllGameMap controller, ModelGameMap model, DataManager data)
    {
        this.controller = controller;
        this.model = model;
        this.data = data;
        land = null;
        nTanks = -1;
        nTanksRemain = -1;
    }

    private void loadNecessaryData()
    {
        land = model.getFirstLand();
        nTanks = controller.getDeployTank();
        nTanksRemain -= nTanks;
    }

    public override string action()
    {
        loadNecessaryData();

        return data.addTanks(land, nTanks);
    }

    public override StateControl nextPhase()
    {
        return this; //lascialo vuoto questo per ora se fosse stato deploy 
    	//return (new StateAttack(controll, managerData));
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
