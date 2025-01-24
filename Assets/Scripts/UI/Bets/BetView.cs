using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class BetView : MonoBehaviour
    {
        [SerializeField] private Button _addBet;
        [SerializeField] private Button _removeBet;
        [SerializeField] private TextMeshProUGUI _betAmount;

        public event Action IncreaseBetClicked;
        public event Action DecreaseBetClicked;

        public void Start()
        {
            _addBet.onClick.AddListener(() => AddBet());
            _removeBet.onClick.AddListener(() => RemoveBet());
        }

        private void AddBet()
        {
            IncreaseBetClicked?.Invoke();
        }

        private void RemoveBet()
        {
            DecreaseBetClicked?.Invoke();
        }

        public void UpdateBetText(int value)
        {
            _betAmount.text = value.ToString("$0");
        }

        public void SetInteractable(bool isInteractable)
        {
            _addBet.interactable = isInteractable;
            _removeBet.interactable = isInteractable;
        }
    }
}