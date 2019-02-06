using System.Collections.Generic;
using UI;
using Communication;
using Model;

namespace Controller
{
    public class StateWait : StateControl
    {
        private readonly ControllGameMap controller;
        private readonly DataManager data;
        private readonly MessageManager manageMessage;
        private readonly ViewGameMap view;
        private readonly string typeMessage, messageData;
        private StateControl nextPhaseLoad;

        public StateWait(ControllGameMap controller, DataManager data, MessageManager manageMessage, ViewGameMap view)
        {
            this.controller = controller;
            this.data = data;
            this.manageMessage = manageMessage;
            this.view = view;
            typeMessage = "";
            messageData = "";
        }

        public override string action()
        {
            string needChangePhase = "";
            loadNecessaryData();
            switch (typeMessage)
            {
                case "Cards":
                    CardMessage();
                    break;
                case "Deploy":
                    DeployMessage();
                    break;
                case "Attack":
                    AttackMessage();
                    break;
                case "Defend":
                    DefendMessage();
                    break;
                case "Move":
                    MoveMessage();
                    break;
                case "nextTurn":
                    NextMessage();
                    break;
            }
            if (nextPhaseLoad != this)
                needChangePhase = "FORCE_NEXT_PHASE";
            return needChangePhase;
        }

        private void CardMessage()
        {
            view.updateLogEvent(manageMessage.readUsedCards(messageData));
            data.useCards(manageMessage.getCard1(), manageMessage.getCard2(), manageMessage.getCard3());
            nextPhaseLoad = this;
        }

        private void DeployMessage()
        {
            view.updateLogEvent(manageMessage.readDeploy(messageData));
            data.addTanks(manageMessage.getLandStart(), manageMessage.getNTank1());
            nextPhaseLoad = this;
        }

        private void AttackMessage()
        {
            view.updateLogEvent(manageMessage.readInitiateCombat(messageData));
            data.setDefendPhase(manageMessage.getLandEnd());
            view.updatePhase(data.getPlayer(), data.getPhase());
            if (!data.getPlayer().Equals(controller.getPlayer()))//check if the player is NOT the one targeted
            {
                nextPhaseLoad = this;
            }
            else
            {
                view.changeCanvasOption("Defend phase");
                nextPhaseLoad = new StateDefend(controller, data, manageMessage, view);
            }
        }

        private void DefendMessage()
        {
            view.updateLogEvent(manageMessage.readDefend(messageData));
            data.setAttackPhase(manageMessage.getLandStart());
            view.updatePhase(data.getPlayer(), data.getPhase());
            if (!data.getPlayer().Equals(controller.getPlayer()))//check if the player is NOT the one who engaged the battle
            {
                nextPhaseLoad = this;
            }
            else
            {
                view.changeCanvasOption("Attack phase");
                view.updateTwoSelected(null, null);
                nextPhaseLoad = new StateAttack(controller, data, manageMessage, view);              
            }
        }

        private void MoveMessage()
        {
            view.updateLogEvent(manageMessage.readMove(messageData));
            data.passTurn();
            view.updatePhase(data.getPlayer(), data.getPhase());
            if (!data.getPlayer().Equals(controller.getPlayer()))
            {
                nextPhaseLoad = this;
            }
            else
            {
                view.changeCanvasOption("Deployment phase");
                view.updateDeploySelected("Select a Land !!!");
                nextPhaseLoad = new StateDeploy(controller, data, manageMessage, view);
            }
        }

        private void NextMessage()
        {

            string infoData = manageMessage.readPhase(messageData);
            data.setPhaseByMessage(infoData);
            view.updatePhase(data.getPlayer(), data.getPhase());
            if (!data.getPlayer().Equals(controller.getPlayer()))
            {
                nextPhaseLoad = this;
            }
            else// this mean it's turn of the player, so we are in certain deploy phase
            {
                if (data.getPhase().Equals("Deployment phase")) //checking if we are in deploy game
                {
                    view.changeCanvasOption("Deployment phase");
                    view.updateDeploySelected("select a Land !!!");
                    nextPhaseLoad = new StateDeploy(controller, data, manageMessage, view);
                }
                else
                {
                    view.changeCanvasOption("Initial Deploy phase");
                    view.updateDeploySelected("select a Land !!!");
                    nextPhaseLoad = new StateStartDeploy(controller, data, manageMessage, view);
                }
            }
        }

        private void loadNecessaryData()
        {
            controller.getMessage();
            controller.getTypeMessage();
        }

        public override StateControl nextPhase()
        {
            return null;
        }

        public override List<string> getMissingData()
        {
            return new List<string>();
        }

        public override StateControl nextPhaseForced()
        {
            return nextPhaseLoad;
        }

        public override string needSaving(string land)
        {
            return "";
        }
    }
}
