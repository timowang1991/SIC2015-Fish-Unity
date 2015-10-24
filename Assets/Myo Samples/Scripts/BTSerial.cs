using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI.Extensions;
using System;

public class BTSerial : MonoBehaviour {
	SerialPort serial;
	string selectedPortName;

	public GameObject portDropDownListGO;
	DropDownList portDropDownList;

	public GameObject baudDropDownListGO;
	DropDownList baudDropDownList;

	// Use this for initialization
	void Start () {
		portDropDownList = portDropDownListGO.GetComponent<DropDownList>();
		portDropDownList.OnSelectionChanged += OnPortSelect;
		populateDropDownList();

		baudDropDownList = baudDropDownListGO.GetComponent<DropDownList>();
		baudDropDownList.OnSelectionChanged += OnBaudSelect;


	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.A)){
//			Debug.Log("A is pressed");
//			serial.Write("a");
//		}
	}

	void populateDropDownList(){
		List<string> portNames = GetPortNames();
		portDropDownList.Items = new List<DropDownListItem>();

		foreach(string portName in portNames){
			DropDownListItem item = new DropDownListItem(portName);
			portDropDownList.Items.Add(item);
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
//					Debug.Log (dev);
				}
			}
			foreach (string dev in cuFiles) {
				if (dev.StartsWith ("/dev/cu.")){
					serial_ports.Add (dev);
//					Debug.Log (dev);
				}
			}
		}

		return serial_ports;
	}

	void OnPortSelect(int idx){
//		Debug.Log("Port selected : " + portDropDownList.Items[idx].Caption);
		selectedPortName = portDropDownList.Items[idx].Caption;

		try{
			if(serial != null && serial.IsOpen){
				serial.Close();
			}
		}catch(Exception e){
			Debug.Log ("OnPortSelect error : " + e.Message);
		}
	}

	void OnBaudSelect(int idx){
//		Debug.Log("Baud selected : " + baudDropDownList.Items[idx].Caption);
		int baudRate = Int32.Parse(baudDropDownList.Items[idx].Caption);

		try{
			if(serial != null && serial.IsOpen){
				serial.Close();
			}

			serial = new SerialPort(selectedPortName, baudRate);

			serial.DtrEnable = true;
			serial.Handshake = Handshake.None;
			serial.DataReceived += ReceiveDataHandler;
			serial.Open();
		}catch(Exception e){
			Debug.Log("OnBaudSelect error : " + e.Message);
		}
	}

	public void SendData(string data){
		try{
			if(serial != null && serial.IsOpen){
				serial.Write(data);
			}
		}catch(Exception e){
			Debug.Log ("sendData error : " + e.Message);
		}
	}

	void ReceiveDataHandler(object sender, SerialDataReceivedEventArgs e){
		SerialPort port = (SerialPort) sender;
		string indata = port.ReadExisting();
		Debug.Log("Data Received : " + indata);
	}
}
