using UnityEngine;

namespace UI.Bets
{
    [CreateAssetMenu(fileName = "BetsConfig", menuName = "SlotMachine/Bets Config", order = 0)]
    public class BetsConfig : ScriptableObject
    {
        [SerializeField] private int _minBet;
        [SerializeField] private int _maxBet;
        [SerializeField] private int _betStep;
        
        public int MinBet => _minBet;
        public int MaxBet => _maxBet;
        public int BetStep => _betStep;
    }
}