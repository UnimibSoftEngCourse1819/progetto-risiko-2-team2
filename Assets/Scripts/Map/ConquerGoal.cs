using Model.Game;
using Model.Game.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Goals
{
    public class ConquerGoal : Goal
    {
        private readonly List<Continent> continentsToConquer;
        private const string TEXT = "Conquer the following continents: ";

        public ConquerGoal()
        {
            continentsToConquer = new List<Continent>();
        }

        public override void fixGoal(List<RiskPlayer> players, RiskPlayer player, List<Continent> world)
        {
            int index1 = Random.Range(0, world.Count - 1);
            int index2 = Random.Range(0, world.Count - 1);

            while (index1 == index2 || areSameGoal(players, player.getName(), world[index1], world[index2]))
            {
                index1 = Random.Range(0, world.Count - 1);
                index2 = Random.Range(0, world.Count - 1);
            }

            continentsToConquer.Add(world[index1]);
            continentsToConquer.Add(world[index2]);
        }

        public override bool isAccomplished(List<RiskPlayer> players, RiskPlayer player, List<Continent> world)
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
            return new ConquerGoal();
        }

        public override string getText()
        {
            string txt = TEXT + " ";
            bool first = true;

            foreach (Continent continent in continentsToConquer)
            {
                txt += continent.getName();
                if (first)
                {
                    txt += " and ";
                    first = false;
                }
            }

            return txt;
        }

        private bool areEqualContinents(Continent continent1, Continent continent2)
        {
            return (continentsToConquer.Contains(continent1) && continentsToConquer.Contains(continent2));
        }

        private bool areSameGoal(List<RiskPlayer> players, string player, Continent continent1, Continent continent2)
        {
            ConquerGoal goal = null;
            bool result = false;

            foreach (RiskPlayer p in players)
            {
                if (!p.getName().Equals(player) && p.getGoal() != null && p.getGoal().GetType() == typeof(ConquerGoal))
                {
                    goal = (ConquerGoal)p.getGoal();
                    if (goal.areEqualContinents(continent1, continent2) && !result)
                        result = true;
                }
            }

            return result;
        }
    }
}

