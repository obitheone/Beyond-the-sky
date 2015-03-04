﻿using UnityEngine;
using System.Collections;

public class Status_Controller_Flytaka : MonoBehaviour {

	private const int ATTACK=0;
	private const int CHASE=1;
	private const int PATROL=2;

	private const int RANGE_ATTACK=0;
	private const int MEELE_ATTACK=1;
	private const int OBSERVE_PLAYER = 2;

	//variables de configuracion del enemigo
	public float range_distance=5; // rango en el que podemos disparar
	public float range_meele=1;	   // rango en el que podemos atacar cuerpo a cuerpo
	public float chase_time;		//tiempo que vamos a perseguirlo

	//variables de evaluacion de la toma de decisiones
	public float player_distace=100f;  //distancia del jugador
	public float chasingTime=0;	//tiempo que hace que lo estamos persiguiendo fuera de vision
	public bool player_view=false;	//vemos al player?

	//variables de salida
	public int state;
	public int attack_type;

	/// <summary>
	/// Las reglas del cambio de estado:
	/// 
	/// Estado por defecto es Patrol
	/// Si el player entra dentro de la distancia de vision // si player_view=true si la distancia es menor que X Attack sino Chase
	/// Si player_view=false y el chasetime <5 lo perseguimos, sino patrol.
	/// 
	/// </summary>

	// Use this for initialization
	void Start () {
		state = 2;
		player_distace = 100;
		chasingTime = 100;
		player_view = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player_view) //vemos al player
				{
			if (player_distace>range_distance) state=CHASE;
					else 
					{
						if (player_distace>range_meele) 
						{
							state=ATTACK;
							attack_type=RANGE_ATTACK;
						}
						else {
							state=ATTACK;
							attack_type=MEELE_ATTACK;
						}
					}
				}
		else //no vemos al player
		{
			if (chasingTime<=chase_time)
				{
					state=CHASE;
				}
			else
			{
				state=PATROL;
			}
		}
	}
}
