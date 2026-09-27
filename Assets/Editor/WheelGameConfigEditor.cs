using UnityEngine;
using UnityEditor;

namespace WheelSpinGame
{
    [CustomEditor(typeof(WheelGameConfig))]
    public class WheelGameConfigEditor : Editor
    {
        private const string WheelSlicesFolder = "Assets/ScriptableObjects/WheelSpinGame/WheelSlices";
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Reward WheelSlices"))
            {
                GenerateRewardWheelSlices();
            }
        }

        private void GenerateRewardWheelSlices()
        {
            //WheelGameConfig config = (WheelGameConfig)target;
            SerializedProperty rewardSprites = serializedObject.FindProperty("rewardSprites");

            for (int i = 0; i < rewardSprites.arraySize; i++)
            {
                Sprite sprite = rewardSprites.GetArrayElementAtIndex(i).objectReferenceValue as Sprite;
                if (sprite == null)
                    continue;

                string assetPath = $"{WheelSlicesFolder}/WheelSlice_{sprite.name}.asset";
                if (AssetDatabase.LoadAssetAtPath<WheelSlice>(assetPath) != null)
                    continue;

                WheelSlice wheelSlice = CreateInstance<WheelSlice>();
                wheelSlice.name = $"WheelSlice_{sprite.name}";
                wheelSlice.sliceType = SliceType.Reward;
                wheelSlice.iconSprite = sprite;

                AssetDatabase.CreateAsset(wheelSlice, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}