using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen7 : State_gen
{
    private const string MESSAGE_ON_FIELD = "inserisci un vicino dello stato ";
    public override string Handle(Land_file_gen generator, string message)
    {
        generator.IncrementCountState();
        generator.setNameState(message);
        generator.SalvaStato();
        generator.setState(new State_gen8());
        return MESSAGE_ON_FIELD + generator.getNameState();
    }
}
