using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.TotalPayout
{
    public class TotalPayoutView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _totalPayoutText;
        private Coroutine _animateTextRoutine;

        private void Start()
        {
            _totalPayoutText.text = "";
        }

        public void UpdateText(float value)
        {
            if (_animateTextRoutine != null)
            {
                StopCoroutine(_animateTextRoutine);
                _animateTextRoutine = null;
            }
            
            _animateTextRoutine = StartCoroutine(AnimateText(value));
        }

        private IEnumerator AnimateText(float value)
        {
            _totalPayoutText.text = value == 0 ? "YOU LOSE" : "YOU WIN!";
            
            yield return new WaitForSeconds(1.5f);

            if (value != 0)
            {
                _totalPayoutText.text = "";
                
                yield return new WaitForSeconds(0.5f);
            
                _totalPayoutText.text = value.ToString("$0.0");
            
                yield return new WaitForSeconds(1.5f);
            }

            _totalPayoutText.text = "";
        }
    }
}