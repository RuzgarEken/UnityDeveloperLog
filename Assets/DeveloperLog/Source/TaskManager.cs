using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TaskManager : ScriptableObject {
	[HideInInspector] public TaskRunner taskRunner;
	[HideInInspector] public TaskSearcher taskSearcher; 

	[HideInInspector] public List<Task> selectedTasks;

	public void SetTasks(List<Task> selectedTasks){
		this.selectedTasks = null;
		this.selectedTasks = selectedTasks;
		
	}	

	public void ViewTask(int index){
        UnityEditor.AssetDatabase.OpenAsset(selectedTasks[index], -1);
	}

	public void WorkOnTask(int index){
		taskRunner.SetTask(selectedTasks[index]);
		UnityEditor.AssetDatabase.OpenAsset(taskRunner, -1);
	}

	Task deletedTask;
	public void DeleteTask(int index){
		if(!UnityEditor.EditorUtility.DisplayDialog("Warning!", 
				"Whould you like to delete this task?"
				,"Yes", "No")
		)
			return;
		deletedTask = selectedTasks[index];
		if(deletedTask.parentTask!=null){
			deletedTask.parentTask.OnChildRemove(deletedTask);
		}
		for(int i=0;i<deletedTask.childTasks.Count;i++){
			deletedTask.childTasks[i].parentTask = null;
		}
		taskRunner.OnDeleteTask(deletedTask);
		selectedTasks.RemoveAt(index);
		taskSearcher.RemoveTask(deletedTask);
	}

	public void DeleteTask(Task deletedTask){
		if(!UnityEditor.EditorUtility.DisplayDialog("Warning!", 
				"Whould you like to delete this task?"
				,"Yes", "No")
		)
			return;
		if(deletedTask.parentTask!=null){
			deletedTask.parentTask.OnChildRemove(deletedTask);
		}
		taskRunner.OnDeleteTask(deletedTask);
		selectedTasks.Remove(deletedTask);
		taskSearcher.RemoveTask(deletedTask);
	}
	
}
