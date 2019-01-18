using System.Collections.Generic;

public class Land 
{
    /*
    MODIFICHE DA EFFETTUARE:
        -Adattare la classe quando viene implementato il game vero e proprio

        Note: NON METTETE VARIABILI CALCOLABILI come numero totale di ect. se vi serve e non 
        si può usare List.Count createvi un metodo 
    */
    private const int MAXNEIGHBOR = 10; 

    private string name;
    private List<Land> neighbors;

    public Land(string name, List<Land> neighbor)
    {
        this.name = name;
        this.neighbors = neighbor;
    }

    public Land(string name): this(name, new List<Land>())
    {
    }
    
    public string getName()
    {
        return name;
    }

    public List<Land> getNeighbors()
    {
        return neighbors;
    }

    public bool hasMaxLands()
    {
        return neighbors.Count == 10;
    }

    public bool addNeighbor(Land newLand)
    {
        bool result = false;
        if (! hasMaxLands())
        {
            neighbors.Add(newLand);
            result = true;
        }

        return result;
    }
    
    public int get_NeighborsNumber()
    {
        return neighbors.Count;
    }

}








