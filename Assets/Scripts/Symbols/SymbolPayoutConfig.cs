using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Symbols
{
    [CreateAssetMenu(fileName = "SymbolPayoutConfig", menuName = "SlotMachine/Symbol Payout Config", order = 0)]
    public class SymbolPayoutConfig : ScriptableObject
    {
        [SerializeField] private List<SymbolPayout> _symbolPayoutPairs;
        
        public IReadOnlyDictionary<string, int> SymbolPayoutPairs => _symbolPayoutPairs
            .ToDictionary(key => key.SymbolName, value => value.Multiplier);
    }
}