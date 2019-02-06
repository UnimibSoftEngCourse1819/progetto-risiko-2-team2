using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyGoal : Goal
{
    private string target;
    private const string TEXT = "Eliminate the player: ";

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
        
        while(player.getName().Equals(players[index].getName()) || isSameTarget(players, players[index].getName()))
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
        return new DestroyEnemyGoal();
    }

    public override string getText()
    {
        return TEXT + target;
    }

    private bool isSameTarget(List<Player> players, string player)
    {
        DestroyEnemyGoal goal = null;
        bool result = false;

        foreach(Player p in players)
        {
            if(!p.getName().Equals(player))
            {
                if(p.getGoal() != null && p.getGoal().GetType() == typeof(DestroyEnemyGoal))
                {
                    goal = (DestroyEnemyGoal)p.getGoal();
                    if (goal.getTarget().Equals(player))
                        result = true;
                }
            }
        }

        return result;
    }
}
