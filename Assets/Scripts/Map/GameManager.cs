using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    private List<Player> players;
    private List<Continent> World;
    private Player currentPlayer;

    public void attack(Land attacker, Land defender, int nTankAttacker)
    {
        /*
        simula un attacco con facendo tutti i controlli (attacker deve essere il currentplayer)
        se defender rimane con 0 di tank far in modo che la land passi al player
        */
    }

    public void move(Land startLand, Land endLand, int nTank)
    {
        /*
        simula lo spostamento di truppe da start a end (controllare che siano di currentplayer)
        */ 
    }

    public void addTroup(Land land, int nTank)
    {
        /*
        aggiunge le truppe a land controllare che siano di currentplayer)
        */
    }

    public void passTurn()
    {
        //cambia currentPlayer
    }
}