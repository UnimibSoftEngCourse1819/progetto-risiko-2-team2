public class State_load5 : State_load
{
	public override void Handle(Land_loader loader, string data)  // stato in cui si legge il numero di stati del contiente e poi lo si istanzia
    {
    	loader.setNumState(int.Parse(data));
    	loader.Istanza_continente();
        loader.setState(new State_load6());
    }
}