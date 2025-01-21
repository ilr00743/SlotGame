using System.Collections.Generic;
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

        if (config.PayLines != null)
        {
            for (int i = 0; i < config.PayLines.Count; i++)
            {
                Payline payline = config.PayLines[i];

                payline.IsExpanded = EditorGUILayout.Foldout(payline.IsExpanded, $"Payline {i + 1}: {payline.Name}", true);

                if (payline.IsExpanded)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    payline.Name = EditorGUILayout.TextField("Name", payline.Name);

                    // Перевірка та ініціалізація payline.positions
                    if (payline.Positions == null || payline.Positions.Count != config.Rows || (payline.Positions.Count > 0 && payline.Positions[0].Count != config.Columns))
                    {
                        bool needResize = payline.Positions == null || 
                                          payline.Positions.Count != config.Rows || 
                                          (payline.Positions.Count > 0 && payline.Positions[0].Count != config.Columns);

                        if (needResize)
                        {
                            var oldPositions = payline.Positions;
                            payline.Positions = new List<List<bool>>();

                            for (int row = 0; row < config.Rows; row++)
                            {
                                payline.Positions.Add(new List<bool>());
                                for (int col = 0; col < config.Columns; col++)
                                {
                                    bool oldValue = (oldPositions != null && row < oldPositions.Count && col < oldPositions[row].Count) && oldPositions[row][col];
                                    payline.Positions[row].Add(oldValue);
                                }
                            }
                        }
                    }

                    EditorGUILayout.LabelField("Positions:");
                    for (int row = 0; row < config.Rows; row++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        for (int col = 0; col < config.Columns; col++)
                        {
                            GUILayoutOption width = GUILayout.Width(20);
                            GUILayoutOption height = GUILayout.Height(20);
                            payline.Positions[row][col] = GUILayout.Toggle(payline.Positions[row][col], GUIContent.none, width, height);
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    
                    payline.LineColor = EditorGUILayout.ColorField("Line Color:", payline.LineColor);
                    
                    EditorGUILayout.Space(35);

                    if (GUILayout.Button("Remove This Payline"))
                    {
                        config.RemoveAt(i);
                        EditorUtility.SetDirty(config);
                        break;
                    }

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
            }
        }

        if (GUILayout.Button("Add Payline"))
        {
            config.Add(new Payline { Name = $"Payline {config.PayLines.Count + 1}"});
            EditorUtility.SetDirty(config);
        }

        if (GUILayout.Button("Remove Last Payline"))
        {
            if (config.PayLines.Count > 0)
            {
                config.RemoveAt(config.PayLines.Count - 1);
                EditorUtility.SetDirty(config);
            }
        }
        
        EditorUtility.SetDirty(target);
    }
}