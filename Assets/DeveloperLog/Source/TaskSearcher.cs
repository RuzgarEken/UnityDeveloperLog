using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSearcher : ScriptableObject {
	[HideInInspector] public TaskManager taskManager;
	public List<Task> tasks;

	[Header("Search")]
	public string searchKey = "";
	public bool searchInUncompletedTasks = true, searchInCompletedTasks = true;
	public bool searchInParents = false, searchInChilds = false;
	public bool searchForAllPriorities = true;
	public Priority priority;
	public bool searchForAllTaskTypes = true;
	public TaskType taskType;

	private List<Task> displayTasks;

	void Awake(){
		Task[] arr = Resources.LoadAll<Task>("TaskEditor/TaskData");
		tasks = new List<Task>();
		for(int i=0;i<arr.Length;i++)
			tasks.Add(arr[i]);
	}

	public void SearchTask(){
		displayTasks = new List<Task>();
		for(int i=0;i<tasks.Count;i++){

			if(searchInCompletedTasks && tasks[i].completed){
				if(searchForAllPriorities || tasks[i].priority == priority){
					if(searchForAllTaskTypes || tasks[i].taskType == taskType){
						if(searchKey == "" || tasks[i].description.Contains(searchKey) || tasks[i].name.Contains(searchKey)){
							if(searchInChilds && tasks[i].parentTask != null){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
							if(searchInParents && tasks[i].childTasks.Count > 0){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
							if(!searchInChilds && !searchInParents){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
						}
					}						
				}
			}

			if(searchInUncompletedTasks && !tasks[i].completed){
				if(searchForAllPriorities || tasks[i].priority == priority){
					if(searchForAllTaskTypes || tasks[i].taskType == taskType){
						if(searchKey == "" || tasks[i].description.Contains(searchKey) || tasks[i].name.Contains(searchKey)){
							if(searchInChilds && tasks[i].parentTask != null){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
							if(searchInParents && tasks[i].childTasks.Count > 0){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
							if(!searchInChilds && !searchInParents){
								if(!displayTasks.Contains(tasks[i]))
									displayTasks.Add(tasks[i]);
							}
						}
					}
				}
			}
		}
		ShowTasks();
		displayTasks = null;
		UnityEditor.AssetDatabase.OpenAsset(taskManager, -1);
	}

	#region Sort Tasks
	public void SortUndoneTasksByCreateTime(){
		
	}

	public void SortAllTaskByEmergency(){

	}
	#endregion

	public void ShowTasks(){
		taskManager.SetTasks(displayTasks);
	}

	string pathToAsset;
	public void RemoveTask(Task task){
		tasks.Remove(task);
		pathToAsset = UnityEditor.AssetDatabase.GetAssetPath(task);
        UnityEditor.AssetDatabase.DeleteAsset(pathToAsset);
	}

}
