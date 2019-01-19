public class State_load5 : State_load
{
	public override void Handle(Land_loader loader, string data)
    {
    	loader.setNumState(int.Parse(data));
    	loader.Istanza_continente();
        loader.setState(new State_load6());
    }
}