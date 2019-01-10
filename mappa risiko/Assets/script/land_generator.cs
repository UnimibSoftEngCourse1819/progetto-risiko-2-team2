using System;
using System.IO;
using UnityEngine;

public class land_generator : MonoBehaviour
{
    private int maxconnections = 10;
    private int n_stati;
    private int n_continenti;
    private String nome_c;
    private String codice_c;
    private string nome_s;
    private string codice_s;
  

    void Start()
    {
        Console.Write("inserrisci il nome della nuova mappa : ");
        String a = Console.ReadLine();
        //createnewmapfile(a);

     /*   // read the file
        string line = "";
        using (StreamReader sr = new StreamReader(a + ".txt"))
        {
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }*/
    }

    void createnewmapfile(String a)
    {
        StreamWriter file = new StreamWriter(a + ".txt"); //  usare la cartella degli asset 

        Console.Write("Inserisci il numero degli continenti : ");
        n_continenti = Console.Read();  // aggiungere controllo input

        for (int i = 0; i < n_continenti; i++) // ciclo per le info dei contineti
        {
            //leggo nome continente
            Console.Write("Inserisci il nome del continente : ");
            nome_c = Console.ReadLine();  // aggiungere controllo input
            file.WriteLine(nome_c); // per scirevere nel file

            //leggo codice continente
            Console.Write("Inserisci il codice del continente "+nome_c+": ");
            codice_c= Console.ReadLine();  // aggiungere controllo input
            file.WriteLine(codice_c); // per scirevere nel file

            //leggo numero di stati del continente
            Console.Write("Inserisci il numero degli stati del continente " + nome_c + " : ");
            n_stati= Console.Read();  // aggiungere controllo input

            for (int j = 0; j < n_stati; j++)  // ciclo per le info dei singoli stati
            {
                //leggo nome stato
                Console.Write("Inserisci il nome dello stato numero "+j+" : ");
                nome_s = Console.ReadLine();  // aggiungere controllo input
                file.WriteLine(nome_s); // per scirevere nel file

                for (int k=0;k<maxconnections;k++)  // ciclo per sapere gli stati connessi 
                {
                    //leggo stati connessi ( bisogna inserire il codice )
                    Console.Write("Inserisci il codice dello stati vicino a "+nome_s+" oppure 0: ");
                    codice_s= Console.ReadLine();  // aggiungere controllo input
                    file.WriteLine(codice_s); // per scirevere nel file
                }
            }


            file.Close();

        }
    }
}

