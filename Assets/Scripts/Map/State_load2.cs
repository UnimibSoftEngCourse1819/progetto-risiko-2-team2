public class State_load2 : State_load
{
	public override void Handle(Land_loader loader, string data)  // stato in cui si legge il nome del continente e si incremente il count_continenete
    {
        loader.setNameContinent(data);
        loader.incrementCountContinent();
        loader.setState(new State_load3());
    }
}