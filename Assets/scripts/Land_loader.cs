using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Land_loader : MonoBehaviour
{
    public Canvas canvas;
    private GameObject Continente;
    private Continent_Script c_script;
    private State_script s_script;
    private StreamReader reader;
    private int count = 1;
    private int N_continenti;
    private string nome_continente;
    private string codice_continente;
    private int value_continente;
    private int N_stati;
    private string nome_stato;
    private int conta_continenti=0;
    private int conta_stati = 0;
    private int conta_vicini = 0;


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
                while (count < 8)
                {
                    count=Dispatcher(count,reader.ReadLine());
                }
            }
            reader.Close(); // chiudo il file
        }
        else  // file non trovato
        {
            Debug.Log("file non trovato");
        }

    }

    private int Dispatcher(int c, string a) // dato count e una stringa sceglie cosa fare
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
    }
    private void Istanza_continente()
    {
        Instantiate(Continente);
        Continente.name = nome_continente;
        c_script.Inizializza(codice_continente,value_continente,N_stati);
        Debug.Log("continente " + nome_continente + " istanziato");
    }

    private void Istanzia_stato() // controllo se esiste lo stato
    {
        string x = codice_continente + "_" + conta_stati;
        s_script = GameObject.Find(x).GetComponent<State_script>();
        s_script.Inizializza(nome_stato, nome_continente, x);
        Debug.Log("stato " + nome_stato + " istanziato");
    }


}
