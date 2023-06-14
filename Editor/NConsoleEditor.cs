#if UNITY_EDITOR
using UnityEditor;

namespace Nazio_LT.Tools.Console.NEditor
{
    [CustomEditor(typeof(NConsole))]
    public class NConsoleEditor : Editor
    {
        private SerializedProperty m_theme;

        private SerializedProperty m_consoleContentParent;
        private SerializedProperty m_textPrefab;
        private SerializedProperty m_clearButton;
        private SerializedProperty m_terminal;
        private SerializedProperty m_autoCompletion;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Theme", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(m_theme);


            EditorGUILayout.Space();


            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(m_clearButton);
            EditorGUILayout.PropertyField(m_consoleContentParent);
            EditorGUILayout.PropertyField(m_terminal);
            EditorGUILayout.PropertyField(m_autoCompletion);
            EditorGUILayout.PropertyField(m_textPrefab);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            m_theme = serializedObject.FindProperty("m_theme");

            m_consoleContentParent = serializedObject.FindProperty("m_consoleContentParent");
            m_textPrefab = serializedObject.FindProperty("m_textPrefab");
            m_clearButton = serializedObject.FindProperty("m_clearButton");
            m_terminal = serializedObject.FindProperty("m_terminal");
            m_autoCompletion = serializedObject.FindProperty("m_autoCompletion");
        }

    }
}
#endif