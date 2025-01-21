using System;
using System.Collections;
using System.Collections.Generic;
using Rolls;
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

        private void Start()
        {
            _spinButton.onClick.AddListener(() => Spin());
        }

        private void Spin()
        {
            StartCoroutine(SpinCoroutine());
        }

        private IEnumerator SpinCoroutine()
        {
            _rolls.ForEach(reel => reel.StartSpin());
            
            yield return new WaitForSeconds(_spinDuration);

            for (int i = 0; i < _rolls.Count; i++)
            {
                _rolls[i].StopSpin();
                _rolls[i].GetLastThreeSymbols();
                yield return new WaitForSeconds(_delayBetweenRollsStopping);
            }
        }
    }
}