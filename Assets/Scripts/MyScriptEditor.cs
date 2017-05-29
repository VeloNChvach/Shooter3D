using UnityEngine;
using UnityEditor;

namespace Shooter3D.Editor
{
    [CustomEditor(typeof(MyScript))]
    public class MyScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MyScript ms = (MyScript)target;

            if (GUILayout.Button("Создать объекты", EditorStyles.miniButtonMid))
                ms.CreateObj();

            EditorGUILayout.HelpBox("Создание объектов по кнопке", MessageType.Info);
        }
    }
}