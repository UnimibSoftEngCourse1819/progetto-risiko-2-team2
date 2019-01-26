using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager
{

    private List<Player> players;
    private List<Continent> World;
    private Player currentPlayer;

    private const int MINIMUM_TANK_ON_LAND = 1;
    private const int MINIMUM_TANK_ATTACK_PER_TIME = 1, MAX_TANK_ATTACK_PER_TIME = 3;

    public GameManager(List<Player> players, List<Continent> World)
    {
        this.players = players;
        this.World = World;
        currentPlayer = this.players[0];
    }

    public void attack(string attacker, string defender, int nTankAttacker, int nTankDefender)
    {
    	attack(FindLandByName(attacker), FindLandByName(defender), nTankAttacker, nTankDefender);
    }

    public void attack(Land attacker, Land defender, int nTankAttacker, int nTankDefender)
    {
        /*
        simula un attacco con facendo tutti i controlli (attacker deve essere il currentplayer)
        se defender rimane con 0 di tank far in modo che la land passi al player
        */
        int currentAttackerTanks = attacker.getTanksOnLand();
        int currentDefenderTanks = defender.getTanksOnLand();

        if(checkedRispectiveOwners(attacker, defender) && checkedTankNumbers(currentAttackerTanks, currentDefenderTanks, nTankAttacker, nTankDefender))
        {
            List<int> attackerDices = new List<int>();
            List<int> defenderDices = new List<int>();

            rollDices(attackerDices, nTankAttacker);
            rollDices(defenderDices, nTankDefender);

            checkResults(attackerDices, defenderDices, attacker, defender);

            if(defender.getTanksOnLand() == 0)
            {
                foreach(Player player in players)
                {
                    if(player.hasLand(defender.getName()))
                    {
                        passLand(defender, player, currentPlayer, nTankAttacker);

                        return;
                    }
                }
            }
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

    private void passLand(Land land, Player oldOwner, Player newOwner, int tanks)
    {
        oldOwner.removeLand(land);
        newOwner.addLand(land);
        land.addTanksOnLand(tanks);
    }

	public void move(string startLand, string endLand, int nTank)
	{
		move(FindLandByName(startLand), FindLandByName(endLand), nTank);
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

    public void addTroup(string land, int nTank)
    {
    	addTroup(FindLandByName(land), nTank);
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
        foreach(Land land in landsOwned)
        {
            data += land.getName() + " tank: " + land.getTanksOnLand() + " ";
        }

        return data;
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
            dices.Add(Random.Range(1, 6));
        }

        dices.Sort();
        dices.Reverse();
    }

    private Land FindLandByName(string name)
	{
		Land result = null;
		foreach (Continent continent in World)
		{
			List<Land> lands = continent.getLands();
			foreach(Land land in lands)
			{
				if(land.getName() == name)
				{
					result = land;
				}
			}
			
		}
		return result;
	}
}