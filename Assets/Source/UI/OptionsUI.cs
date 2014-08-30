using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

	public Toggle useGamepadToggle;
	private bool useGamepad;

	void Start () {
	
		useGamepad = PlayerPrefs.GetInt ("useGamepad", 0) > 0 ? true : false;
		useGamepadToggle.isOn = useGamepad;

	}
	
	void Update () {
	
		if (useGamepad != useGamepadToggle.isOn) {
			useGamepad = useGamepadToggle.isOn;
			PlayerPrefs.SetInt("useGamepad", useGamepad ? 1 : 0);
		}

	}

}
