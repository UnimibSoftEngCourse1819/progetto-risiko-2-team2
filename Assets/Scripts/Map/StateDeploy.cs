using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeploy : StateControl
{
    private ControllGameMap controller;
    private DataManager data;
    private string land;
    private int nTanks;

    public StateDeploy(ControllGameMap controller, DataManager data)
    {
        this.controller = controller;
        this.data = data;
        land = null;
        nTanks = -1;
    }

    public override string action()
    {
        string error = "";
        loadNecessaryData();
        error = data.addTanks(land, nTanks);
        if(error.Equals(""))
        {
            string message = manageMessage.messageDeploy(controller.getPlayer(), nTanks, land);
            view.updateDeploySelected("Select a Land !!!");
            view.updateRemainTank(nTanksRemain);
            view.updateLogEvent(manageMessage.readDeploy());
            DataSender.sendPosizionamento(message);
        }
        return error;
    }

    private void loadNecessaryData()
    {
        land = controller.getFirstLand();
        nTanks = controller.getDeployTank();
    }

    public override StateControl nextPhase()
    {
        data.nextPhase();
        string message = manageMessage.messagePhase(data.getPlayer(), data.getCurrentPhase());
        DataSender.sendNextPhase(message);
        return (new StateAttack(controller,data));
    }

    public override List<string> getMissingData()
    {
        List<string> missingData = new List<string>();

        if (controller.getFirstLand() == null)
            missingData.Add("Land selected ");
        if (controller.getDeployTank() <= 0)
            missingData.Add("Valid Number of Tanks");
        return missingData;
    }

    public override StateControl nextPhaseForced()
    {
        return null;
    }

}
