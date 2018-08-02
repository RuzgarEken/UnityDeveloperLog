using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskCreator))]
public class TaskCreator_CustomEditor : Editor {
	TaskCreator taskCreator;

	void Awake(){
		taskCreator = ((TaskCreator)target);
	}

	public override void OnInspectorGUI(){
		base.OnInspectorGUI();

		if(GUILayout.Button("Create Task"))
			taskCreator.CreateTask();
		GUILayout.Space(5);
		if(GUILayout.Button("Clear Task"))
			taskCreator.ClearTask();
	}

}
