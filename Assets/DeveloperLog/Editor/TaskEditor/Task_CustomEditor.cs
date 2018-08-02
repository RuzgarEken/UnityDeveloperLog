using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Task))]
public class Task_CustomEditor : Editor {
	Task task;

	void Awake(){
		task = ((Task)target);
	}


	Task selectedTaskAsNewParent;
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();

		GUILayout.Space(10);
		if(GUILayout.Button("Work"))
			task.OpenInRunner();

		GUILayout.Space(10);
		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Change Parent", GUILayout.Width(100))){
			if(selectedTaskAsNewParent!=null)
				task.SetParent(selectedTaskAsNewParent);
		}
		selectedTaskAsNewParent = EditorGUILayout.ObjectField(selectedTaskAsNewParent, typeof(Task), true) as Task;
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(10);
		if(GUILayout.Button("Delete Task"))
			task.DeleteTask();
	}
	
}
