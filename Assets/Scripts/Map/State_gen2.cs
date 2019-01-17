using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen2 : State_gen
{
    private const string MESSAGE_ON_FIELD = "Inserisci il nome del continente ";
    public override string Handle(Land_file_gen generator, string message)
    {
        generator.setNumContinenti(int.Parse(message));
        generator.NumeroContinenti();
        generator.setState(new State_gen3());
        return MESSAGE_ON_FIELD + generator.getNameContinent();
    }
}
