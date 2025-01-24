using System.Collections.Generic;
using Symbols;
using UnityEngine;

namespace Paylines
{
    public class PaylineVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject _paylinePrefab;
        private List<LineRenderer> _activePaylinesRenderers = new();
        private int _minMatchesCount = 3;

        public void InitializePaylines(List<Payline> paylines)
        {
            foreach (var payline in paylines)
            {
                GameObject paylineObject = Instantiate(_paylinePrefab, transform);
                LineRenderer lineRenderer = paylineObject.GetComponent<LineRenderer>();
                
                lineRenderer.startColor = payline.LineColor;
                lineRenderer.endColor = payline.LineColor;
                
                _activePaylinesRenderers.Add(lineRenderer);
            }
        }

        public void UpdatePaylines(List<Payline> paylines, SymbolView[,] symbols, List<(SymbolView symbol, int matches)> symbolMatchesList, int activePaylinesCount)
        {
            if (activePaylinesCount != symbolMatchesList.Count)
            {
                Debug.LogError($"Кількість активних пейлайнів ({activePaylinesCount}) не відповідає кількості результатів ({symbolMatchesList.Count})!");
                return;
            }

            for (int i = 0; i < activePaylinesCount; i++)
            {
                Payline payline = paylines[i];
                LineRenderer lineRenderer = _activePaylinesRenderers[i];
                int matches = symbolMatchesList[i].matches;

                if (matches >= _minMatchesCount)
                {
                    List<Vector3> positions = GetMatchedSymbolsPositions(payline.PositionsArray, symbols, matches);
    
                    lineRenderer.positionCount = positions.Count;
                    lineRenderer.SetPositions(positions.ToArray());
                }
                else
                {
                    lineRenderer.positionCount = 0;
                }
            }
        }

        private List<Vector3> GetMatchedSymbolsPositions(bool[,] paylinePattern, SymbolView[,] symbolsViews, int matches)
        {
            var positions = new List<Vector3>();
            
            var columns = paylinePattern.GetLength(1);
            var rows = paylinePattern.GetLength(0);
            var currentMatches = 0;

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (paylinePattern[row, col])
                    {
                        if (currentMatches < matches)
                        {
                            positions.Add(symbolsViews[row, col].transform.position);
                            currentMatches++;
                        }
                        break;
                    }
                }

                if (currentMatches >= matches)
                    break;

            }
            
            return positions;
        }

        public void HideAllPaylines()
        {
            foreach (var paylineRenderer in _activePaylinesRenderers)
            {
                paylineRenderer.positionCount = 0;
            }
        }
    }
}