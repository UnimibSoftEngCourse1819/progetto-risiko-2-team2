public class State_load7 : State_load
{
	public override void Handle(Land_loader loader, string data)
   	{

   		loader.incrementCountNeighbor();
   		loader.getStateScript().Inser_new_neighbour(data);
   		if(loader.getCountNeighbor() < 10)
   			loader.setState(new State_load7());
		else if(loader.getCountNeighbor() < loader.getNumState())
		{
			loader.setCountNeighbor(0);
			loader.setState(new State_load6());
		}
		else if(loader.getCountContinent() < loader.getNumContinent())
		{
			loader.setCountState(0);
			loader.setCountNeighbor(0);
			loader.setState(new State_load2());
		}
		else 
			loader.setState(null);
    }
}