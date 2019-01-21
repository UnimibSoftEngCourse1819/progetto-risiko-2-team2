public class State_load3 : State_load
{
	public override void Handle(Land_loader loader, string data)  // stato in cui si legge il codice del continente
    {
       	loader.setCodeContinent(data);
		loader.setState(new State_load4());
    }
}