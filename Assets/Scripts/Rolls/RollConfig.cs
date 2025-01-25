using UnityEngine;

namespace Rolls
{
    [CreateAssetMenu(fileName = "RollConfig", menuName = "SlotMachine/Roll Config", order = 0)]
    public class RollConfig : ScriptableObject
    {
        [SerializeField] private float _spinSpeed;
        [SerializeField] private float _spinDuration;
        [SerializeField] private float _delayBetweenRollsStop;
        [SerializeField] private float _itemsOffset;
        [SerializeField] private int _minSymbols;
        [SerializeField] private int _maxSymbols;
        
        public float SpinSpeed => _spinSpeed;
        public float SpinDuration => _spinDuration;
        public float DelayBetweenRollsStop => _delayBetweenRollsStop;
        public float ItemsOffset => _itemsOffset;
        public int MinSymbols => _minSymbols;
        public int MaxSymbols => _maxSymbols;
    }
}