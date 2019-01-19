using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class File_Creator_Controller : MonoBehaviour
{
    /*
        Deve fare la stessa cosa di Land_file_gen con queste caratteristiche:
        -diversamente da land_file_gen non ha nessuna variabile per salvare i dati da scrivere
            come num_Continenti, nome_Continente ... questi valori gli vengono passati come parametri
            perchè ci pensa il model a ricevere i dati
        -usate le classi Continent e Land : li abbiamo creati quindi tanto vale usarli altrimenti
            rischiamo di scrivere più codice
        -Applicare il design Pattern Singleton siccome ne serve solo 1
    */

    public bool CreateFileMap(string name,  List<Continent> continents)
    {
        bool result = false; //true è stato correttamente letto il file, false c'è stato un errore (ad esempio non è stato trovato il file)
        /*
            crea il file, ovviamente i dati li estrae da continents usando i get,
            se manca qualche get implementatelo sulla classe Continent cosi 
            può essere riutilizzata altrove
        */
        return result;
    }

    /*
        questi metodi di Land_file_gen possono essere utili come metodi ausiliari 
        ovviamente devono essere riaddattati


        private void GetInput(string stringa)// this is the Request method of the Design pattern State
    {
        string message = state.Handle(this, stringa);
        testo.text = message;         
    } 
        
        private void NumeroContinenti() // salvo il numero di continenti
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(num_Continenti);
            file.Close();
        }
    }
    private void SalvaContinente() // salvo le info del continente
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file 
        {
            file.WriteLine(nome_Continente);
            file.WriteLine(codice_Continente);
            file.WriteLine(value_Continente);
            file.WriteLine(num_stati);
            file.Close();
        }
    }
    private void SalvaStato() // salvo le info dello stato
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(nome_Stato);
            file.Close();
        }
    }
    private void SalvaVicino() // salvo un vicino
    {
        using (StreamWriter file = File.AppendText(fileName + ".txt"))  // apro il file
        {
            file.WriteLine(codice_Vicino);
            file.Close();

        }
    }




    */


}






    

