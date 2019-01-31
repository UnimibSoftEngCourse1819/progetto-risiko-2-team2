using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyGoal : Goal
{
    private readonly string armyColor;

    public DestroyEnemyGoal(string color)
    {
        armyColor = color;
    }

    public override bool isAccomplished(List<Player> players, Player player, List<Continent> world)
    {
        foreach(Player p in players)
        {
            if(player.getColor().Equals(armyColor))
            {
                if (player.getTotalLand() == 0)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }
}
