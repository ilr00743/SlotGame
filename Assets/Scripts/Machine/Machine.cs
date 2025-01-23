using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Balance;
using Bets;
using Paylines;
using Payout;
using Rolls;
using Symbols;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Machine
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private List<Roll> _rolls;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private float _delayBetweenRollsStopping = 1f;
        [SerializeField] private PaylineConfig _paylineConfig;
        [SerializeField] private BetView _betView;
        [SerializeField] private TotalPayoutView _totalPayoutView;
        [SerializeField] private SymbolsConfig _symbolsConfig;
        
        private VisibleSymbolsHolder _visibleSymbolsHolder;

        private void Start()
        {
            _visibleSymbolsHolder = new VisibleSymbolsHolder(3,5);
            
            _spinButton.onClick.AddListener(() => Spin());
        }

        private void Spin()
        {
            _visibleSymbolsHolder.Reset();
            StartCoroutine(SpinCoroutine());
        }

        private IEnumerator SpinCoroutine()
        {
            _rolls.ForEach(reel => reel.StartSpin());
            
            yield return new WaitForSeconds(_spinDuration);

            for (int i = 0; i < _rolls.Count; i++)
            {
                _rolls[i].StopSpin();
                
                _visibleSymbolsHolder.SetSymbolsInColumn(i, _rolls[i].GetLastThreeSymbols());
                yield return new WaitForSeconds(_delayBetweenRollsStopping);
            }

            var payoutCalculator = new PayoutCalculator(_symbolsConfig);

            var totalPayout = payoutCalculator.CalculateTotalPayout(_visibleSymbolsHolder.VisibleSymbols,
                _paylineConfig.PayLines.Take(_betView.ActivePaylines).ToList(), _betView.CurrentBet);

            Debug.Log($"Total payout: {totalPayout}");
            _totalPayoutView.SetTotalPayout(totalPayout);

        }
    }
}