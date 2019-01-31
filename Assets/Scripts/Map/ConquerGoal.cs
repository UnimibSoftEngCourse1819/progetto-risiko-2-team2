using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conquer : Goal
{
    private readonly List<Continent> continentsToConquer;

    public Conquer(List<Continent> continents)
    {
        continentsToConquer = continents;
    }

    public override bool isAccomplished(List<Player> players, Player player, List<Continent> world)
    {
        int nLands = 0;
        int nLandsOwned;

        foreach(Continent continent in continentsToConquer)
        {
            nLands = continent.getLands().Count;
            nLandsOwned = 0;

            foreach(Land land in continent.getLands())
            {
                if (player.hasLand(land.getName()))
                    nLandsOwned += 1;
            }

            if (nLands != nLandsOwned)
                return false;
        }

        return true;
    }
}
