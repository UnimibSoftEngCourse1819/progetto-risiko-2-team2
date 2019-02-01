using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goals
{
    public class ConquerGoal : Goal
    {
        private readonly List<Continent> continentsToConquer;

        public ConquerGoal()
        {
            continentsToConquer = new List<Continent>();
        }

        private ConquerGoal(ConquerGoal goal)
        {
            continentsToConquer = goal.getContinentsToConquer();
        }

        public override void fixGoal(List<Player> players, Player player, List<Continent> world)
        {
            int index = Random.Range(0, world.Count - 1);

            Continent continent = world[index];
            continentsToConquer.Add(continent);

            while (continentsToConquer.Contains(world[index]))
                index = Random.Range(0, world.Count - 1);

            continent = world[index];
            continentsToConquer.Add(continent);
        }

        public override bool isAccomplished(List<Player> players, Player player, List<Continent> world)
        {
            int nLands = 0;
            int nLandsOwned;

            foreach (Continent continent in continentsToConquer)
            {
                nLands = continent.getLands().Count;
                nLandsOwned = 0;

                foreach (Land land in continent.getLands())
                {
                    if (player.hasLand(land.getName()))
                        nLandsOwned += 1;
                }

                if (nLands != nLandsOwned)
                    return false;
            }

            return true;
        }

        public List<Continent> getContinentsToConquer()
        {
            return continentsToConquer;
        }

        public override Goal getClone()
        {
            return new ConquerGoal(this);
        }
    }
}
