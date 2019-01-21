using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen6 : State_gen
{
    private const string MESSAGE_ON_FIELD = "Inserisci il nome dello stato numero ";
    public override string Handle(Land_file_gen generator, string message)
    {
        int c = generator.getCountState() + 1;
        generator.setNumState(int.Parse(message));
        generator.SalvaContinente();
        generator.setState(new State_gen7());
        return MESSAGE_ON_FIELD;
    }
}
