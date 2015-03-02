using UnityEngine;
using System.Collections;

public class ProximityDetector : MonoBehaviour {

	private AgentScript AS;
	// Use this for initialization
	void Start () {
		AS = GetComponent<AgentScript> () as AgentScript;
		AS.target = gameObject.transform;
	}

	void OnTriggerStay(Collider other) {
			
			if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
				AS.chaseTimer = 0;
			}
		}

	void OnTriggerEnter(Collider other) {
			if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
				
			AS.target = other.gameObject.transform;
			AS.state=1; //chasing
			AS.chaseTimer=0;
			}
		}
}
