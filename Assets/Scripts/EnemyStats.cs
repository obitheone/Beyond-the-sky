using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public int startingHealth = 100; 	//vida inicial
	public int currentHealth;			//vida actual
	public float sinkSpeed = 2.5f; 		//velocidad en que se hunde al morir
	public AudioClip deathClip;			//sonido al morir

	AudioSource enemyAudio;
	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;

	//Animator anim; //animacion de muerte
	//ParticleSystem hitParticles; //efecto al golpear al enemigo.
	
	
	void Awake ()
	{
		//anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		//hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
	}
		
	void Update ()
	{
		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if(isDead)
			return;
		
		enemyAudio.Play ();
		
		currentHealth -= amount;
		
		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play();
		
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;
		
		capsuleCollider.isTrigger = true;
		//anim.SetTrigger ("Dead"); //activamos la animacion de la muerte.
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
	}

	public void StartSinking ()
	{
		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
		Destroy (gameObject, 2f);
	}
}
