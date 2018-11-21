using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class SaveDataCSV : MonoBehaviour 
{
	public CalculateBackAngle ba;
	List<string[]> rowData = new List<string[]>();

	// Use this for initialization
	void Start () 
	{
		ba = GetComponent<CalculateBackAngle>();
		
		string filePath = GetPath();

		if(File.Exists(filePath) == false)
		{
		InitFile();
		}
	}

	void Update ()
	{
		if(Input.GetKeyUp("space")) Save();

		if(Input.GetKeyUp("n")){
			string[] foo = new string[2];
			foo[0] = "rotangle";
			foo[1] = ba.rotationDifferenceEuler.y.ToString();
			rowData.Add(foo);
		}

	}
	
	void InitFile()
	{
		string[] tempRowData = new string[2];
		tempRowData[0] = "Field1";
		tempRowData[1] = "Field2";
		rowData.Add(tempRowData);
	}

	void Save()
	{
		for(int i = 0; i<5; i++)
		{
			string[] tempRowData = new string[2];
			tempRowData[0] = "0"+i;
			tempRowData[1] = UnityEngine.Random.Range(1,100).ToString();
			rowData.Add(tempRowData);
		}
		string[][] output = new string[rowData.Count][];
		for(int i = 0; i<output.Length; i++)
		{
			output[i] = rowData[i];
		}
		int length = output.GetLength(0);
		string delimiter = ",";

		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < length; i++)
		{
			sb.AppendLine(string.Join(delimiter,output[i]));
		}
		
		string fliePath = GetPath();

		StreamWriter outStream = System.IO.File.AppendText(fliePath);
		outStream.WriteLine(sb);
		outStream.Close();
		rowData.Clear();
	}


	string GetPath()
	{
		DateTime today = DateTime.Now;
		string date = today.Day + "-" + today.Month + "-" + today.Year;
		return Application.dataPath + "/SavedResults/" + "result_" + date + ".csv";
	}
	
	string GetPath(int id)
	{
		DateTime today = DateTime.Now;
		string date = today.Day + "-" + today.Month + "-" + today.Year;
		return Application.dataPath + "/SavedResults/" + "result_" + date + "_" + id + ".csv";
	}
}
