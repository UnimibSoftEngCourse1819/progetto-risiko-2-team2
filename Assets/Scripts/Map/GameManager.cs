using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager
{
    public void attack(Land attacker, Land defender, int nTankAttacker, int nTankDefender)
    {
        /*
        simula un attacco con facendo tutti i controlli (attacker deve essere il currentplayer)
        se defender rimane con 0 di tank far in modo che la land passi al player
        */
        
        List<int> attackerDices = new List<int>();
        List<int> defenderDices = new List<int>();

        rollDices(attackerDices, nTankAttacker);
        rollDices(defenderDices, nTankDefender);

        checkResults(attackerDices, defenderDices, attacker, defender);
    }

    public void passLand(Land landAttacker, Land landDefender, Player oldOwner, Player newOwner, int tanks)
    {
        oldOwner.removeLand(landDefender);
        newOwner.addLand(landDefender);
        moveTanks(landAttacker, landDefender, tanks);
    }

    public void moveTanks(Land startLand, Land endLand, int nTank)
    {     
        startLand.removeTanksOnLand(nTank);
        endLand.addTanksOnLand(nTank);
    }

    public void addTanks(Player player, Land land, int nTank)
    {
        player.removeTanks(nTank);
        land.addTanksOnLand(nTank);
    }

    private void checkResults(List<int> attackerDices, List<int> defenderDices, Land attacker , Land defender)
    {
        for (int i = 0; i < attackerDices.Count; i++)
            {
                // Se il difensore ha tank con cui difendersi
                if (i <= defenderDices.Count - 1)
                {
                    // Se attaccante vince il difensore perde 1 tank
                    if (attackerDices[i] > defenderDices[i])
                    {
                        defender.removeTanksOnLand(1);
                    }
                    else // altrimenti attaccante perde 1 tank
                    {
                        attacker.removeTanksOnLand(1);
                    }
                }
                else 
                {
                    return;
                }
            }
    }

    private void rollDices(List<int> dices, int n)
    {
        for(int i = 0; i < n; i++)
        {
            dices.Add(UnityEngine.Random.Range(1, 6));
        }

        dices.Sort();
        dices.Reverse();
    }

    public void giveTanks(Player player, List<Continent> world)
    {
        int nTanks = player.getTotalLand() / 3;

        foreach(Continent continent in world)
        {
            if (player.hasContinent(continent))
                nTanks += continent.getBonusTank();
        }

        player.addTanks(nTanks);
    }

    public void distributeTanksToPlayers(List<Player> players)
    {
        int nTanks;
        int nPlayers = players.Count;

        if (nPlayers == 3)
            nTanks = 8; //modificami
        else if (nPlayers == 4)
            nTanks = 30;
        else if (nPlayers == 5)
            nTanks = 25;
        else
            nTanks = 20;

        foreach (Player player in players)
            player.setNTanks(nTanks);
    }

    // Restituisce il numero di tanks in base al simbolo
    private int getTanks(string symbol)
    {
        if (symbol.Equals("Cavalry"))
            return 8;
        else if (symbol.Equals("Infantry"))
            return 6;
        else
            return 4;
    }


    // Assegna al player i tanks in base al tris di carte precedentement selezionate dal player
    public void useCards(Player player, List<LandCard> cards, string type, Dealer dealer, int additionalTanks)
    {
        if (type.Equals("Equal"))
        {
            player.addTanks(getTanks(cards[0].getSymbol()) + additionalTanks);
            player.removeLandCard(cards);
            dealer.addCardsToDeck(cards);
        }
        else if (type.Equals("Different"))
        {
            player.addTanks(10 + additionalTanks);
            player.removeLandCard(cards);
            dealer.addCardsToDeck(cards);
        }
        else
        {
            player.addTanks(12 + additionalTanks);
            player.removeLandCard(cards);
            dealer.addCardsToDeck(cards);
        }         
    }

    public void giveCard(Player player)
    {
        
    }
}