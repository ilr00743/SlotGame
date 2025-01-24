using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Paylines;
using Payout;
using Rolls;
using Symbols;
using UI.Balance;
using UI.Bets;
using UI.TotalPayout;
using UnityEngine;
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
        [SerializeField] private BalanceView _balanceView;
        [SerializeField] private SymbolsConfig _symbolsConfig;
        [SerializeField] private PaylineVisualizer _paylineVisualizer;
        
        private VisibleSymbolsHolder _visibleSymbolsHolder;
        private PaylineChecker _paylineChecker;
        private BetPresenter _betPresenter;
        private TotalPayoutController _totalPayoutController;
        private BalanceController _balanceController;

        private void Start()
        {
            _betPresenter = new BetPresenter(_betView, new BetModel(5, 5, 100, _paylineConfig.PayLines.Count));
            _totalPayoutController = new TotalPayoutController(new TotalPayoutModel(), _totalPayoutView);
            _balanceController = new BalanceController(new BalanceModel(1000), _balanceView);
            
            _paylineChecker = new PaylineChecker();
            _visibleSymbolsHolder = new VisibleSymbolsHolder(3,5);
            
            _paylineVisualizer.InitializePaylines(_paylineConfig.PayLines.ToList());
            
            _spinButton.onClick.AddListener(() => Spin());
        }

        private void Spin()
        {
            _betPresenter.SetInteractable(false);
            
            _balanceController.DecreaseBalance(_betPresenter.GetBet());
            _balanceController.UpdateBalanceView();
            
            _visibleSymbolsHolder.Reset();
            StartCoroutine(SpinCoroutine());
            _paylineVisualizer.HideAllPaylines();
            
            _spinButton.interactable = false;
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

            List<(SymbolView, int)> symbolMatchesList = new List<(SymbolView, int)>();
            
            for (int i = 0; i < _betPresenter.GetActivePaylinesCount(); i++)
            {
                if (i >= _paylineConfig.PayLines.Count)
                    break;
                
                var symbolMatches = _paylineChecker.GetPaylineMatches(_visibleSymbolsHolder.VisibleSymbols,
                    _paylineConfig.PayLines[i].PositionsArray);
                
                Debug.Log($"Пейлайн {i}: Символ = {symbolMatches.symbol?.Name}, Збігів = {symbolMatches.matches}");
                
                symbolMatchesList.Add(symbolMatches);
            }

            var payoutCalculator = new PayoutCalculator(_symbolsConfig);

            var totalPayout = payoutCalculator.CalculateTotalPayout(symbolMatchesList, _betPresenter.GetBet());

            Debug.Log($"Total payout: {totalPayout}");
            
            _totalPayoutController.SetTotalPayout(totalPayout);
            _totalPayoutController.UpdateView();
            
            _paylineVisualizer.UpdatePaylines(_paylineConfig.PayLines.ToList(), _visibleSymbolsHolder.VisibleSymbols, symbolMatchesList, _betPresenter.GetActivePaylinesCount());
            
            _balanceController.IncreaseBalance(_totalPayoutController.GetTotalPayout());
            
            _balanceController.UpdateBalanceView();
            
            _spinButton.interactable = true;
            
            _betPresenter.SetInteractable(true);


        }
    }
}