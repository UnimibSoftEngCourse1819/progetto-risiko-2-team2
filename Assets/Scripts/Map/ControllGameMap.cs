using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Communication;
using Model;
using Model.Game;
using Model.Game.Map;
using Loader;
using Server;

namespace Controller
{
    public class ControllGameMap : MonoBehaviour
    {

        private InputField attackTank, defendTank, moveTank, deployTank;
        private Dropdown card1, card2, card3;

        public MessageManager messageManager;
        public DataManager model;
        public ViewGameMap view;
        public MapLoader loader;

        private StateControl state;
        private string firstLand, secondLand, player;
        private int tank1, tank2;
        private string message, typeMessage;
        private readonly bool localMode = true;

        private void Awake()//prepara i componenti da cui prendere gli input
        {

            InizializeComponents();
            view.prepareView();
            loadData();
            NetworkManager.istance.InizializeController();
            initializeText();
            initializeController();
        }

        //Private Methods for inizializing

        private void initializeText()
        {
            view.updateTextPlayerData(model.getPlayerData(player));
            view.updateGoal(model.getGoalData(player));
            view.updateCardList(model.getLandCardData(player));
        }

        private void initializeController()
        {
            state = new StateStartDeploy(this, model, messageManager, view);
        }

        private void InizializeComponents()
        {
            //InputField
            attackTank = GameObject.Find("InputFieldAttackTank").GetComponent<InputField>();
            defendTank = GameObject.Find("InputFieldDefendTank").GetComponent<InputField>();
            moveTank = GameObject.Find("InputFieldMoveTank").GetComponent<InputField>();
            deployTank = GameObject.Find("InputFieldDeployTank").GetComponent<InputField>();

            //Dropdown
            card1 = GameObject.Find("DropdownCard1").GetComponent<Dropdown>();
            card2 = GameObject.Find("DropdownCard2").GetComponent<Dropdown>();
            card3 = GameObject.Find("DropdownCard3").GetComponent<Dropdown>();
        }

        private void loadData()//inizializza i dei dati su cui ci si può testare la parte logica
        {
            Debug.Log("CIAONE");

            MapData data = loader.loadMap();
            view.drawMap(data.actualStates);

            List<RiskPlayer> players = new List<RiskPlayer>();

            players.Add(new RiskPlayer("Pippo", "red"));

            players.Add(new RiskPlayer("Paperino", "blue"));

            players.Add(new RiskPlayer("Topolino", "green"));

            List<Continent> world = loader.getWorld(data);
            model = new DataManager(players, world, loader.getAllLands(world));

            foreach (RiskPlayer p in players)
            {
                foreach (Land land in p.getTerritoryOwned())
                {
                    view.setColorState(land.getName(), p.getColor());
                    view.setNumberLands(land.getName(), 1);
                }
            }

            view.updateTextPlayerData(model.getPlayer());
            if (localMode)
                setLocalMode();
            Debug.Log("*************************************" + player + "***************************");
            for (int i = 0; i < 10; i++)
                model.giveCard(player);
        }

        public void resetMemoryBuffer()
        {
            firstLand = null;
            secondLand = null;
            tank1 = -1;
            tank2 = -1;
        }

        //Methods that return value from UI components

        public int getDeployTank()
        {
            return int.Parse(deployTank.text);
        }

        public int getAttackTank()
        {
            return int.Parse(attackTank.text);
        }

        public int getMoveTank()
        {
            return int.Parse(moveTank.text);
        }

        public int getDefendTank()
        {
            return int.Parse(defendTank.text);
        }

        //Methods get

        public string getFirstLand()
        {
            return firstLand;
        }

        public string getSecondLand()
        {
            return secondLand;
        }

        public int getTank1()
        {
            return tank1;
        }

        public int getTank2()
        {
            return tank2;
        }

        public string getPlayer()
        {
            return player;
        }

        public string getMessage()
        {
            return message;
        }

        public string getTypeMessage()
        {
            return typeMessage;
        }

        // Button methods

        public void onClickAction()
        {
            Debug.Log("state : " + state);
            List<string> error = state.getMissingData();
            if (error.Count != 0)
            {
                int count = 0;
                string messageToDisplay = "You can't do that action because are missing some data : ";
                foreach (string missing in error)
                {
                    messageToDisplay += missing;
                    if (count != error.Count)
                        messageToDisplay += ", ";
                }
                view.showMessage(messageToDisplay);
            }
            else
            {
                string errorAction = state.action();
                if (errorAction.Equals("FORCE_NEXT_PHASE"))
                    state = state.nextPhaseForced();
                else if (!errorAction.Equals(""))
                    view.showMessage(errorAction);
            }
            view.updateTextPlayerData(model.getPlayerData(player));
            if (firstLand != null && !firstLand.Equals(""))
                view.updateLandText(model.getLandData(firstLand));

            if (model.hasWon(player))
                view.showMessage(player + " has won!");
        }

        public void onClickUseCard()
        {
            if (card1.value == card2.value || card2.value == card3.value || card1.value == card3.value)
                view.showMessage("You can't select the same cards");

            else
            {
                string error = "";
                error += model.useCards(card1.value, card2.value, card3.value);
                if (!error.Equals(""))
                    view.showMessage(error);
                else
                {
                    onClickCloseCard();
                    view.updateTextPlayerData(model.getPlayerData(player));
                    view.updateCardList(model.getLandCardData(player));
                    string messageToDisplay = messageManager.messageUsedCards(player, card1.value, card2.value, card3.value);
                    view.updateLogEvent(messageManager.readUsedCards(messageToDisplay));
                    view.updateTanksRemain(model.getPlayerTanksReinforcement(player));
                    DataSender.SendComboCarte(messageToDisplay);
                }
            }
        }

        public void onClickExit()
        {
            //esce dal game
        }

        public void onClickQuit()
        {
            view.showConfirmQuit();
        }

        public void onClickShowCards()
        {
            List<string> options = model.getListCard(player);
            if (options.Count < 3)
                view.showMessage("You have 2 or less cards !!!");
            else
                view.showCards(options);
        }

        public void onClickClosePopup()
        {
            view.closePopup();
        }

        public void onClickCloseCard()
        {
            view.closeCard();
        }

        public void onClickNextPhase()
        {
            state = state.nextPhase();
        }

        public void onClickLand(string land)
        {
            model.getLandData(land);
            view.updateLandText(model.getLandData(land));
            string field = state.needSaving(land);
            if (field.Equals("firstLand"))
                firstLand = land;
            else if (field.Equals("secondLand"))
                secondLand = land;

        }
        //local mode methods

        public void setLocalMode()
        {
            player = model.getPlayer();
            view.updateTextPlayerData(model.getPlayerData(player));
            view.updateCardList(model.getLandCardData(player));
            view.updateGoal(model.getGoalData(player));
        }

        public bool isLocalMode()
        {
            return localMode;
        }

        public void setFirstLand(string land)
        {
            firstLand = land;
        }

        public void setSecondLand(string land)
        {
            secondLand = land;
        }

        public void setTank1(int n)
        {
            tank1 = n;
        }

        public void setTank2(int n)
        {
            tank2 = n;
        }

        // methods for networks
        public void receiveMessage(string type, string message)
        {
            typeMessage = type;
            this.message = message;
            onClickAction();
        }
    }
}