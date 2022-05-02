using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class PrintData
{
    private string _customer;
    private List<string> _names;
    private List<int> _amounts;
    private List<int> _audiences;
    private int _totalAmount;
    private int _volumeCredits;

    public string Customer { get => _customer; set => _customer = value; }
    public List<string> Names { get => _names; set => _names = value; }
    public List<int> Amounts { get => _amounts; set => _amounts = value; }
    public List<int> Audiences { get => _audiences; set => _audiences = value; }
    public int TotalAmount { get => _totalAmount; set => _totalAmount = value; }
    public int VolumeCredits { get => _volumeCredits; set => _volumeCredits = value; }

    public PrintData()
    {
        _names = new List<string>();
        _amounts = new List<int>();
        _audiences = new List<int>();
    }

}