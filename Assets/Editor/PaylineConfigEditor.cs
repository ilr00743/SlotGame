using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PaylineConfig))]
public class PaylineConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PaylineConfig config = (PaylineConfig)target;
        
        config.Rows = EditorGUILayout.IntField("Rows", config.Rows);
        config.Columns = EditorGUILayout.IntField("Columns", config.Columns);

        EditorGUILayout.Space();

        if (config.Paylines != null)
        {
            for (int i = 0; i < config.Paylines.Count; i++)
            {
                Payline payline = config.Paylines[i];
                payline.name = EditorGUILayout.TextField($"Payline {i + 1} Name", payline.name);

                if (payline.positions == null || payline.positions.GetLength(0) != config.Rows || payline.positions.GetLength(1) != config.Columns)
                {
                    payline.positions = new bool[config.Rows, config.Columns];
                }

                EditorGUILayout.LabelField("Positions:");
                for (int row = 0; row < config.Rows; row++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int col = 0; col < config.Columns; col++)
                    {
                        GUILayoutOption width = GUILayout.Width(20);
                        GUILayoutOption height = GUILayout.Height(20);
                        payline.positions[row, col] = GUILayout.Toggle(payline.positions[row, col], GUIContent.none, width, height);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space();
            }
        }

        if (GUILayout.Button("Add Payline"))
        {
            config.Paylines.Add(new Payline { name = $"Payline {config.Paylines.Count + 1}" });
        }

        if (GUILayout.Button("Remove Last Payline"))
        {
            if (config.Paylines.Count > 0)
            {
                config.Paylines.RemoveAt(config.Paylines.Count - 1);
            }
        }

        EditorUtility.SetDirty(target);
    }
}
