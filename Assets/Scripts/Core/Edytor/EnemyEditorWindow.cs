using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEditorWindow : EditorWindow
{
    [MenuItem("Tools/Enmey Editor Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<EnemyEditorWindow>();
        window.titleContent = new GUIContent("Enmey Editor");
        window.minSize = new Vector2(800, 600);
    }

    private void OnEnable()
    {
        VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Core/Edytor/EnemyEditor.uxml");
        TemplateContainer treeAsset = original.CloneTree();
        rootVisualElement.Add(treeAsset);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Core/Edytor/EnemyEditorStyle.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        CreateEnemyListView();
    }

    private void CreateEnemyListView()
    {
        FindAllEnemy( out MonsterData[] enemies);

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

                MonsterData enemy = item as MonsterData;

                SerializedObject serializedEnemy = new SerializedObject(enemy);
                SerializedProperty enemyProperty = serializedEnemy.GetIterator();
                enemyProperty.Next(true);

                while (enemyProperty.NextVisible(false))
                {
                    PropertyField prop = new PropertyField(enemyProperty);
                    prop.SetEnabled(enemyProperty.name != "n_Script");
                    prop.Bind(serializedEnemy);
                    enemyInfoBox.Add(prop);

                    if (enemyProperty.name == "enemyImage")
                    {
                        prop.RegisterCallback<ChangeEvent<Object>>((changeEvent) => LoadEnemyImage(enemy.Sprite().texture));
                    }
                }

                LoadEnemyImage(enemy.Sprite().texture);
            }
        };

        enemyList.Refresh();
    }

    private void FindAllEnemy(out MonsterData[] enemies)
    {
        var guids = AssetDatabase.FindAssets("t:MonsterData");

        enemies = new MonsterData[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            enemies[i] = AssetDatabase.LoadAssetAtPath<MonsterData>(path);
        }
    }

    private void LoadEnemyImage(Texture texture)
    {
        var cardPreviewImage = rootVisualElement.Query<Image>("prewiew").First();
        cardPreviewImage.image = texture;
    }
}
