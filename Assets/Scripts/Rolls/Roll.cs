using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private float _itemYPosition;

        private List<SymbolView> _symbols;
        
        [SerializeField] private float _symbolHeight = 2f;
        [SerializeField] private float _spinSpeed = 3f;
        [SerializeField] private float _itemsOffset = 4f;
        
        private bool _isSpinning = false;
        private bool _centerItemsOnScreen;

        private void Start()
        {
            _symbols = new List<SymbolView>(Random.Range(5, _symbolsConfig.Symbols.Count));

            for (var i = 0; i < _symbols.Capacity; i++)
            {
                var randomSymbolFromConfig = _symbolsConfig.Symbols[Random.Range(0, _symbolsConfig.Symbols.Count)];
                var symbolView = Instantiate(_symbolPrefab, default, Quaternion.identity, transform);
                symbolView.gameObject.name = (i + 1).ToString();
                symbolView.Initialize(randomSymbolFromConfig.Name, randomSymbolFromConfig.Icon, i + 1);
                
                symbolView.transform.localPosition = Vector3.up * _itemYPosition + (i * GetSpaceBetweenSymbols());
                
                _symbols.Add(symbolView);
            }
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

        public void StartSpin()
        {
            _isSpinning = true;
        }

        private void SpinReel()
        {
            for (var i = 0; i < _symbols.Count; i++)
            {
                var symbolView = _symbols[i];
                SpinSymbol(symbolView);
            }
        }
        
        private void SpinSymbol(SymbolView symbol)
        {
            symbol.MoveDown(_spinSpeed);

            if (symbol.transform.localPosition.y < _reelBottomBoundary.localPosition.y)
            {
                symbol.transform.localPosition = GetLastItemPosition() + GetSpaceBetweenSymbols();
                ReplaceFirstSymbolToBack();
                GenerateNewSymbol(symbol);
            }
        }

        private void GenerateNewSymbol(SymbolView symbolView)
        {
            var randomSymbolFromConfig = _symbolsConfig.Symbols[Random.Range(0, _symbolsConfig.Symbols.Count)];
            
            symbolView.Initialize(randomSymbolFromConfig.Name, randomSymbolFromConfig.Icon);
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
        
        public void StopSpin()
        {
            _isSpinning = false;
            _centerItemsOnScreen = true;
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
            if (_centerItemsOnScreen)
            {
                Vector3 localPosition = Vector3.zero;
                for (int i = 0; i < _symbols.Count; ++i)
                {
                    localPosition = Vector3.up * _itemYPosition + (i * GetSpaceBetweenSymbols());
                    _symbols[i].transform.localPosition = Vector3.Lerp(_symbols[i].transform.localPosition, localPosition, 2f * Time.deltaTime );
                }
                if (_symbols[^1] && Mathf.Abs(_symbols[^1].transform.localPosition.y - localPosition.y) < 0.01f)
                {
                    _centerItemsOnScreen = false;
                }
            }
        }
    }
}