using System.Collections.Generic;
using Model.Goals;
using Model.Game.Map;

namespace Model.Game
{
    public class RiskPlayer
    {

        private readonly string nickname;
        private readonly List<Land> territoryOwned;
        private readonly List<LandCard> landCards;
        private readonly string color;
        private int nTanks;
        private Goal goal;
        private bool conqueredLand;

        public RiskPlayer()
        {
            nickname = null;
            territoryOwned = new List<Land>();
            color = null;
            nTanks = 0;
            landCards = new List<LandCard>();
            conqueredLand = false;
        }

        public RiskPlayer(string username, string color) : this(username, new List<Land>(), color, 0, null)
        {
        }

        public RiskPlayer(string username)
        {
            nickname = username;
            territoryOwned = new List<Land>();
            color = null;
            nTanks = 0;
            landCards = new List<LandCard>();
            conqueredLand = false;
        }

        public RiskPlayer(string nickname, List<Land> territory, string color, int nTanks, Goal goal)//need to add Goal
        {
            this.nickname = nickname;
            territoryOwned = territory;
            this.color = color;
            this.nTanks = nTanks;
            this.goal = goal;
            this.landCards = new List<LandCard>();
            conqueredLand = false;
        }

        public RiskPlayer(string nickname, int nTanks) : this(nickname, new List<Land>(), null, nTanks, null)//need to add Goal
        {
        }

        public void setConqueredLand(bool value)
        {
            conqueredLand = value;
        }

        public bool getConqueredLand()
        {
            return conqueredLand;
        }

        public void setGoal(Goal goal)
        {
            this.goal = goal;
        }

        public Goal getGoal()
        {
            return goal;
        }

        public void addCard(LandCard card)
        {
            landCards.Add(card);
        }

        public void addLand(Land conquered)
        {
            territoryOwned.Add(conquered);
        }

        public void removeLand(Land land)
        {
            territoryOwned.Remove(land);
        }

        public bool hasLand(string name)
        {
            bool result = false;

            foreach (Land territory in territoryOwned)
            {
                if (name.Equals(territory.getName()))
                {
                    result = true;
                }
            }
            return result;
        }

        public int getTotalLand()
        {
            return territoryOwned.Count;
        }

        public bool hasLost()
        {
            if (territoryOwned.Count == 0)
                return true;

            return false;
        }

        public List<Land> getTerritoryOwned()
        {
            return territoryOwned;
        }

        public string getColor()
        {
            return color;
        }

        public string getName()
        {
            return nickname;
        }

        public void addTanks(int nTanks)
        {
            this.nTanks += nTanks;
        }

        public void removeTanks(int nTanks)
        {
            this.nTanks -= nTanks;
        }

        public void setNTanks(int nTanks)
        {
            this.nTanks = nTanks;
        }

        public int getNTanks()
        {
            return nTanks;
        }

        public bool hasContinent(Continent continent)
        {
            List<Land> lands = continent.getLands();

            foreach (Land land in lands)
            {
                if (!hasLand(land.getName()))
                    return false;
            }

            return true;
        }

        public List<LandCard> getLandCards()
        {
            return landCards;
        }

        public void addLandCard(LandCard card)
        {
            landCards.Add(card);
        }

        public void removeLandCard(List<LandCard> cards)
        {
            foreach (LandCard card in cards)
                landCards.Remove(card);
        }

        public LandCard getCard(int index)
        {
            return landCards[index];
        }

        public List<string> getListCard()
        {
            List<string> nameCards = new List<string>();
            foreach (LandCard card in landCards)
            {
                if (card.isJolly())
                    nameCards.Add("Jolly");
                else
                    nameCards.Add("" + card.getSymbol() + ": " + card.getLand().getName());
            }
            return nameCards;
        }
    }
}