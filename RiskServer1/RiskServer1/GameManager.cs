using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
     class GameManager
    {
        private static Dictionary<int, string> playerList_Name = new Dictionary<int, string>();
        private static Dictionary<int, string> playerList_Color = new Dictionary<int, string>();
        private static int state=0; // indica se sono pronto a leggere o meno 
        private static int game = 0;// indica se siamo in gioco o meno

        public static void AddPlayer(int ConnectionID, string nome)
        {
            playerList_Name.Add(ConnectionID, nome);
            Console.WriteLine("Il Game manager ti ha aggiunto");
            int c = playerList_Name.Count;
            switch (c) // assegno i colori in base al primo che arriva
            {
                case 1:
                    {
                        playerList_Color.Add(ConnectionID, "rosso");// perche ross è figo
                        break;
                     }
               case 2:
                    {
                        playerList_Color.Add(ConnectionID, "nero");//
                        break;
                    }   
                case 3:
                    {
                        playerList_Color.Add(ConnectionID, "blu");// 
                        break;
                    }
                case 4:
                    {
                        playerList_Color.Add(ConnectionID, "viola");// 
                        break;
                    }
                case 5:
                    {
                        playerList_Color.Add(ConnectionID, "verde");// 
                        break;
                    }
                case 6:
                    {
                        playerList_Color.Add(ConnectionID, "giaalo");// perchè nessuno lo vuole
                        break;
                    } 
            }

        }
        public static void SetGame(int c)
        {
            game = c;
        }
        public static int GetGame()
        {
            return game;
        }
        public static void SetState(int c)
        {
            state = c;
        }
        public static int GetState()
        {
            return state;
        }
        

    }
}
