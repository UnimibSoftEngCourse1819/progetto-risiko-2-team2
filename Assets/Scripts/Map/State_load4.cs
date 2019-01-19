public class State_load4 : State_load
{
	public override void Handle(Land_loader loader, string data)
    {
    	loader.setValueContinent(int.Parse(data));
        loader.setState(new State_load5());
    }
}