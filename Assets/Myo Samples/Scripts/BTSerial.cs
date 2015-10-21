using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class BTSerial : MonoBehaviour {
	SerialPort serial;
	public string[] portNames;

	// Use this for initialization
	void Start () {
		portNames = SerialPort.GetPortNames();
		Debug.Log("portNames : " + portNames);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnValidate(){
		Debug.Log("OnValidate");
		serial = new SerialPort("/dev/tty.HC-06-DevB", 115200);
	}
}
