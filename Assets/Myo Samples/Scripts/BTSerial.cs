using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI.Extensions;
using System;

public class BTSerial : MonoBehaviour {
	SerialPort serial = new SerialPort("/dev/tty.KPBluetooth20-DevB", 38400);

	public GameObject dropDownListGO;
	DropDownList dropDownList;

	// Use this for initialization
	void Start () {
		dropDownList = dropDownListGO.GetComponent<DropDownList>();
		dropDownList.OnSelectionChanged += OnPortSelect;
		populateDropDownList();


//		try{
//			serial.Open();
//		}catch (UnityException unityException){
//			Debug.Log("UnityException : " + unityException.Message);
//		}

	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.anyKey){
//			Debug.Log("anyKey is pressed");
//			serial.Write("a");
//		}
	}

	void populateDropDownList(){
		List<string> portNames = GetPortNames();
		dropDownList.Items = new List<DropDownListItem>();

		foreach(string portName in portNames){
			DropDownListItem item = new DropDownListItem(portName);
			dropDownList.Items.Add(item);
		}

//		dropDownList.ItemsToDisplay = dropDownList.Items.Count;
	}

	List<string> GetPortNames()
	{
		int p = (int)System.Environment.OSVersion.Platform;
		List<string> serial_ports = new List<string> ();
		
		// Are we on Unix?
		if (p == 4 || p == 128 || p == 6) {
			string[] ttys = Directory.GetFiles ("/dev/", "tty.*");
			string[] cuFiles = Directory.GetFiles("/dev/", "cu.*");
			foreach (string dev in ttys) {
				if (dev.StartsWith ("/dev/tty.")){
					serial_ports.Add (dev);
					Debug.Log (dev);
				}
			}
			foreach (string dev in cuFiles) {
				if (dev.StartsWith ("/dev/cu.")){
					serial_ports.Add (dev);
					Debug.Log (dev);
				}
			}
		}

		return serial_ports;
	}

	void OnPortSelect(int idx){
		Debug.Log("Port selected : " + dropDownList.Items[idx].Caption);
	}
}
