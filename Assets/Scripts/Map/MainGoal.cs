using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGoal : Goal
{
    private readonly int nTerritoryGoal;
    private const int PERCENTAGE = 70;

    public MainGoal(List<Continent> world)
    {
        int nLands = 0;

        foreach(Continent continent in world)
        {
            nLands += continent.getLands().Count;
        }

        nTerritoryGoal = nLands * PERCENTAGE / 100;
    }

    public override bool isAccomplished(List<Player> players, Player player, List<Continent> world)
    {
        if (player.getTerritoryOwned().Count == nTerritoryGoal)
            return true;

        return false;
    }
}
