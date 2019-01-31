using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyGoal : Goal
{
    private string target;

    public DestroyEnemyGoal()
    {
        target = null;
    }

    private DestroyEnemyGoal(DestroyEnemyGoal goal)
    {
        target = goal.getTarget();
    }

    public override void fixGoal(List<Player> players, Player player, List<Continent> world)
    {
        int index = Random.Range(0, players.Count - 1);
        
        while(player.getName().Equals(players[index]))
            index = Random.Range(0, players.Count - 1);

        target = players[index].getName();
    }

    public override bool isAccomplished(List<Player> players, Player player, List<Continent> world)
    {
        foreach(Player p in players)
        {
            if(player.getColor().Equals(target))
            {
                if (player.getTotalLand() == 0)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }

    public string getTarget()
    {
        return target;
    }

    public override Goal getClone()
    {
        return new DestroyEnemyGoal(this);
    }
}
