using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Task : ScriptableObject {
	[HideInInspector] public TaskRunner taskRunner;
	[HideInInspector] public TaskManager taskManager;

	public string name;
	[Multiline]	public string description;
	[ReadOnly] public bool completed;
	[ReadOnly] public string totalWorkTime;
	public TaskType taskType;
	public Priority priority;
	[ReadOnly] public string createTime;
	public Task parentTask;
	public List<Task> childTasks;
	[ReadOnly] public List<WorkTime> workTimes;

	public void OnChildRemove(Task child){
		childTasks.Remove(child);
	}

	public void OnChildAdd(Task newChild){
		childTasks.Add(newChild);
	}

	public void AddWorkTime(WorkTime newWorkTime){
		workTimes.Add(newWorkTime);
		totalWorkTime = TimeSpan.Parse(totalWorkTime).Add(TimeSpan.Parse(newWorkTime.workTime)).ToString();
		
		if(parentTask!=null)
			parentTask.totalWorkTime = TimeSpan.Parse(parentTask.totalWorkTime).Add(newWorkTime.CalculateWorkTime()).ToString();
	}

	public void OpenInRunner(){
		taskRunner.SetTask(this);
		UnityEditor.AssetDatabase.OpenAsset(taskRunner, -1);
	}

	public void SetParent(Task newParentTask){
		if(newParentTask.completed){
			Debug.Log("<color=red>Completed task can not be assign as parent." +
			"If you want it to do. Firstly you need to putback parentTask to \"Uncompleted List\"</color>");
			return;
		}
		if(parentTask!=null)
			parentTask.OnChildRemove(this);
		parentTask = newParentTask;
		newParentTask.OnChildAdd(this);
	}

	public void DeleteTask(){
		taskManager.DeleteTask(this);
	}

}
