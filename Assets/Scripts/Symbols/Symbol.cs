using System;
using UnityEngine;

namespace Symbols
{
    [Serializable]
    public class Symbol
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _weight;
        
        public string Name => _name;
        public Sprite Icon => _icon;
        public int Weight => _weight;
    }
}