public class State_load7 : State_load
{
	public override void Handle(Land_loader loader, string data) // stato in cui si leggono i vicini
   	{

   		loader.incrementCountNeighbor();
   		loader.getStateScript().Inser_new_neighbour(data);
   		if(loader.getCountNeighbor() < 10)  // controllo se non ho letto tutti i vicini
   			loader.setState(new State_load7());
		else if(loader.GetCountState() < loader.getNumState()) // se ho letto tuti i vicini  -> controllo se ho letto tutti gli stati
		{
			loader.setCountNeighbor(0);
			loader.setState(new State_load6());
		}
		else if(loader.getCountContinent() < loader.getNumContinent())  // se ho letto tutti gli stati -> contrllo se ho letto tutti i continenti
		{
			loader.setCountState(0);
			loader.setCountNeighbor(0);
			loader.setState(new State_load2());
		}
		else 
			loader.setState(null);
    }
}