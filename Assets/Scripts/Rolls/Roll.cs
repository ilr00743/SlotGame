using System.Collections;
using System.Collections.Generic;
using RNG.Strategies;
using SymbolGenerators;
using Symbols;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rolls
{
    public class Roll : MonoBehaviour
    {
        [SerializeField] private SymbolsConfig _symbolsConfig;
        [SerializeField] private SymbolView _symbolPrefab;
        [SerializeField] private Transform _reelBottomBoundary;
        [SerializeField] private Transform _firstSymbolPosition;
        [SerializeField] private float _spinSpeed = 3f;
        [SerializeField] private float _itemsOffset = 4f;

        public bool IsAligning => _centerItemsOnScreen;
        
        private List<SymbolView> _symbols;
        private bool _isSpinning = false;
        private bool _centerItemsOnScreen;
        
        private IRandomNumberGenerator _randomNumberGenerator;
        private ISymbolGenerator _symbolGenerator;

        public void Initialize(ISymbolGenerator symbolGenerator, int minSymbolCount, int maxSymbolCount)
        {
            var initialSymbolCount = Random.Range(minSymbolCount, maxSymbolCount);
            
            _symbolGenerator = symbolGenerator;
            _symbols = new List<SymbolView>();

            for (int i = 0; i < initialSymbolCount; i++)
            {
                var symbolView = Instantiate(_symbolPrefab, transform);
                _symbolGenerator.GenerateNewSymbol(symbolView);
                
                symbolView.transform.localPosition = Vector3.up * _firstSymbolPosition.localPosition.y + (i * GetSpaceBetweenSymbols());
                
                _symbols.Add(symbolView);
            }
        }
        
        public void StartSpin()
        {
            _isSpinning = true;
        }
        
        public void StopSpin()
        {
            _isSpinning = false;
            _centerItemsOnScreen = true;
        }

        private void Update()
        {
            if (!_isSpinning)
            {
                AlignSymbols();
                return;
            }
            
            SpinReel();
        }

        private Vector3 GetSpaceBetweenSymbols()
        {
            return Vector3.up * _itemsOffset;
        }

        private void SpinReel()
        {
            for (var i = 0; i < _symbols.Count; i++)
            {
                var symbolView = _symbols[i];
                
                symbolView.MoveDown(_spinSpeed);

                if (symbolView.transform.localPosition.y < _reelBottomBoundary.localPosition.y)
                {
                    symbolView.transform.localPosition = GetLastItemPosition() + GetSpaceBetweenSymbols();
                    ReplaceFirstSymbolToBack();
                    _symbolGenerator.GenerateNewSymbol(symbolView);
                }
            }
        }

        private Vector3 GetLastItemPosition()
        {
            return _symbols[^1].transform.localPosition;
        }

        private void ReplaceFirstSymbolToBack()
        {
            var firstSymbol = _symbols[0];
            _symbols.Add(firstSymbol);
            _symbols.RemoveAt(0);
        }
        
        public List<SymbolView> GetLastThreeSymbols()
        {
            var list = new List<SymbolView>();

            for (var i = 2; i >= 0; i--)
            {
                list.Add(_symbols[i]);
            }
            return list;
        }

        private void AlignSymbols()
        {
            if (!_centerItemsOnScreen) return;
            
            bool allAligned = true;
            float alignmentSpeed = 20f;

            for (int i = 0; i < _symbols.Count; i++)
            {
                Vector3 targetPosition = _firstSymbolPosition.localPosition + (i * GetSpaceBetweenSymbols());
                _symbols[i].transform.localPosition = Vector3.Lerp(
                    _symbols[i].transform.localPosition,
                    targetPosition,
                    alignmentSpeed * Time.deltaTime
                );

                if (Vector3.Distance(_symbols[i].transform.localPosition, targetPosition) > 0.01f)
                {
                    allAligned = false;
                }
            }

            if (allAligned)
            {
                _centerItemsOnScreen = false; // Вирівнювання завершено
            }
        }
        
        /*private void AlignSymbols()
        {
            if (_centerItemsOnScreen)
            {
                Vector3 localPosition = Vector3.zero;
                for (int i = 0; i < _symbols.Count; ++i)
                {
                    localPosition = Vector3.up * _firstSymbolPosition.localPosition.y + (i * GetSpaceBetweenSymbols());
                    _symbols[i].transform.localPosition = Vector3.Lerp(_symbols[i].transform.localPosition, localPosition, 40f * Time.deltaTime );
                }
                if (_symbols[^1] && Mathf.Abs(_symbols[^1].transform.localPosition.y - localPosition.y) < 0.01f)
                {
                    _centerItemsOnScreen = false;
                }
            }
        }*/
    }
}