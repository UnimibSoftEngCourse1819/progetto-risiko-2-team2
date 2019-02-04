using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateControl
{
    public abstract string action();
    public abstract StateControl nextPhase();
    public abstract StateControl nextPhaseForced();
    public abstract string needSaving(string land);
    public abstract List<string> getMissingData();

}
