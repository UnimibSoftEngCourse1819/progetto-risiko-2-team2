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
        private static int turno=0; // numero di turni giocati
        private static int Nplayer = 0;
        private static int statoTurno = 0; // indica in che punto del turno si è
        private static string[] stati=new string[2];
        private static string[] risultatoAttacco=new  string[2];

        public static void AddPlayer(int ConnectionID, string nome)
        {
            Nplayer++;
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

        public static void StartGame()
        {
            SetGame(1);
            foreach (KeyValuePair<int, string> keyValue in playerList_Color)
            {
                int c = keyValue.Key;
                string s = keyValue.Value;
                if (s=="rosso")
                {
                    DataSender.SendTurn(keyValue.Key); // mando al player la notifica che sia il suo turno
                    Console.WriteLine("turno del player "+keyValue.Key);
                }

            }
            turno = 1;
         }
        public static void PassaTurno()
        {
            turno++;
            string s="";
            int control = turno % Nplayer; // 
            switch (control)
            {
                case 0:
                    {
                        turno++;
                        s = "nero";
                        break;
                    }
                case 1:
                    {
                        if (Nplayer > 2)
                        {
                            turno++;
                            s = "blu";
                        }
                        else
                        {
                            turno++;
                            s = "rosso";
                        }
                        break;
                    }
                case 2:
                    {
                        if (Nplayer > 3)
                        {
                            turno++;
                            s = "viola";
                        }
                        else
                        {
                            turno++;
                            s = "rosso";
                        }
                        break;
                    }
                case 3:
                    {
                        if (Nplayer > 4)
                        {
                            turno++;
                            s = "verde";
                        }
                        else
                        {
                            turno++;
                            s = "rosso";
                        }
                        break;
                    }
                case 4:
                    {
                        if (Nplayer > 5)
                        {
                            turno++;
                            s = "giallo";
                        }
                        else
                        {
                            turno++;
                            s = "rosso";
                        }
                        break;
                    }
                case 5:
                    {
                        turno++;
                        s = "rosso";
                        break;
                    }
             }
            foreach (KeyValuePair<int, string> keyValue in playerList_Color)
            {
                int c = keyValue.Key;
                string v = keyValue.Value;
                if (s == v)
                {
                    DataSender.SendTurn(keyValue.Key); // mando al player la notifica che sia il suo turno
                    Console.WriteLine("turno del player " + keyValue.Key);
                }

            }
        }
        public static void GestioneAttacco(string s,int PlayerID)
        {
           statoTurno++;
            switch (statoTurno) // inizializzo le 4 variabili e poi le invio 
            {
                case 1:
                    {
                        stati[0] = s;
                        break;
                    }

                case 2:
                    {
                        stati[1] = s;
                        break;
                    }

                case 3:
                    {
                        risultatoAttacco[0] = s;
                        break;
                    }

                case 4:
                    {
                        risultatoAttacco[1] = s;
                        statoTurno = 0; // mi preparo per l'attacco successivo
                        state = 0;// riposto lo stato generale a 0
                        foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                        {                
                            DataSender.SendAttacco(keyValue.Key,playerList_Name[PlayerID],stati,risultatoAttacco );
                        }
                        break;
                    }
            }
        }
        public static void GestioneSpostamento(string s,int PlayerID) // simile a gestione attacco 
        {
            statoTurno++;
            switch (statoTurno) // inizializzo le 4 variabili e poi le invio 
            {
                case 1:
                    {
                        stati[0] = s;
                        break;
                    }

                case 2:
                    {
                        stati[1] = s;
                        break;
                    }

                case 3:
                    {
                        risultatoAttacco[0] = s;
                        statoTurno = 0; // mi preparo per l'attacco successivo
                        state = 0; // riposrto lo stato generale a 0
                        foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                        {

                            DataSender.SendSpostamento(keyValue.Key,playerList_Name[PlayerID] ,stati, risultatoAttacco);
                        }
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
        public static int GetStatoTurno()
        {
            return statoTurno;
        }  
        public static void SetStatoTurno(int a)
        {
            statoTurno = a;
        }

    }
}
