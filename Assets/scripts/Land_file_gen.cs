using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Land_file_gen : MonoBehaviour
{
    
    private InputField input;
    private Text testo;
    private string fileName;
    private int count = 1; // inizializza il file
    private int num_Continenti;
    private string nome_Continente;
    private string codice_Continente;
    private int num_stati;
    private string nome_Stato;
    private string codice_Vicino;
    private int max_vicini = 10; // massimo 10 vicini per stato
    private int conta_contineti = 0;
    private int conta_stati = 0;
    private int conta_vicini = 0;


    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        testo = GameObject.Find("Text").GetComponent<Text>();

    }
    public void Start()
    {
        testo.text = "Inserisci il nome della nuova mappa";
    }

    public void GetInput(string stringa)
    { 
        input.text = "";
        if(count < 9)
        count = Inizializzatore(count, stringa); 
        
    }

    public void CreateFile(string stringa)
    {
        fileName = stringa;
        StreamWriter file = new StreamWriter(fileName + ".txt"); //creao il nuovo file
        file.Close();
    }
    public void NumeroContinenti() // salvo il numero di continenti
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(num_Continenti);
            file.Close();
        }
    }
    public void SalvaContinente() // salvo le info del continente
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file 
        {
            file.WriteLine(nome_Continente);
            file.WriteLine(codice_Continente);
            file.WriteLine(num_stati);
            file.Close();
        }
    }
    public void SalvaStato() // salvo le info dello stato
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(nome_Stato);
            file.Close();
        }
    }
    public void SalvaVicino() // salvo un vicino
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(codice_Vicino);
            file.Close();

        }
    }

    public int Inizializzatore(int count, string stringa)
    {
        int a = conta_contineti + 1;
        switch (count)
        {
            case 1:  // creo il nuovo file;
                {

                    CreateFile(stringa); // chiamo la crea file
                    testo.text = "Inserisci il numero di contienti";
                    count = 2;
                    break;
                }
            case 2: // setto il numero di continenti
                {

                    num_Continenti = int.Parse(stringa);
                    NumeroContinenti();
                    testo.text = "inserisci il nome del  continente " + a;
                    count = 3;
                    break;
                }
            case 3: // salvo il nome continete
                {
                    conta_contineti++;
                    nome_Continente = stringa;
                    testo.text = "inserisci il codice del continete " + nome_Continente;
                    count = 4;
                    break;
                }
            case 4: // inserisco il codice del continente
                {

                    codice_Continente = stringa;
                    testo.text = "inserisci il numero di stati del continente " + nome_Continente;
                    count = 5;
                    break;
                }
            case 5: // inserisci il numero di stati 
                {
                    int c = conta_stati + 1;
                    num_stati = int.Parse(stringa);
                    SalvaContinente();
                    testo.text = "inserisci il nome dello stato numero "+c ;
                    count = 6;
                    break;
                }

            case 6: // nome stato
                {
                    conta_stati++;
                    nome_Stato = stringa;
                    SalvaStato();
                    testo.text = "inserisci un vicino dello stato " + nome_Stato;
                    count = 7;
                    break;
                }
                
            case 7:  // vicini dello stato ( max 10 )
                {
                    conta_vicini++;
                    codice_Vicino = stringa;
                    SalvaVicino();
                    testo.text = "inserisci un vicino dello stato " + nome_Stato;
                    if (conta_vicini < max_vicini) // ho inserito tutti i valori dei vicini ?                                           
                        count = 7; // inserisci nuovo vicino
                    else if (conta_stati < num_stati) // controlla se ho finito di inizializzare gli stati di un continente
                    {
                        conta_vicini = 0;
                        count = 6; // vicini finiti
                        testo.text = "inserisci il nome dello stato numero "+num_stati;
                    }
                    else if (conta_contineti < num_Continenti)  // controlla se ho finito di inizializzare un continente
                    {
                        conta_stati = 0;
                        count = 3;   // 
                        testo.text = "inserisci il nome del continente " + a;

                    }
                    else if( conta_contineti<num_Continenti)
                    {
                        count = 8;
                    }
                    break;
                }
            case 8:  // caso finale
                {
                    testo.text = "inizializzazione completata";
                    input.DeactivateInputField();
                    break;
                }
                

        }
        return count;  // fine metodo inizializzatore
    }





}






    

