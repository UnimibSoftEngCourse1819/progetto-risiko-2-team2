using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// questa classe è la generale per i territori 



public class OldLand : MonoBehaviour
{
    private const int MAX_BORDERS = 10;

    private int land_ID;
    private string land_name;
    private int[] near_lands;
    private int player_ID;
    private string player_colour; // probabilemnte non è necessario segnarsi il colore qui
    private int n_tank;
    public GameObject state;


    // si può usare sia il metodo qui sotto che i setter per inizializzare il territorio

    public OldLand(int Land_ID, string Land_name, int[] Near_lands, int Player_ID, string Player_colour, int N_tank)
    {
        land_ID = Land_ID;
        land_name = Land_name;
        this.near_lands = Near_lands;
        player_ID = Player_ID;
        player_colour = Player_colour;
        n_tank = N_tank;
    }

    void Start()  // in base al metodo che istanzia il land gli si passano cose diverse 
    {


    }


    void Update()  // si controllano chi è il player che possiede il territorio, il suo colore e il numero di armate
    {
        // quando si preme sul territorio si deve zoomaare leggermente
        
    }

        public int Is_land_near(int Land2_ID)
        {
            for (int i = 0; i < MAX_BORDERS; i++) // ciclo per il numero di territori
                if (Land2_ID == near_lands[i]) // l'ID del territorio passato è nell'array near_lands
                    return 1;

            return -1;

        }

        // metodi get
        public int get_Ntanks()
        {
            return n_tank;
        }
        public string get_land_name()
        {
            return land_name;
        }
        public int get_land_ID()
        {
            return land_ID;
        }
        public int get_player_ID()
        {
            return player_ID;
        }
        public string get_player_colour()
        {
            return player_colour;
        }

        // metodi set
        public void set_player_ID(int player_id)
        {
            this.player_ID = player_id;
        }
        public void gset_player_colour(string player_c)
        {
            this.player_colour = player_c;
        }
        public void set_land_id(int land_id)
        {
            this.land_ID = land_id;
        }
        public void set_land_name(string land_name)
        {
            this.land_name = land_name;
        }

        public void set_near_lands(int[] near_lands)
        {
            for (int i = 0; i < MAX_BORDERS; i++) // per il numero max di territori affiancati
                this.near_lands[i] = near_lands[i];
        }



    }

