using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    public abstract bool isAccomplished(List<RiskPlayer> players, RiskPlayer player, List<Continent> world);

    public abstract void fixGoal(List<RiskPlayer> players, RiskPlayer player, List<Continent> world);

    public abstract Goal getClone();

    public abstract string getText();
}
