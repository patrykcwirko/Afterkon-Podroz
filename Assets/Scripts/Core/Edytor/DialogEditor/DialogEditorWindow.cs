using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Core.Edytor.DialogEditor
{
    public class DialogEditorWindow : EditorWindow
    {
        [MenuItem("Tools/Dialog Editor Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<DialogEditorWindow>();
            window.titleContent = new GUIContent("Dialog Editor");
            window.minSize = new Vector2(800, 600);
        }

        private void OnEnable()
        {
            VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Core/Edytor/DialogEditor/DialogEditor.uxml");
            TemplateContainer treeAsset = original.CloneTree();
            rootVisualElement.Add(treeAsset);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Core/Edytor/DialogEditor/DialogEditorStyle.uss");
            rootVisualElement.styleSheets.Add(styleSheet);

            CreateEnemyListView();
        }

        private void CreateEnemyListView()
        {
            FindAllEnemy(out Dialog[] enemies);

            ListView enemyList = rootVisualElement.Query<ListView>("enemy-list").First();
            enemyList.makeItem = () => new Label();
            enemyList.bindItem = (element, i) => (element as Label).text = enemies[i].name;

            enemyList.itemsSource = enemies;
            enemyList.itemHeight = 16;
            enemyList.selectionType = SelectionType.Single;

            enemyList.onSelectionChanged += (enumerable) =>
            {
                foreach (Object item in enumerable)
                {
                    Box enemyInfoBox = rootVisualElement.Query<Box>("enemy-info").First();
                    enemyInfoBox.Clear();

                    Dialog enemy = item as Dialog;

                    SerializedObject serializedEnemy = new SerializedObject(enemy);
                    SerializedProperty enemyProperty = serializedEnemy.GetIterator();
                    enemyProperty.Next(true);

                    while (enemyProperty.NextVisible(false))
                    {
                        PropertyField prop = new PropertyField(enemyProperty);
                        prop.SetEnabled(enemyProperty.name != "n_Script");
                        prop.Bind(serializedEnemy);
                        enemyInfoBox.Add(prop);

                        if (enemyProperty.name == "monsterStats" && enemyProperty.objectReferenceValue != null)
                        {
                            Stats stats = enemyProperty.objectReferenceValue as Stats;

                            SerializedObject serializedStats = new SerializedObject(stats);
                            SerializedProperty statsProperty = serializedStats.GetIterator();
                            statsProperty.Next(true);
                            while (statsProperty.NextVisible(false))
                            {
                                PropertyField propStats = new PropertyField(statsProperty);
                                propStats.SetEnabled(statsProperty.name != "n_Script");
                                propStats.Bind(serializedStats);
                                enemyInfoBox.Add(propStats);
                            }
                        }
                    }
                    System.Action action = () =>
                    {
                        rootVisualElement.Query<Button>().ForEach((button) =>
                        {
                            enemy.dialog.Add(new Dialog.DialogStr());
                        });
                    };

                    // Create a new Button with an action and give it a style class.
                    var csharpButton = new Button(action) { text = "Dodaj wypowiedź" };
                    csharpButton.AddToClassList("some-styled-button");
                    enemyInfoBox.Add(csharpButton);
                }
            };

            enemyList.Refresh();
        }

        private void FindAllEnemy(out Dialog[] enemies)
        {
            var guids = AssetDatabase.FindAssets("t:Dialog");

            enemies = new Dialog[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                enemies[i] = AssetDatabase.LoadAssetAtPath<Dialog>(path);
            }
        }
    }
}