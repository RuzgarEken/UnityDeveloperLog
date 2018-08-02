using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class WorkTime{
	[ReadOnly] public string startDate;
	[ReadOnly] public string endDate;
	[ReadOnly] public string workTime;
	[Multiline]	public string developerNote;

	public WorkTime(){
		startDate = DateTime.Now.ToString();
	}

	public void StopWork(){
		SetEndTime();
		workTime = CalculateWorkTime().ToString();
	}

	public void SetEndTime(){
		endDate = DateTime.Now.ToString();
	}

	public TimeSpan CalculateWorkTime(){
		return DateTime.Parse(endDate).Subtract(DateTime.Parse(startDate));
	}

}