using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskSearcher))]
public class TaskSearcher_CustomEditor : Editor {
	[HideInInspector] public TaskManager taskManager;
	TaskSearcher taskSearcher;

	void Awake(){
		taskSearcher = ((TaskSearcher)target);
	}

	public override void OnInspectorGUI(){
		base.OnInspectorGUI();

		if(GUILayout.Button("Search Tasks")){
			taskSearcher.SearchTask();
		}
			

	}

}
