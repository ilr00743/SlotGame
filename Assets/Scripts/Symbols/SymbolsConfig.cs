using System.Collections.Generic;
using UnityEngine;

namespace Symbols
{
    [CreateAssetMenu(fileName = "SymbolsConfig", menuName = "SlotMachine/SymbolsConfig", order = 0)]
    public class SymbolsConfig : ScriptableObject
    {
        [SerializeField] private List<Symbol> _symbols;
        
        public IReadOnlyList<Symbol> Symbols => _symbols;
    }
}