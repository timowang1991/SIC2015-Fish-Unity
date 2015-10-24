using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProtocolTextScript : MonoBehaviour {
	public GameObject fishControlGO;
	FishControl fishControl;

	public GameObject protocolTextGO;
	Text protocolText;

	// Use this for initialization
	void Start () {
		fishControl = fishControlGO.GetComponent<FishControl>();
		protocolText = protocolTextGO.GetComponent<Text>();

		fishControl.OnProtocolChange += OnProtocolTextChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnProtocolTextChange(FishControlProtocol newProtocol){
		switch(newProtocol){
		case FishControlProtocol.OPEN_MOUTH:
			protocolText.text = "Open Mouth";
			break;
		case FishControlProtocol.CLOSE_MOUTH:
			protocolText.text = "Close Mouth";
			break;
		case FishControlProtocol.LEFT:
			protocolText.text = "Left";
			break;
		case FishControlProtocol.RIGHT:
			protocolText.text = "Right";
			break;
		case FishControlProtocol.FRONT:
			protocolText.text = "Front";
			break;
		case FishControlProtocol.UP:
			protocolText.text = "Up";
			break;
		case FishControlProtocol.DOWN:
			protocolText.text = "Down";
			break;
		default:
			protocolText.text = "None";
			break;
		}
	}
}
