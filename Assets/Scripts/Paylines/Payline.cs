using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[Serializable]
public class Payline : ISerializationCallbackReceiver
{
    public string Name;
    public Color LineColor = Color.white;
    public bool IsExpanded = true;

    [NonSerialized] public List<List<bool>> Positions = new List<List<bool>>();

    [SerializeField] private List<bool> _serializedPositions = new List<bool>();
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public bool[,] PositionsArray => ListUtils.NestedListsToArray(Positions);
    public void OnBeforeSerialize()
    {
        _serializedPositions.Clear();
        rows = Positions.Count;
        columns = (rows > 0) ? Positions[0].Count : 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                _serializedPositions.Add(Positions[row][col]);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        Positions.Clear();

        for (int row = 0; row < rows; row++)
        {
            Positions.Add(new List<bool>());
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                Positions[row].Add(_serializedPositions[index]);
            }
        }
    }
}