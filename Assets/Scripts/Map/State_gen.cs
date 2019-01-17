using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class State_gen
{
    public abstract string Handle(Land_file_gen generator, string message);
}
