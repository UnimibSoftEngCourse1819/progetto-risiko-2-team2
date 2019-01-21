using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen3 : State_gen
{
    private const string MESSAGE_ON_FIELD = "Inserisci il codice del continente";
    public override string Handle(Land_file_gen generator, string message)
    {
        generator.IncrementCountContinent();
        generator.setNameContinent(message);
        generator.setState(new State_gen4());
        return MESSAGE_ON_FIELD + generator.getNameContinent();
    }
}
