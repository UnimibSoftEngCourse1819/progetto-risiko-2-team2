public class State_load2 : State_load
{
	public override void Handle(Land_loader loader, string data)
    {
        loader.setNameContinent(data);
        loader.incrementCountContinent();
        loader.setState(new State_load3());
    }
}