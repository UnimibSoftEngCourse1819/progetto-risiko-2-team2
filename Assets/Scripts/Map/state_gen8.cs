using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen8 : State_gen
{
    private string MESSAGE_ON_FIELD;
    public override string Handle(Land_file_gen generator, string message)
    {
        generator.IncrementCountVicino();
        generator.setCodeVicino(message);
        generator.SalvaVicino();
        if(generator.getCountVicino() < generator.getMaxVicini())// ho inserito tutti i valori dei vicini ?
        {
            generator.setState(new State_gen8());// inserisci nuovo vicino
            MESSAGE_ON_FIELD = "inserisci un vicino dello stato " + generator.getNameState();
        }
        else if(generator.getCountState() < generator.getNumStati())
        {// controlla se ho finito di inizializzare gli stati di un continente
            generator.setCountVicino(0);
            MESSAGE_ON_FIELD = "inserisci il nome dello stato numero " + generator.getNumStati();
        }
        else if(generator.getCountContinent() < generator.getNumContinent()) // controlla se ho finito di inizializzare un continente
        {
            generator.setCountState(0);
            generator.setState(new State_gen3());   // 
            MESSAGE_ON_FIELD = "inserisci il nome del continente " + (generator.getCountContinent() + 1);

        }
        else if (generator.getCountContinent() < generator.getNumContinent())// c'è qualche errore
        {
            generator.setState( new State_gen9());
        }
        return MESSAGE_ON_FIELD;
    }
}
