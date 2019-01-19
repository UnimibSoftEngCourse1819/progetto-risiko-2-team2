public class State_gen1 : State_gen
{
    // creo il nuovo file;
    private const string MESSAGE_ON_FIELD = "Inserisci il numero di continenti";
	public override string Handle(Land_file_gen generator, string message)
    {
        generator.CreateFile(message);
        generator.setState(new State_gen2());
        return MESSAGE_ON_FIELD;
    }
}