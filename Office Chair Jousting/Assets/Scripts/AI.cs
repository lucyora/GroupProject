using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class AI : Player
{
	public enum aiState { MOVETO = 0, ATTACK = 1, DEAD = 2 }; // states whether AI is attacking or moving towards player
	public aiState CurrentState;
	private bool callonce = true;
	int attackType;
	Quaternion qTo;
	public GameObject player;
	Vector3 Target;
	public float AttackDistance = 15.0f; // determines when the AI Will start attacking
	public float speed;
	float timer = 0.0f;
	private NavMeshAgent nav;

	// Use this for initialization
	void Awake()
	{
		//Stops any controller from being set to this player
		Current_Player = current_player.AI;
		// CurrentState = aiState.MOVETO;

		nav = GetComponent<NavMeshAgent>();
		Debug.Log("I AM AWAKE");
		//nav.enabled = true;
		isAlive = true;
	}
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		qTo = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
		if (gameObject.tag == "AI")
		{
			Debug.Log("Is Player AI");
			ChangeState(CurrentState);
		}
		else
		{
			Debug.Log("Ottoman AI");
		}

	}

	public override void UpdatePosition()//Overrides the player UpdatePosition function that handles player input. Raycasting for death is handled in player.cs
	{
		if (player != null)
		{
			if (gameObject.tag == "AI")
			{
				Debug.Log("Is Player AI");
				ChangeState(CurrentState);
			}
			else
			{
				Debug.Log("Ottoman AI");
			}
		}
		else
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

	}
	//moves AI towards the player
	public IEnumerator Moveto()
	{
		//      while (CurrentState == aiState.MOVETO)
		//      {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{

			//rotates Ai towards Player while moving
			transform.LookAt(Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime));
			//moves player forward
			transform.position += transform.forward * speed * Time.deltaTime;
			//if the Ai is close to the player switch to attack mode
			if (Vector3.Distance(transform.position, player.transform.position) <= AttackDistance)
			{
				//switching to attack mode    
				callonce = true;
				ChangeState(aiState.ATTACK);
				yield break;
			}
			if (isAlive == false)
			{
				ChangeState(aiState.DEAD);
			}
			transform.rotation *= Quaternion.Euler(0, -90, 0);
			yield return null;
			//     }
		}
	}
	public IEnumerator Attack()
	{
		//    while (CurrentState == aiState.ATTACK)
		//   {

		//randomizes the way the Ai will attack
		if (callonce == true)
		{
			attackType = Random.Range(0, 3);
			callonce = false;

		}
		attackType = 0;
		Debug.Log(attackType);
		//Attach where AI does constant 360 degree spin
		if (attackType == 0)
		{
			timer += Time.deltaTime;
			//  transform.rotation *= Quaternion.Euler(transform.rotation.x, Random.Range(-180, 180), transform.rotation.z);
			if (timer > 0.5f)
			{ // timer resets at 2, allowing .5 s to do the rotating
				qTo = Quaternion.Euler(new Vector3(transform.rotation.x, Random.Range(90, -90), transform.rotation.z));
				timer = 0;
			}
			transform.rotation = Quaternion.Lerp(transform.rotation, qTo, Time.deltaTime);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		//Attack where AI rotates quickly in 90 degrees
		else if (attackType == 1)
		{
			transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-45, 45), transform.rotation.z);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		// Attack where AI moves towards the player
		else
		{
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		transform.rotation *= Quaternion.Euler(0, -90, 0);
		//if AI if further than than attack distance move towards the player
		if (Vector3.Distance(transform.position, player.transform.position) > AttackDistance)
		{
			ChangeState(aiState.MOVETO);
			yield break;
		}
		if (isAlive == false)
		{
			ChangeState(aiState.DEAD);
		}

		yield return null;
		//   }
	}
	public IEnumerator dead()
	{
		yield return null;
	}

	//switches between states
	public void ChangeState(aiState NewState)
	{
		StopAllCoroutines();
		CurrentState = NewState;

		switch (NewState)
		{
			case aiState.MOVETO:
				StartCoroutine(Moveto());
				break;

			case aiState.ATTACK:
				StartCoroutine(Attack());
				break;
			case aiState.DEAD:
				StartCoroutine(dead());
				break;
		}
	}

}
