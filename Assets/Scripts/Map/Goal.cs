using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    public abstract bool isAccomplished(List<Player> players, Player player, List<Continent> world);

    public abstract void fixGoal(List<Player> players, Player player, List<Continent> world);

    public abstract Goal getClone();
}
