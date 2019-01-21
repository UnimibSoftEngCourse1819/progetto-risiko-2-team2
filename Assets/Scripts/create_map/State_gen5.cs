using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen5 : State_gen
{
    private const string MESSAGE_ON_FIELD = "Inserisci il valore del continente";
    public override string Handle(Land_file_gen generator, string message)
    {
        generator.setValueContinent(int.Parse(message));
        generator.setState(new State_gen6());
        return MESSAGE_ON_FIELD;
    }
}
