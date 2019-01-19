using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Land_loader : MonoBehaviour
{

    /*
        dividere il codice in 2 classi:
        1)File_Loader_Controller simile a File_Creator_Controller
            ma fa l'operazione inversa ovvero ha un metodo che prende 
            il nome del file e da passa in output 
        2) Land_loader può darsi che non rimanga nulla però
            se ho capito bene ora con i dati della mappa dovrebbe
            "disegnare" la mappa usando appunto di dati ottenuti

    */
    public Canvas canvas;
    private GameObject Continente;
    private Continent_Script c_script;
    private State_script s_script;
    private StreamReader reader;
    private State_load state = new State_load1();
    private int N_continenti;
    private string nome_continente;
    private string codice_continente;
    private int value_continente;
    private int N_stati;
    private string nome_stato;
    private int conta_continenti=0;
    private int conta_stati = 0;
    private int conta_vicini = 0;


    public void setState(State_load nextState)
    {
        state = nextState;
    }

    public void setNumContinent(int value)
    {
        N_continenti = value;
    }

    public void setNumState(int value)
    {
        N_stati = value;
    } 

    public void setNameContinent(string name)
    {
        nome_continente = name;
    }

    public void setNameState(string name)
    {
        nome_stato = name;
    }

    public void setCountState(int value)
    {
        conta_stati = value;
    }

    public void setCountNeighbor(int value)
    {
        conta_vicini = value;
    }

     public void setValueContinent(int value)
    {
        value_continente = value;
    }

    public void setCodeContinent(string code)
    {
        codice_continente = code;
    }


    public State_script getStateScript()
    {
        return s_script;
    }

    public int getCountNeighbor()
    {
        return conta_vicini;
    }

    public int getCountContinent()
    {
        return conta_continenti;
    }

    public int getNumState()
    {
        return N_stati;
    }

    public int getNumContinent()
    {
        return N_continenti;
    } 

    public void incrementCountContinent()
    {
        conta_continenti++;
    }

    public void incrementCountState()
    {
        conta_stati++;
    }

    public void incrementCountNeighbor()
    {
        conta_vicini++;
    }

    private void Awake()
    {
        Continente = GameObject.Find("Continente");
        c_script = GameObject.Find("Continente").GetComponent<Continent_Script>();       
        // Debug.Log(canvas.ToString());
    }

    private void Start() //canvas.ToString()+".txt"
    {
        if (File.Exists("Mappa_0.txt")) // file trovato ?
        {
            using(reader= new StreamReader("Mappa_0.txt"))
            {
                Debug.Log("file trovato");
                while (state != null)
                {
                    state.Handle(this, reader.ReadLine());
                }
            }
            reader.Close(); // chiudo il file
        }
        else  // file non trovato
        {
            Debug.Log("file non trovato");
        }

    }



    /*private int Dispatcher(int c, string a) // dato count e una stringa sceglie cosa fare
    {
        switch (c)
        {
            case 1: //leggo il numero di continenti
                {
                    N_continenti = int.Parse(a);
                    c = 2;
                    break;
                }
            case 2: // leggo il nome del continente
                {
                    nome_continente = a;
                    conta_continenti++;
                    c = 3;
                    break;
                }
            case 3: // leggo il codice del continente
                {
                    codice_continente = a;
                    c = 4;
                    break;
                }
            case 4: // leggo il valore del continente
                {
                    value_continente = int.Parse(a);
                    c = 5;
                    break;
                }
            case 5: // leggo il numero di stati del continente
                {
                    N_stati = int.Parse(a);
                    Istanza_continente();
                    c = 6;
                    break;
                }
            case 6: // leggo nome dello stato
                {
                    nome_stato = a;
                    conta_stati++;
                    Istanzia_stato();
                    c = 7;
                    break;
                }
            case 7: // leggo un vicino
                {
                    conta_vicini++;
                    s_script.Inser_new_neighbour(a);

                    if (conta_vicini < 10) // devo ancora leggere dei vicini ?                    
                        c = 7;
                    else if (conta_stati < N_stati) // devo ancora leggere degli stati ?
                    {
                        conta_vicini = 0;
                        c = 6;
                    }
                    else if (conta_continenti < N_continenti) // devo ancora leggere continenti?
                    {
                        conta_stati = 0;
                        conta_vicini = 0;
                        c = 2;
                    }
                    else
                        c = 8; // fine
                    break;
                }
        }

        return c;
    }*/


    public void Istanza_continente()
    {
        Instantiate(Continente);
        Continente.name = nome_continente;
        c_script.Inizializza(codice_continente,value_continente,N_stati);
        Debug.Log("continente " + nome_continente + " istanziato");
    }

    public void Istanzia_stato() // controllo se esiste lo stato
    {
        string x = codice_continente + "_" + conta_stati;
        s_script = GameObject.Find(x).GetComponent<State_script>();
       
              s_script.Inizializza(nome_stato, nome_continente, x);
            Debug.Log("stato " + nome_stato + " istanziato");
       
          //  Debug.Log("sprite dello stato non trovato");
    }


}
