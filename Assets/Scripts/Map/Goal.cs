using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    public abstract bool isAccomplished(List<Player> players, Player player, List<Continent> world);
}
