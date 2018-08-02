using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TaskEditor : EditorWindow {
	static TaskEditor instance;
	static string applicationEditorPath;
	static string editorPath = "Assets/Resources/TaskEditor";

	static TaskCreator taskCreator;
	static TaskSearcher taskSearcher;
	static TaskManager taskManager;
	static TaskRunner taskRunner;

	[MenuItem("Window/Task Editor")]
	private static void ShowWindow() {
		Debug.Log("Show");

		if(!System.IO.Directory.Exists(applicationEditorPath)){
			applicationEditorPath = Application.dataPath + "/Resources/TaskEditor";
			System.IO.Directory.CreateDirectory(applicationEditorPath);
			System.IO.Directory.CreateDirectory(applicationEditorPath + "/TaskData");			

			CreateUtils();
		}
		else
			LoadUtils();
		
		SetUtils();

		instance = GetWindow<TaskEditor>();
		instance.minSize = new Vector2(300, 30);
		instance.maxSize = new Vector2(300, 30);
		instance.Show();
	}

	void OnEnable(){
		LoadUtils();
		SetUtils();
	}

	static void CreateUtils(){
		taskCreator = ScriptableObject.CreateInstance(typeof(TaskCreator)) as TaskCreator;
		EditorUtility.SetDirty(taskCreator);
		taskSearcher = ScriptableObject.CreateInstance(typeof(TaskSearcher)) as TaskSearcher;
		EditorUtility.SetDirty(taskSearcher);
		taskManager = ScriptableObject.CreateInstance(typeof(TaskManager)) as TaskManager;
		EditorUtility.SetDirty(taskManager);
		taskRunner = ScriptableObject.CreateInstance(typeof(TaskRunner)) as TaskRunner;
		EditorUtility.SetDirty(taskRunner);

		taskSearcher.tasks = new List<Task>();

		AssetDatabase.CreateAsset(taskCreator, editorPath + "/TaskCreator.asset");
		AssetDatabase.CreateAsset(taskSearcher, editorPath + "/TaskSearcher.asset");
		AssetDatabase.CreateAsset(taskManager, editorPath + "/TaskManager.asset");
		AssetDatabase.CreateAsset(taskRunner, editorPath + "/TaskRunner.asset");
	}

	static void LoadUtils(){
		if(taskCreator==null) taskCreator = AssetDatabase.LoadAssetAtPath(editorPath + "/TaskCreator.asset", typeof(TaskCreator)) as TaskCreator;
		if(taskSearcher==null) taskSearcher = AssetDatabase.LoadAssetAtPath(editorPath + "/TaskSearcher.asset", typeof(TaskSearcher)) as TaskSearcher;
		if(taskManager==null) taskManager = AssetDatabase.LoadAssetAtPath(editorPath + "/TaskManager.asset", typeof(TaskManager)) as TaskManager;
		if(taskRunner==null) taskRunner = AssetDatabase.LoadAssetAtPath(editorPath + "/TaskRunner.asset", typeof(TaskRunner)) as TaskRunner;
	}

	static void SetUtils(){
		taskCreator.taskSearcher = taskSearcher;
		taskCreator.taskRunner = taskRunner;
		taskCreator.taskManager = taskManager;
		taskCreator.editorPath = editorPath;

		taskManager.taskRunner = taskRunner;
		taskManager.taskSearcher = taskSearcher;
		taskManager.selectedTasks = new List<Task>();
		
		taskSearcher.taskManager = taskManager;
	}

	private void OnGUI() {
		GUILayout.BeginHorizontal();	
			if(GUILayout.Button("Creator")){
				AssetDatabase.OpenAsset(taskCreator, -1);
			}
				
			if(GUILayout.Button("Searcher")){
				AssetDatabase.OpenAsset(taskSearcher, -1);
			}
				
			if(GUILayout.Button("Manager")){
				AssetDatabase.OpenAsset(taskManager, -1);
			}
				
			if(GUILayout.Button("Runner")){
				AssetDatabase.OpenAsset(taskRunner, -1);
			}
				
		GUILayout.EndHorizontal();
	}

}
