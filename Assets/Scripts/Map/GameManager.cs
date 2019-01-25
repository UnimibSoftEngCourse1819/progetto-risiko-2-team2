using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    private List<Player> players;
    private List<Continent> World;
    private Player currentPlayer;

    public void attack(Land attacker, Land defender, int nTankAttacker, int nTankDefender)
    {
        /*
        simula un attacco con facendo tutti i controlli (attacker deve essere il currentplayer)
        se defender rimane con 0 di tank far in modo che la land passi al player
        */
        int currentAttackerTanks = attacker.getTanksOnLand();
        int currentDefenderTanks = defender.getTanksOnLand();

        if(currentPlayer.hasLand(attacker.getName()) && !currentPlayer.hasLand(defender.getName())) {
            if((nTankAttacker >= 1 && nTankAttacker <= 3 && (currentAttackerTanks - 1) >= nTankAttacker)
                && ((nTankDefender >= 1 && nTankDefender <= 3 && (currentDefenderTanks - 1) >= nTankDefender)))
            {
                List<int> attackerDices = new List<int>();
                List<int> defenderDices = new List<int>();

                rollDices(attackerDices, nTankAttacker);
                rollDices(defenderDices, nTankDefender);

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
                    else //altrimenti perde 1 tank
                    {
                        defender.removeTanksOnLand(1);
                    }
                }
            }
        }
    }

    public void rollDices(List<int> dices, int n)
    {
        for(int i = 0; i < n; i++)
        {
            dices.Add(Random.Range(1, 6));
        }

        dices.Sort();
        dices.Reverse();
    }

    public void move(Land startLand, Land endLand, int nTank)
    {
        /*
        simula lo spostamento di truppe da start a end (controllare che siano di currentplayer)
        */
        if (currentPlayer.hasLand(startLand.getName()) && currentPlayer.hasLand(endLand.getName()))
        {
            int temp = startLand.getTanksOnLand();
            // Il territorio deve rimanere con almeno 1 tank
            if (temp - 1 >= nTank)
            {
                startLand.removeTanksOnLand(nTank);
                endLand.addTanksOnLand(nTank);
            }
            else
                Debug.Log("Numero truppe insufficienti");
        }
    }

    public void addTroup(Land land, int nTank)
    {
        /*
        aggiunge le truppe a land controllare che siano di currentplayer)
        */
        if(currentPlayer.hasLand(land.getName()))
        {
            land.addTanksOnLand(nTank);
        }
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
}