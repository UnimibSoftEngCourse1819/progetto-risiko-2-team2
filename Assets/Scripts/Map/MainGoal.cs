using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGoal : Goal
{
    private int nTerritoryGoal;
    private const int PERCENTAGE = 58;
    private const string TEXT = "Conquer this number of territories: ";

    public MainGoal()
    {
        nTerritoryGoal = 0;
    }

    private MainGoal(MainGoal goal)
    {
        nTerritoryGoal = goal.getNTerritoryGoal();
    }

    public override void fixGoal(List<Player> players, Player player, List<Continent> world)
    {
        int nLands = 0;

        foreach (Continent continent in world)
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

    public int getNTerritoryGoal()
    {
        return nTerritoryGoal;
    }

    public override Goal getClone()
    {
        return new MainGoal(this);
    }

    public override string getText()
    {
        return TEXT + nTerritoryGoal;
    }
}
