using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// questa classe è la generale per i territori 



public class land : MonoBehaviour
{
    private int MAX_BORDERS = 10;

    private int Land_ID;
    private string land_name;
    private int Continent_ID;
    private int[] near_lands;
    private int Player_ID;
    private string Player_colour; // probabilemnte non è necessario segnarsi il colore qui
    private int N_tank;
    public GameObject state;


    // si può usare sia il metodo qui sotto che i setter per inizializzare il territorio

    public land(int land_ID, string land_name, int continent_ID, int[] near_lands, int player_ID, string player_colour, int n_tank)
    {
        Land_ID = land_ID;
        this.land_name = land_name;
        Continent_ID = continent_ID;
        this.near_lands = near_lands;
        Player_ID = player_ID;
        Player_colour = player_colour;
        N_tank = n_tank;
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
            return N_tank;
        }
        public string get_land_name()
        {
            return land_name;
        }
        public int get_land_ID()
        {
            return Land_ID;
        }
        public int get_player_ID()
        {
            return Player_ID;
        }
        public string get_player_colour()
        {
            return Player_colour;
        }
        public int get_continent_id()
        {
            return Continent_ID;
        }

        // metodi set
        public void set_player_ID(int player_id)
        {
            Player_ID = player_id;
        }
        public void gset_player_colour(string player_c)
        {
            Player_colour = player_c;
        }
        public void set_land_id(int land_id)
        {
            Land_ID = land_id;
        }
        public void set_land_name(string land_name)
        {
            this.land_name = land_name;
        }

        public void set_continetID(int continent_id)
        {
            Continent_ID = continent_id;
        }

        public void set_near_lands(int[] near_lands)
        {
            for (int i = 0; i < MAX_BORDERS; i++) // per il numero max di territori affiancati
                this.near_lands[i] = near_lands[i];
        }



    }

