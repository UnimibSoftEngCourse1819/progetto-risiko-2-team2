public class State_load6 : State_load
{
	public override void Handle(Land_loader loader, string data)
    {
    	loader.setNameState(data);
    	loader.incrementCountState();
    	loader.Istanzia_stato();
        loader.setState(new State_load7());
    }
}