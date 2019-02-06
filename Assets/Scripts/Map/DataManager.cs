using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private const string START_DEPLOYMENT = "Initial Deploy phase";
    private const string DEPLOYMENT = "Deployment phase";
    private const string ATTACK = "Attack phase";
    private const string MOVE = "Move phase";
    private const string DEFEND = "Defend phase";
    private const int MINIMUM_TANK_ON_LAND = 1;
    private const int MINIMUM_TANK_ATTACK_PER_TIME = 1, MAX_TANK_ATTACK_PER_TIME = 3;

    private readonly GameManager gameManager;
    private readonly List<Player> players;
    private readonly List<Continent> world;
    private Player currentPlayer;
    private string currentPhase;
    private readonly Dealer dealer;

    public DataManager(List<Player> players, List<Continent> world, List<Land> lands)
    {
        gameManager = new GameManager();
        this.players = players;
        this.world = world;
        currentPlayer = this.players[0];
        currentPhase = START_DEPLOYMENT;
        dealer = new Dealer(lands);

        Debug.Log("The killer!");

        gameManager.distributeTanksToPlayers(players);
        dealer.drawCards(players);

        Debug.Log("Gnè!");

        foreach (Player player in players)
            dealer.assignGoal(players, player, world);

        Debug.Log("Quello che vuoi");
    }

    public string getPlayerData()
    {
        return getPlayerData(currentPlayer.getName());
    }


    private Player getPlayerByName(string name)
    {
        Player searching = null;
        foreach (Player player in players)
        {
            if (player.getName().Equals(name))
                searching = player;
        }
        return searching;
    }

    public string getPlayerData(string name)
    {
        string data = name + " reserve tanks: " + getPlayerTanksReinforcement(name);
        return data;
    }

    public List<string> getListCard(string name)
    {
        return getPlayerByName(name).getListCard();
    }

    public string getPlayer()
    {
        return currentPlayer.getName();
    }

    public string getPlayerByLand(string land)
    {
        string result = "";
        foreach (Player player in players)
        {
            if (player.hasLand(land))
                result = player.getName();
        }
        return result;
    }

    public string getPhase()
    {
        return currentPhase;
    }

    public int getTankOfLand(string name)
    {
        return findLandByName(name).getTanksOnLand();
    }

    public int getPlayerTanksReinforcement(string name)
    {
        return getPlayerByName(name).getNTanks();
    }

    public string getLandData(string land)
    {
        string result = "";
        Land selectedLand = findLandByName(land);
        if(selectedLand != null)
        {
        	result += land + System.Environment.NewLine;
        	result += findContinentByLand(selectedLand).getName() + System.Environment.NewLine;
	        result += getPlayerByLand(land) + System.Environment.NewLine;
	        result += selectedLand.getTanksOnLand() + System.Environment.NewLine;
	        foreach(Land neighbor in selectedLand.getNeighbors())
	        {
	            result += neighbor.getName() + System.Environment.NewLine;
	        }
        }
        return result;
    }

    public string getGoalData(string player)
    {
        return getPlayerByName(player).getGoal().getText(); 
    }

    private bool checkedRispectiveOwners(Land attacker, Land defender)//controlla che lo stato attacante è di sua proprietà e quello difensivo non sia suo
    {
        return (currentPlayer.hasLand(defender.getName()) && !currentPlayer.hasLand(attacker.getName()));
    }

    private bool checkedTankNumbers(int currentAttackerTanks, int currentDefenderTanks, int nTankAttacker, int nTankDefender)
    {
        /*
            In ordine della condizione controlla :
            -ci sia almeno 1 tank che attacchi
            -il numero dei tank attaccanti non sia superiore a quello permesso
            -rimangano almeno certo numero di tank dallo stato attacante
            -ci sia almeno 1 tank che difenda
            -il numero dei tank difensori non sia superiore a quello permesso
            -controlla che il difensore non usi più tank di quelli che ha
        */

        Debug.Log(nTankAttacker + ">=" + MINIMUM_TANK_ATTACK_PER_TIME);
        Debug.Log(nTankAttacker + "<=" + MAX_TANK_ATTACK_PER_TIME);
        Debug.Log(currentAttackerTanks + "-" + MINIMUM_TANK_ON_LAND + ">=" + nTankAttacker);
        Debug.Log(nTankDefender + ">=" + MINIMUM_TANK_ATTACK_PER_TIME);
        Debug.Log(nTankDefender + "<=" + MAX_TANK_ATTACK_PER_TIME);
        Debug.Log(currentDefenderTanks + ">=" + nTankDefender);




        return ((nTankAttacker >= MINIMUM_TANK_ATTACK_PER_TIME && nTankAttacker <= MAX_TANK_ATTACK_PER_TIME &&
                (currentAttackerTanks - MINIMUM_TANK_ON_LAND) >= nTankAttacker) &&
                (nTankDefender >= MINIMUM_TANK_ATTACK_PER_TIME && nTankDefender <= MAX_TANK_ATTACK_PER_TIME &&
                currentDefenderTanks >= nTankDefender));
    }

    // Controlla se c'è più di una carta jolly all'interno del tris
    private bool moreJolly(List<LandCard> cards)
    {
        bool moreJolly = false;

        foreach (LandCard card in cards)
        {
            if (card.isJolly() && moreJolly)
                return true;
            else if (card.isJolly())
                moreJolly = true;
        }

        return false;
    }

    // Controlla che le tre carte abbiano lo stesso simbolo
    private bool areEqual(List<LandCard> cards)
    {
        if (cards[0].getSymbol().Equals(cards[1].getSymbol())
            && cards[1].getSymbol().Equals(cards[2].getSymbol()))
            return true;

        return false;
    }

    // Controlla che le tre carte abbiano diverso simbolo
    private bool areAllDifferent(List<LandCard> cards)
    {
        if (!cards[0].getSymbol().Equals(cards[1].getSymbol()) &&
            !cards[0].getSymbol().Equals(cards[2].getSymbol()) &&
            !cards[1].getSymbol().Equals(cards[2].getSymbol()))
        {
            return true;
        }

        return false;
    }

    // Controlla che le carte selezionate siano un Jolly e due carte di ugual simbolo
    private bool isTrisWithJolly(List<LandCard> cards)
    {
        bool result = false;
        if (cards[0].isJolly() && cards[1].getSymbol().Equals(cards[2].getSymbol()) && !cards[1].isJolly())
            result = true;
        else if (cards[1].isJolly() && cards[0].getSymbol().Equals(cards[2].getSymbol()) && !cards[0].isJolly())
            result = true;
        else if (cards[2].isJolly() && cards[0].getSymbol().Equals(cards[1].getSymbol()) && !cards[0].isJolly())
            result = true;
        return result;
    }

    public bool isAllPlayerRunOutOfTanks()
    {
        foreach (Player player in players)
        {
            if (player.getNTanks() > 0)
                return false;
        }

        return true;
    }

    public bool isValidAttack(string firstLand, string secondLand,int nTank)
    {
        bool result = false;
        if(getPlayerByLand(firstLand).Equals(currentPlayer.getName()) && !(getPlayerByLand(secondLand).Equals(currentPlayer.getName()))
            && getTankOfLand(firstLand) > nTank && nTank <= MAX_TANK_ATTACK_PER_TIME && nTank >= MINIMUM_TANK_ATTACK_PER_TIME)
            result = true;
        return result;
    }

    public bool areNeighbor(string firstLand, string secondLand)
    {
        Land start = findLandByName(firstLand);
        return start.isNeighbor(secondLand);
    }

    public void startGame()
    {
        currentPhase = DEPLOYMENT;
        currentPlayer = players[0];
    }

    public string attack(string attacker, string defender, int nTankAttacker, int nTankDefender)
    {
        Land attackerLand = findLandByName(attacker);
        Land defenderLand = findLandByName(defender);

        int currentAttackerTanks = attackerLand.getTanksOnLand();
        int currentDefenderTanks = defenderLand.getTanksOnLand();

        string result = "";

        if (checkedRispectiveOwners(attackerLand, defenderLand) && checkedTankNumbers(currentAttackerTanks, currentDefenderTanks, nTankAttacker, nTankDefender))
        {
            gameManager.attack(attackerLand, defenderLand, nTankAttacker, nTankDefender);

            if (defenderLand.getTanksOnLand() == 0)
            {
                int tanksMoving = nTankAttacker - (currentAttackerTanks -  attackerLand.getTanksOnLand());
                gameManager.passLand(attackerLand, defenderLand, currentPlayer, getPlayerByName(getPlayerByLand(attacker)), tanksMoving);
            }
        }
        else
            result = "Land not belonging to their respective owners or tank number is not correct";

        return result;
    }

    public string useCards(string card1, string card2, string card3)
    {
        List<LandCard> choosed = new List<LandCard>();

        choosed.Add(currentPlayer.getCard(card1));
        choosed.Add(currentPlayer.getCard(card2));
        choosed.Add(currentPlayer.getCard(card3));

        return useCards(choosed);
    }

    private string useCards(List<LandCard> selectedCards)
    {
        string result = "";
        int additionalTanks = 0;

        if (selectedCards.Count != 3)
            result = "Insufficient number of cards";
        else if (moreJolly(selectedCards))
            result = "You can only use a jolly at a time";
        else
        {
            if (areEqual(selectedCards))
            {
                additionalTanks = hasLands(selectedCards);
                gameManager.useCards(currentPlayer, selectedCards, "Equal", dealer, additionalTanks);
            }
            else if (areAllDifferent(selectedCards))
            {
                additionalTanks = hasLands(selectedCards);
                gameManager.useCards(currentPlayer, selectedCards, "Different", dealer, additionalTanks);
            }
            else if (isTrisWithJolly(selectedCards))
            {
                additionalTanks = hasLands(selectedCards);
                gameManager.useCards(currentPlayer, selectedCards, "Jolly", dealer, additionalTanks);
            }
            else
                result = "The selected tris is not allowed";
        }

        return result;
    }

    public void giveCard(string player)
    {
        dealer.drawCard(getPlayerByName(player));
    }

    public string moveTanks(string startLand, string endLand, int nTank)
    {
        Land firstLand = findLandByName(startLand);
        Land secondLand = findLandByName(endLand);

        string result = "";

        if (currentPlayer.hasLand(startLand) && currentPlayer.hasLand(endLand) &&
            (firstLand.getTanksOnLand() - 1) >= nTank)
            gameManager.moveTanks(firstLand, secondLand, nTank);
        else
            result = "One of the lands don't belong to the current player or the number of tanks you want to " +
                "move is not allowed!";

        return result;
    }

    public string addTanks(string land, int nTank)
    {
        string result = "";

        if (!currentPlayer.hasLand(land))
            result = "This land doesn't belong to the player";
        else if(getPlayerTanksReinforcement(currentPlayer.getName()) < nTank)
            result = "You dont have enough tanks to do that ";
        else
            gameManager.addTanks(currentPlayer, findLandByName(land), nTank);

        return result;
    }

    public void nextDeploy()
    {
        nextPlayer();
        while(currentPlayer.getNTanks() == 0)
            nextPlayer();
    }

    public void nextPlayer()
    {
        int index = players.IndexOf(currentPlayer);
        if (index == players.Count - 1)
            currentPlayer = players[0];
        else
            currentPlayer = players[index + 1];
    }

    // Cambia fase di gioco
    public void nextPhase()
    {
        if (currentPhase.Equals(DEPLOYMENT))
            currentPhase = ATTACK;
        else if (currentPhase.Equals(ATTACK))
            currentPhase = MOVE;
        else
            currentPhase = DEPLOYMENT;
    }

    public void passTurn()
    {
        nextPlayer();
        nextPhase();
    }

    public void setAttackPhase(string landAttacker)
    {
        currentPlayer = getPlayerByName(getPlayerByLand(landAttacker));
        currentPhase = ATTACK;
    }

    public void setDefendPhase(string landDefender)
    {
        currentPlayer = getPlayerByName(getPlayerByLand(landDefender));
        currentPhase = DEFEND;
    }

    public void setPhaseByMessage(string message)
    {
        string[] info =  message.Split(new[] { System.Environment.NewLine },System.StringSplitOptions.None);
        currentPlayer = getPlayerByName(info[0]);
        currentPhase = info[1];
    }

    // Assegna tanks all'inizio di ogni turno
    public void giveTanks()
    {
        gameManager.giveTanks(currentPlayer, world);
    }

    // Assegna tanks ad ogni player una sola volta all'inizio del gioco
    public void distributeTanksToPlayers()
    {
        gameManager.distributeTanksToPlayers(players);
    }

    // Restituisce il numero di tank addizionali nel caso possegga delle land raffigurate nell carte
    private int hasLands(List<LandCard> cards)
    {
        int counter = 0;

        foreach (LandCard card in cards)
        {
            if (!card.isJolly() && currentPlayer.hasLand(card.getLand().getName()))
                counter += 2;
        }

        return counter;
    }

    private Land findLandByName(string name)
    {
        Land result = null;
        foreach (Continent continent in world)
        {
            List<Land> lands = continent.getLands();
            foreach (Land land in lands)
            {
                if (land.getName() == name)
                {
                    result = land;
                }
            }

        }
        return result;
    }

    private Continent findContinentByLand(Land land)
    {
        Continent c = null;

        foreach(Continent continent in world)
        {
            if (continent.hasLand(land))
                c = continent;
        }

        return c;
    }
}