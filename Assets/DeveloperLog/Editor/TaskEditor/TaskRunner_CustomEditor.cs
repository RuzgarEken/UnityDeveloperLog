using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskRunner))]
public class TaskRunner_CustomEditor : UnityEditor.Editor {
	
	TaskRunner taskRunner;
	private void OnEnable(){
		taskRunner = ((TaskRunner)target);
	}

	Vector2 scroll;
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();

		if(!taskRunner.taskSelected)
			return;

		if(taskRunner.task != null){
			if(taskRunner.task.completed)
				GUI.enabled = false;
			else 
				GUI.enabled = !taskRunner.working;
		}
		if(GUILayout.Button("Start Working")){
			taskRunner.StartWorking();
		}
		
		GUI.enabled = taskRunner.task.completed;
		if(taskRunner.task.completed && GUILayout.Button("Put Back To Uncompleted List"))
			taskRunner.PutbackTaskToUndoneList(); 
		GUI.enabled = true;

		//scroll = EditorGUILayout.BeginScrollView(scroll);
		taskRunner.developerNote = EditorGUILayout.TextArea(taskRunner.developerNote, GUILayout.Height(GetTabCount()*15 + 60));
        //EditorGUILayout.EndScrollView();
		
		GUI.enabled = taskRunner.working;
		if(GUILayout.Button("Check Timer"))
			taskRunner.UpdateTimer();
		if(GUILayout.Button("Stop Working")){
			taskRunner.UpdateDeveloperNote();
			taskRunner.StopWorking();
		}
		GUI.enabled = true;

		GUI.enabled = !taskRunner.task.completed && taskRunner.working;
		if(GUILayout.Button("Task Done")){
			if(taskRunner.TaskDone()){
				taskRunner.UpdateDeveloperNote();	
				taskRunner.StopWorking();
			}
		}
		GUI.enabled = true;

		if(GUILayout.Button("Clear Work Area"))
			taskRunner.AttemptToClearWorkArea();
		
		GUILayout.Space(20f);
		GUILayout.Label("Old Notes:", EditorStyles.boldLabel);
		for(int i=0;i<taskRunner.task.workTimes.Count;i++){
			GUILayout.TextArea(taskRunner.task.workTimes[i].developerNote);
		}
	}

	int tabCount;
	public int GetTabCount(){
		tabCount = 0;
		for(int i=0;i<taskRunner.developerNote.Length;i++){
			if(taskRunner.developerNote[i] == '\n')
				tabCount++;
		}
		return tabCount;
	}

}
