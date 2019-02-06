using System.Collections.Generic;
using UnityEngine;
using Server;
using UI;
using Communication;
using Model;

namespace Controller
{
    public class StateDefend : StateControl
    {
        private readonly ControllGameMap controller;
        private readonly DataManager data;
        private readonly MessageManager manageMessage;
        private readonly ViewGameMap view;
        private string firstLand, secondLand;
        private int nTanksAttacker, nTanksDefender;
        private StateControl nextPhaseLoad;

        public StateDefend(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
        {
            this.controller = controller;
            this.data = data;
            this.manageMessage = manageMessage;
            this.view = view;
            firstLand = null;
            secondLand = null;
            nextPhaseLoad = this;
            nTanksAttacker = -1;
            nTanksDefender = -1;
            view.changeCanvasOption("Defend phase");
            view.updatePhase(data.getPlayer(), data.getPhase());
            view.updateTanksRemain(data.getTankOfLand(controller.getSecondLand()));
            Debug.Log("Data phase: " + data.getPhase() + " - State : Defense Deploy");
        }

        public override string action()
        {
            string error = "";
            loadNecessaryData();
            int lossTanksAttacker = data.getTankOfLand(firstLand);
            int lossTanksDefender = data.getTankOfLand(secondLand);
            error = data.attack(firstLand, secondLand, nTanksAttacker, nTanksDefender);
            if (error.Equals(""))
            {
                error = "FORCE_NEXT_PHASE";
                data.setAttackPhase(firstLand);
                lossTanksAttacker -= data.getTankOfLand(firstLand);
                lossTanksDefender -= data.getTankOfLand(secondLand);
                string result = "";
                if (data.getPlayerByLand(secondLand).Equals(data.getPlayerByLand(firstLand)))
                    result = "The land has been conquered";
                else
                    result = "The land has not been conquered";
                string message = manageMessage.messageDefend(secondLand, firstLand, lossTanksAttacker, lossTanksDefender, "" + nTanksDefender, result);
                view.updateLogEvent(manageMessage.readDefend(message));
                DataSender.SendAttacco(message);
                if (!controller.isLocalMode())
                    nextPhaseLoad = new StateWait(controller, data, manageMessage, view);
                else
                {
                    controller.setLocalMode();
                    nextPhaseLoad = new StateAttack(controller, data, manageMessage, view);
                }
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
            return nextPhaseLoad;
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

        public override string needSaving(string land)
        {
            return "";
        }
    }
}
