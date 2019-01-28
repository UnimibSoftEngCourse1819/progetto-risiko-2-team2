using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private const string DEPLOYMENT = "Deployment phase";
    private const string ATTACK = "Attack phase";
    private const string MOVE = "Move phase";
    private const int MINIMUM_TANK_ON_LAND = 1;
    private const int MINIMUM_TANK_ATTACK_PER_TIME = 1, MAX_TANK_ATTACK_PER_TIME = 3;

    private GameManager gameManager;
    private List<Player> players;
    private List<Continent> world;
    private Player currentPlayer;
    private string currentPhase;
    private Dealer dealer;

    public DataManager(List<Player> players, List<Continent> world, List<Land> lands)
    {
        gameManager = new GameManager();
        this.players = players;
        this.world = world;
        currentPlayer = this.players[0];
        dealer = new Dealer(lands);
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

    private bool checkedRispectiveOwners(Land attacker, Land defender)//controlla che lo stato attacante è di sua proprietà e quello difensivo non sia suo
    {
        return (currentPlayer.hasLand(attacker.getName()) && !currentPlayer.hasLand(defender.getName()));
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
        return ((nTankAttacker >= 1 && nTankAttacker <= MAX_TANK_ATTACK_PER_TIME &&
                (currentAttackerTanks - MINIMUM_TANK_ON_LAND) >= nTankAttacker) &&
                ((nTankDefender >= 1 && nTankDefender <= MAX_TANK_ATTACK_PER_TIME &&
                currentDefenderTanks >= nTankDefender)));
    }

    public string attack(string attacker, string defender, int nTankAttacker, int nTankDefender)
    {
        int currentAttackerTanks = findLandByName(attacker).getTanksOnLand();
        int currentDefenderTanks = findLandByName(defender).getTanksOnLand();
        string result = null;
        
        if (checkedRispectiveOwners(findLandByName(attacker), findLandByName(defender)) && checkedTankNumbers(currentAttackerTanks, currentDefenderTanks, nTankAttacker, nTankDefender))
        {
            gameManager.attack(findLandByName(attacker), findLandByName(defender), nTankAttacker, nTankDefender);

            if (findLandByName(defender).getTanksOnLand() == 0)
            {
                foreach (Player player in players)
                {
                    if (player.hasLand(defender))
                    {
                        gameManager.passLand(findLandByName(defender), player, currentPlayer, nTankAttacker);
                    }
                }
            }
        }
        else
            result = "Land not belonging to their respective owners or tank number is not correct";

        return result;
    }

    public void passTurn()
    {
        //cambia currentPlayer
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

    public string moveTanks(string startLand, string endLand, int nTank)
    {
        string result = null;
        
        if (currentPlayer.hasLand(startLand) && currentPlayer.hasLand(endLand) &&
            (findLandByName(startLand).getTanksOnLand() - 1) >= nTank)
            gameManager.moveTanks(findLandByName(startLand), findLandByName(endLand), nTank);
        else
            result = "One of the lands don't belong to the current player or the number of tanks you want to " +
                "move is not allowed!";

        return result;
    }

    public string addTanks(string land, int nTank)
    {
        string result = null;

        if (currentPlayer.hasLand(land))
            gameManager.addTanks(findLandByName(land), nTank);
        else
            result = "This land doesn't belong to the player";

        return result;
    }

    public List<string> getDataForView()
    {
        List<string> data = new List<string>();
        data.Add(currentPlayer.getName());
        data.Add(getPlayerData());
        return data;
    }

    private string getPlayerData()
    {
        string data = "";
        List<Land> landsOwned = currentPlayer.getTerritoryOwned();
        foreach (Land land in landsOwned)
        {
            data += land.getName() + " tank: " + land.getTanksOnLand() + " ";
        }

        return data;
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
        if (cards[0].isJolly() && cards[1].getSymbol().Equals(cards[2].getSymbol()) && !cards[1].isJolly())
            return true;
        else if (cards[1].isJolly() && cards[0].getSymbol().Equals(cards[2].getSymbol()) && !cards[0].isJolly())
            return true;
        else if (cards[2].isJolly() && cards[0].getSymbol().Equals(cards[1].getSymbol()) && !cards[0].isJolly())
            return true;

        return false;
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

    public string useCards(List<LandCard> selectedCards)
    {
        string result = null;
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
}