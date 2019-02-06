using Model.Game;
using Model.Game.Map;
using System.Collections.Generic;

namespace Model.Goals
{
    public abstract class Goal
    {
        public abstract bool isAccomplished(List<RiskPlayer> players, RiskPlayer player, List<Continent> world);

        public abstract void fixGoal(List<RiskPlayer> players, RiskPlayer player, List<Continent> world);

        public abstract Goal getClone();

        public abstract string getText();
    }
}
