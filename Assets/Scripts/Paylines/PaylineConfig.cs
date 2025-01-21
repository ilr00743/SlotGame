using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PaylineConfig", menuName = "SlotMachine/PaylineConfig", order = 1)]
public class PaylineConfig : ScriptableObject
{
    public int Rows = 3;
    public int Columns = 5;
    public List<Payline> Paylines = new();
}   