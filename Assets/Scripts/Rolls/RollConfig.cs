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
        
        public float SpinSpeed => _spinSpeed;
        public float SpinDuration => _spinDuration;
        public float DelayBetweenRollsStop => _delayBetweenRollsStop;
        public float ItemsOffset => _itemsOffset;
    }
}