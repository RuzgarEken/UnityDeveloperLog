using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Priority{
	VeryLow,
	Low,
	Normal,
	High,
	VeryHigh

}

[ExecuteInEditMode]
public class TaskCreator : ScriptableObject {
	[HideInInspector] public TaskSearcher taskSearcher;
	[HideInInspector] public TaskRunner taskRunner;
	[HideInInspector] public TaskManager taskManager;

	[Header("Task Properties")]
	public string taskName;
	[Multiline]
	public string description;
	public TaskType taskType;
	public Priority priority;
	public Task parentTask;

	[HideInInspector] public string editorPath;

	private Task newTask;
	public void CreateTask(){
		newTask = ScriptableObject.CreateInstance<Task>();

        UnityEditor.AssetDatabase.CreateAsset(newTask, editorPath + "/TaskData/" + taskName + ".asset");
		
		EditorUtility.SetDirty(newTask);
		
		newTask.name = taskName;
		newTask.description = description;
		newTask.taskType = taskType;
		newTask.priority = priority;
		newTask.parentTask = parentTask;
		newTask.createTime = System.DateTime.Now.ToString();
		newTask.workTimes = new List<WorkTime>();
		newTask.childTasks = new List<Task>();
		newTask.totalWorkTime = "0";
		newTask.taskRunner = taskRunner;
		newTask.taskManager = taskManager;
		if(parentTask != null)
			parentTask.childTasks.Add(newTask);

		taskSearcher.tasks.Add(newTask);
		ClearTask();
	}

	public void ClearTask(){
		taskName = "";
		description = "";
		taskType = 0;
		priority = 0;
		parentTask = null;
	}
}
