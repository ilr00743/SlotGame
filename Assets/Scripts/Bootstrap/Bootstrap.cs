using System;
using System.Linq;
using System.Threading.Tasks;
using Machine;
using Paylines;
using Payout;
using RNG.Strategies;
using SymbolGenerators;
using Symbols;
using UI.Balance;
using UI.Bets;
using UI.TotalPayout;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Canvas _levelUI;
        [SerializeField] private SlotMachine _slotMachinePrefab;
        [SerializeField] private PaylineConfig _paylineConfig;
        [SerializeField] private BetView _betViewPrefab;
        [SerializeField] private TotalPayoutView _totalPayoutViewPrefab;
        [SerializeField] private BalanceView _balanceViewPrefab;
        [SerializeField] private SpinButtonView _spinButtonPrefab;
        [SerializeField] private SymbolsConfig _symbolsConfig;
        [SerializeField] private PaylineVisualizer _paylineVisualizerPrefab;

        private async void Start()
        {
            _loadingScreen = Instantiate(_loadingScreen);

            await InitializeGameAsync();
            
            // pseudo loading delay
            await Task.Delay(TimeSpan.FromSeconds(3));
            
            _loadingScreen.SetActive(false);
        }

        private async Task InitializeGameAsync()
        {
            var levelUI = Instantiate(_levelUI);
            
            var betModel = new BetModel(5, 5, 100, _paylineConfig.PayLines.Count);
            var totalPayoutModel = new TotalPayoutModel();
            var balanceModel = new BalanceModel(currentBalance: 1000);
            
            var betView = Instantiate(_betViewPrefab, levelUI.transform);
            var betPresenter = new BetPresenter(betView, betModel);

            var totalPayoutView = Instantiate(_totalPayoutViewPrefab, levelUI.transform);
            var totalPayoutController = new TotalPayoutController(totalPayoutModel, totalPayoutView);

            var balanceView = Instantiate(_balanceViewPrefab, levelUI.transform);
            var balanceController = new BalanceController(balanceModel, balanceView);
            
            var spinButton = Instantiate(_spinButtonPrefab, levelUI.transform);

            var randomNumberGenerator = new TimeBasedRandom(0, _symbolsConfig.Symbols.Count);
            var symbolGenerator = new SymbolGenerator(_symbolsConfig, randomNumberGenerator);

            var paylineVisualizer = Instantiate(_paylineVisualizerPrefab);
            paylineVisualizer.InitializePaylines(_paylineConfig.PayLines.ToList());
            
            var paylineChecker = new PaylineChecker();
            var payoutCalculator = new PayoutCalculator(_symbolsConfig);

            var slotMachine = Instantiate(_slotMachinePrefab);
            
            slotMachine.Initialize(
                paylineChecker,
                payoutCalculator,
                balanceController,
                betPresenter,
                totalPayoutController,
                spinButton,
                paylineVisualizer,
                symbolGenerator
            );

            await Task.CompletedTask;
        }
    }
}