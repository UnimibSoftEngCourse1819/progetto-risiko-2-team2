public class State_load1 : State_load
{
	public override void Handle(Land_loader loader, string data)
    {
        loader.setNumContinent(int.Parse(data));
        loader.setState(new State_load2());
    }
}