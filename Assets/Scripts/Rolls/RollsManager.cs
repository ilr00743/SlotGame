using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Symbols;
using UnityEngine;

namespace Rolls
{
    public class RollsManager
    {
        private const int ROWS = 3;
        private const int COLUMNS = 5;
        
        private readonly List<Roll> _rolls;

        private SymbolView[,] _visibleSymbols;
        private Coroutine _spinRoutine;

        public RollsManager(List<Roll> rolls)
        {
            _rolls = rolls;
            _visibleSymbols = new SymbolView[3, _rolls.Count];
        }

        public void StartSpin()
        {
            _rolls.ForEach(roll => roll.StartSpin());
        }

        public void StopReel(int index)
        {
            _rolls[index].StopSpin();
        }

        public SymbolView[,] GetVisibleSymbols()
        {
            for (int i = 0; i < _rolls.Count; i++)
            {
                var symbols = _rolls[i].GetLastThreeSymbols();
                for (int j = 0; j < symbols.Count; j++)
                {
                    _visibleSymbols[j, i] = symbols[j];
                }
            }
            return _visibleSymbols;
        }

        public void ResetVisibleSymbols()
        {
            if (_visibleSymbols.Length == 0) return;
            
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    _visibleSymbols[i, j] = null;
                }
            }
        }
    }
}