using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveSystem save_system = (SaveSystem)target;

        if(GUILayout.Button("Delete Current Save"))
        {
            save_system.Delete();
        }
    }
}
