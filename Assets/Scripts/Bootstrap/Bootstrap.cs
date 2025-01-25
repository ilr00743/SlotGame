using System;
using System.Linq;
using System.Threading.Tasks;
using Machine;
using Paylines;
using Payout;
using RNG.Factories;
using Services;
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
        [Space]
        [SerializeField] private PaylineConfig _paylineConfig;
        [SerializeField] private SymbolsConfig _symbolsConfig;
        [SerializeField] private BetsConfig _betsConfig;
        [Space]
        [SerializeField] private BetView _betViewPrefab;
        [SerializeField] private SlotMachine _slotMachinePrefab;
        [SerializeField] private TotalPayoutView _totalPayoutViewPrefab;
        [SerializeField] private BalanceView _balanceViewPrefab;
        [SerializeField] private SpinButtonView _spinButtonPrefab;
        [SerializeField] private PaylineVisualizer _paylineVisualizerPrefab;

        private async void Start()
        {
            Application.targetFrameRate = 60;
            
            _loadingScreen = Instantiate(_loadingScreen);

            await InitializeGameAsync();
            
            // pseudo loading delay
            await Task.Delay(TimeSpan.FromSeconds(3));
            
            _loadingScreen.SetActive(false);
        }

        private async Task InitializeGameAsync()
        {
            var levelUI = Instantiate(_levelUI);
            
            var betModel = new BetModel(_betsConfig.BetStep, _betsConfig.MinBet, _betsConfig.MaxBet, _paylineConfig.PayLines.Count);
            var betView = Instantiate(_betViewPrefab, levelUI.transform);
            var betPresenter = new BetPresenter(betView, betModel);

            var totalPayoutModel = new TotalPayoutModel();
            var totalPayoutView = Instantiate(_totalPayoutViewPrefab, levelUI.transform);
            var totalPayoutController = new TotalPayoutController(totalPayoutModel, totalPayoutView);

            var balanceModel = new BalanceModel(currentBalance: 1000);
            var balanceView = Instantiate(_balanceViewPrefab, levelUI.transform);
            var balanceController = new BalanceController(balanceModel, balanceView);
            
            var spinButton = Instantiate(_spinButtonPrefab, levelUI.transform);
            
            var playerFinanceService = new PlayerFinanceService(balanceController, betPresenter, totalPayoutController);
            playerFinanceService.UpdateBalanceOnly();
            
            var machineButtonsService = new MachineButtonsService(betPresenter, spinButton);


            var paylineChecker = new PaylineChecker();
            var paylineVisualizer = Instantiate(_paylineVisualizerPrefab);
            paylineVisualizer.InitializePaylines(_paylineConfig.PayLines.ToList());
            var payoutCalculator = new PayoutCalculator(_symbolsConfig);
            var paylineService = new PaylineService(paylineChecker, payoutCalculator, paylineVisualizer, _paylineConfig);

            // var weigths = _symbolsConfig.Symbols.Select(s => s.Weight).ToList();
            // var randomNumberGenerator = RNGFactory.CreateWeightedRNG(weigths);

            //var randomNumberGenerator = RNGFactory.CreateGuaranteedWinRNG(_symbolsConfig.Symbols.Count - 3);
            
            var randomNumberGenerator = RNGFactory.CreateBufferedCryptoRNG(0, _symbolsConfig.Symbols.Count);
            var symbolGenerator = new SymbolGenerator(_symbolsConfig, randomNumberGenerator);
            var slotMachine = Instantiate(_slotMachinePrefab);
            
            slotMachine.Initialize(paylineService, playerFinanceService, machineButtonsService, symbolGenerator);

            await Task.CompletedTask;
        }
    }
}