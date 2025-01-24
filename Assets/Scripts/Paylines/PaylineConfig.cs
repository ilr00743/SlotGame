using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaylineConfig", menuName = "SlotMachine/PaylineConfig", order = 1)]
public class PaylineConfig : ScriptableObject
{
    [SerializeField] private int _rows = 3;
    [SerializeField] private int _columns = 5;
    [SerializeField] private List<Payline> _payLines = new();

    public IReadOnlyList<Payline> PayLines => _payLines;

    public int Rows { get => _rows;
        set {
            if (value < 1)
            {
                Debug.LogError("Rows cannot be less than 1");
                return;
            }
            _rows = value; 
        }
    }

    public int Columns { get => _columns; 
        set {
        if (value < 3)
        {
            Debug.LogError("Columns cannot be less than 3");
            return;
        }
        _columns = value; 
    }}

    public void Add(Payline payline)
    {
        _payLines.Add(payline);
    }

    public void RemoveAt(int index)
    {
        _payLines.RemoveAt(index);
    }
}   