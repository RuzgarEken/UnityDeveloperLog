using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class TaskRunner : ScriptableObject{
	public Task task;
	
	[ReadOnly] public string currentWorkTime;
	[ReadOnly] public bool working;
	[ReadOnly] public bool taskSelected;

	[HideInInspector] public string developerNote = "";

	public void OnDeleteTask(Task deletedTask){
		if(deletedTask == task)
			ClearWorkArea();
	}
	
	public void SetTask(Task task){
		if(CheckOngoingWork()){
			ClearWorkArea();
			this.task = task;
			taskSelected = true;
		}
	}

	private bool CheckOngoingWork(){
		if(working){
			if(UnityEditor.EditorUtility.DisplayDialog("Warning!", 
				"There is ongoing task. Whould you like to change current task?\n" + 
				"(Current task will generate workTime with current situation)","Ok", "Cancel")
			){
				UpdateDeveloperNote();
				StopWorking();
			}	
			else 
				return false;
		}
		return true;
	}

	WorkTime workTime;
	public void StartWorking(){
		workTime = new WorkTime();
		UnityEditor.EditorApplication.quitting += OnUnityClose;
		currentWorkTime = "";
		working = true;
		UpdateTimer();
	}

	private void OnUnityClose(){
		UpdateDeveloperNote();
		StopWorking();
	}

	public void UpdateTimer(){
		currentWorkTime = System.DateTime.Now.Subtract(DateTime.Parse(workTime.startDate)).ToString();
	}

	public void UpdateDeveloperNote(){
		workTime.developerNote = developerNote;
		developerNote = "";
	}

	public void StopWorking(){
        UnityEditor.EditorApplication.quitting -= OnUnityClose;
		if(workTime!=null){
			UpdateTimer();
			workTime.StopWork();
			task.AddWorkTime(workTime);
			working = false;
			workTime = null;
		}
	}

	bool allChildsCompleted;
	public bool TaskDone(){
		allChildsCompleted = true;
		for(int i=0;i<task.childTasks.Count;i++){
			if(!task.childTasks[i].completed){
				allChildsCompleted = false;
				break;
			}	
		}
		if(allChildsCompleted){
			task.completed = true;
			return true;
		}
		else{
			Debug.Log("Task can not complete. There are uncompleted child tasks!");
			return false;
		}
	}

	public void AttemptToClearWorkArea(){
		if(CheckOngoingWork())
			ClearWorkArea();
	}

	public void ClearWorkArea(){	
		developerNote = "";
		task = null;
		taskSelected = false;
		currentWorkTime = "00:00:00";
		working = false;
	}

	public void PutbackTaskToUndoneList(){
		task.completed = false;
	}

}
