using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour {

	public Transform target;
	public float chasingFlytakaSpeed = 5.0f;
	public float chasingOrutakaSpeed = 15.0f;
	public float patrolSpeed = 2.0f;
	public float patrolWaitTime = 1f;	// Tiempo que espera cuando alcanza un punto ruta.
	public float chasingWaitTime =2f;   //tiempo que le sigue cuando ya esta fuera de vision
	public float orutakaWaitTime = 2f;  // tiempo que te va a observar el orutaka
	public Transform[] patrolWayPoints;	// Ruta que sigue el que apatrulla la ciudad
	public int state = 0; // 0 = patrol // 1= chasing
	public float chaseTimer = 0f;	// Tiempo que lleva persiguiendo.
	public int enemytype=0;

	private NavMeshAgent agent;
	private int wayPointIndex = 0;	// indice dle waypoint donde nos encontramos.
	private float patrolTimer = 0f;	// Tiempo que lleva esperando en el waypoint.la m
	private float orutakaTimer = 0f; // tiempo que lleva observandote el orutaka
	private Vector3 lasplayerposition;

	/* enemigos */
	private const int ORUTAKA = 0;
	private const int FLYTAKA = 1;

	/**/

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {

		if (state == 0) {
						Patrolling ();
				}
		else {
			switch (enemytype) {
				case ORUTAKA:
					ChasingOrutaka ();
					break;
				case FLYTAKA: 
					ChasingFlytaka ();
					break;
				default:
					break;
			}
			
		}
	}

	void ChasingFlytaka()
	{
		if (chaseTimer > chasingWaitTime) 
		{	
			state=0;
			patrolTimer = 0;
			agent.destination = patrolWayPoints[wayPointIndex].position;
		}
		else{
			agent.speed = chasingFlytakaSpeed;
			agent.SetDestination (target.position);
			chaseTimer += Time.deltaTime;
		}
	}

	void ChasingOrutaka()
	{

			if (orutakaTimer>orutakaWaitTime) // orutaka te persigue!!
			{
				agent.Resume();
				if (chaseTimer > chasingWaitTime) 
				{	
					Debug.Log ("1");
					state=0;
					patrolTimer = 0;
					orutakaTimer=0f;
					agent.destination = patrolWayPoints[wayPointIndex].position;
				}
				else{
					//Debug.Log (agent.remainingDistance);
					Debug.Log ("2");	
					agent.speed = chasingOrutakaSpeed;
					if (agent.remainingDistance>0) agent.SetDestination (lasplayerposition);
					else {					
						// ya le hemos dado la yoya.. ahora que? decidir comportamiento despues de impactar.
					}
					chaseTimer += Time.deltaTime;
				}
			}
			else //orutaka te observa
			{
				Debug.Log ("3");	
				agent.Stop();
				//mirar hacia el objetivo
				Quaternion rotation = Quaternion.LookRotation(target.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
				lasplayerposition=target.position;	
				orutakaTimer += Time.deltaTime;
			}
	}

	void Patrolling ()
	{
		agent.speed = patrolSpeed;

		if (agent.remainingDistance <= agent.stoppingDistance) {
			patrolTimer += Time.deltaTime;
			if (patrolTimer >= patrolWaitTime) {
				if (wayPointIndex == patrolWayPoints.Length - 1) {wayPointIndex = 0;}
				else { wayPointIndex++;}
				patrolTimer = 0;
			}
		} 
		else {
				patrolTimer = 0;
			}
		agent.destination = patrolWayPoints[wayPointIndex].position;
	}

}
