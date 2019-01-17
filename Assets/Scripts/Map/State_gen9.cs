using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class State_gen9 : State_gen
{
    private const string MESSAGE_ON_FIELD = "inizializzazione completata";
    public override string Handle(Land_file_gen generator, string message)
    {
            generator.setState(null);
            generator.getInputController().DeactivateInputField();
            return MESSAGE_ON_FIELD;
    }
}
