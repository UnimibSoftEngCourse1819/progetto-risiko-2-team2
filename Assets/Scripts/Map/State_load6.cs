public class State_load6 : State_load
{
	public override void Handle(Land_loader loader, string data)  // stato in cui si legge il nome di uno stato e lo si istanzia e si incremente la count_stati
    {
    	loader.setNameState(data);
    	loader.incrementCountState();
    	loader.Istanzia_stato();
        loader.setState(new State_load7());
    }
}