using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer
{
    private readonly List<LandCard> landCards;
    private readonly List<Goal> goals;

    public Dealer()
    {
        landCards = new List<LandCard>();
        goals = new List<Goal>();
        goals.Add(new MainGoal());
        goals.Add(new ConquerGoal());
        goals.Add(new DestroyEnemyGoal());
    }

    public Dealer(List<Land> lands) : this()
    {
        int counter = 0;

        foreach (Land land in lands)
        {
            landCards.Add(new LandCard(land, counter));

            if (counter == 2)
                counter = 0;
            else
                counter++;
        }

        shuffleCards();
    }

    public void assignGoal(List<Player> players, Player player, List<Continent> world)
    {
        int index = Random.Range(0, goals.Count - 1);

        Goal goal = goals[index].getClone();

        goal.fixGoal(players, player, world);
        player.setGoal(goal);
    }

    public void shuffleCards()
    {
        int index = 0;
        LandCard temp = null;

        for(int i = 0; i < landCards.Count - 1; i++)
        {
            index = Random.Range(i, landCards.Count);
            temp = landCards[index];
            landCards[index] = landCards[i];
            landCards[i] = temp;
        }
    }

    public void drawCards(List<Player> players)
    {
        int index = 0;

        for (int i = 0; i < landCards.Count; i++)
        {
            players[index].addLand(landCards[i].getLand());

            if (index == players.Count - 1)
                index = 0;
            else
                index++;
        }

        landCards.Add(new LandCard());
        landCards.Add(new LandCard());

        shuffleCards();
    }

    // Rimette nel mazzo le carte utilizzate dal player
    public void addCardsToDeck(List<LandCard> cards)
    {
        foreach (LandCard card in cards)
            landCards.Add(card);
    }

    public void drawCard(Player player)
    {
        player.addLandCard(landCards[0]);
        landCards.RemoveAt(0);
    }
}
