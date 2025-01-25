using System;
using TMPro;
using UnityEngine;

namespace UI.Balance
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balance;

        public void UpdateBalance(float value)
        {
            _balance.text = value.ToString("$0.0");
        }
    }
}