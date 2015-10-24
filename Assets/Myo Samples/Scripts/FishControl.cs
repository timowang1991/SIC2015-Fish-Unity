using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class FishControl : MonoBehaviour {


	public GameObject btSerialGO;
	BTSerial btSerial;

	public GameObject myoGO;
	ThalmicMyo thalmicMyo;

	public GameObject jointGO;
	
	FishControlProtocol lastProtocol = FishControlProtocol.NONE;
	public FishControlProtocol LastProtocol{
		get{return lastProtocol;}
	}

	public delegate void ProtocolChangeAction(FishControlProtocol newProtocol);
	public event ProtocolChangeAction OnProtocolChange;

	// Use this for initialization
	void Start () {
		btSerial = btSerialGO.GetComponent<BTSerial>();
		thalmicMyo = myoGO.GetComponent<ThalmicMyo>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleNewPose();
	}

	void HandleNewPose(){
		FishControlProtocol newProtocol = GetNewMappedProtocol();

		if(newProtocol != lastProtocol){
			if (newProtocol != FishControlProtocol.NONE){
				btSerial.SendData( ((int)newProtocol).ToString());
//				Debug.Log("newProtocol.ToString() : " + ((int)newProtocol).ToString());
			}
			lastProtocol = newProtocol;

			if(OnProtocolChange != null){
				OnProtocolChange(newProtocol);
			}
		}
	}

	FishControlProtocol GetNewMappedProtocol(){
		Pose newPose = thalmicMyo.pose;

		switch(newPose){
		case Pose.FingersSpread:
			return FishControlProtocol.OPEN_MOUTH;
		case Pose.Fist:
			return FishControlProtocol.CLOSE_MOUTH;
		case Pose.WaveIn:
			return FishControlProtocol.LEFT;
		case Pose.WaveOut:
			return FishControlProtocol.RIGHT;
		case Pose.Rest:
			return UpDownOrFrontProtocol();
		default:
			break;
		}
		return FishControlProtocol.NONE;
	}


	FishControlProtocol UpDownOrFrontProtocol(){
//		Debug.Log ("jointGO.transform.localEulerAngles.x = " + jointGO.transform.localEulerAngles.x);
		if (jointGO.transform.localEulerAngles.x > 270 && jointGO.transform.localEulerAngles.x < 340){
			return FishControlProtocol.UP;
		} else if (jointGO.transform.localEulerAngles.x > 25 && jointGO.transform.localEulerAngles.x < 90){
			return FishControlProtocol.DOWN;
		} else {
			return FishControlProtocol.FRONT;
		}
	}
}
