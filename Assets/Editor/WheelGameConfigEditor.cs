using UnityEditor;
using UnityEngine;

namespace WheelSpinGame
{
    [CustomEditor(typeof(WheelGameConfig))]
    public class WheelGameConfigEditor : Editor
    {
        private const string RewardDataFolder = "Assets/ScriptableObjects/WheelSpinGame/Rewards";
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Reward Data"))
            {
                GenerateRewardData();
            }

            WheelGameConfig config = (WheelGameConfig)target;
            if (GUILayout.Button("Generate Random Wheels"))
            {
                config.GenerateWheelPreview();

                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
            }
        }

        private void GenerateRewardData()
        {
            SerializedProperty rewardSprites = serializedObject.FindProperty("_rewardSprites");

            for (int i = 0; i < rewardSprites.arraySize; i++)
            {
                Sprite sprite = rewardSprites.GetArrayElementAtIndex(i).objectReferenceValue as Sprite;
                if (sprite == null)
                    continue;

                string assetPath = $"{RewardDataFolder}/Reward_{sprite.name}.asset";
                if (AssetDatabase.LoadAssetAtPath<RewardData>(assetPath) != null)
                    continue;

                RewardData rewardData = CreateInstance<RewardData>();
                rewardData.name = $"Reward_{sprite.name}";
                rewardData.SetDefaultValues(sprite);

                AssetDatabase.CreateAsset(rewardData, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}