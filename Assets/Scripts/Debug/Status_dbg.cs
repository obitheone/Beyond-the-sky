using UnityEngine;
using System.Collections;

public class Status_dbg : MonoBehaviour {
	private Skills modecamera;
	private string mode_str; 

void OnGUI () {
		// Make a background box

		//int life = TP_Status.Instance.GetVida();
		//GUI.Box(new Rect(10,10,170,90), "Vida: " + life);

		modecamera = TP_Camera.Instance.GetMode();

		switch (modecamera) {
			case Skills.Follow:
								mode_str = "Camera: Follow";
								break;
			case Skills.Libre:
								mode_str = "Camera: Libre";
								break;
			case Skills.Orbit:
								mode_str = "Camera: Orbit";
								break;
			case Skills.Dios:
								mode_str = "Camera: GOD";
								break;
			case Skills.Puntos:
								mode_str = "Camera: Puntos";
								break;
			case Skills.Cinema:
								mode_str = "Camera: Cinematica";
								break;
			case Skills.Targetting:
								mode_str = "Camera: Targetting";
								break;
		}
		GUI.Box(new Rect(20,40,150,20), mode_str);

	}
	
}

	