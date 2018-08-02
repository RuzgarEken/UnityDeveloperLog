using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskManager))]
public class TaskManager_CustomEditor : Editor {
	TaskManager taskManager;

	void OnEnable(){
		taskManager = ((TaskManager)target);
	}

	public override void OnInspectorGUI(){
		base.OnInspectorGUI();

		GUILayout.Space(30f);

		if(taskManager.selectedTasks.Count>0){
			for(int i=0;i<taskManager.selectedTasks.Count;i++){
				GUILayout.BeginHorizontal();	
				if(GUILayout.Button("Work", GUILayout.Width(40)))
					taskManager.WorkOnTask(i);
				if(GUILayout.Button("View", GUILayout.Width(40)))
					taskManager.ViewTask(i);
				GUILayout.Label(taskManager.selectedTasks[i].name);
				if(GUILayout.Button("X", GUILayout.Width(20)))
					taskManager.DeleteTask(i);
				GUILayout.EndHorizontal();
			}
		}
		else
			GUILayout.Label("There is no task with current search conditions.", EditorStyles.boldLabel);

		
	}

}
