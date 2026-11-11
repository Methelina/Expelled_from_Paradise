using UnityEngine;
using System.Collections;
using Pathfinding;
using BehaviourMachine;

[RequireComponent(typeof(Senses))]
public class AIChase : MonoBehaviour {
	public float distance = 5f;
	public float reactDistance = 6f;
	
	Senses senses_;
	
	private float nextShot;
	private float triggerReaction_;
	
	void Start () {
		triggerReaction_ = -1;
		senses_ = GetComponent<Senses>();
	}

    void FixedUpdate () {
		if(senses_.chaseTarget != null) {
			if(triggerReaction_ < 0)
				triggerReaction_ = Time.time + senses_.triggerReaction + Random.Range(-senses_.triggerReactionVariation, senses_.triggerReactionVariation);
			
			GetComponent<AINavigator>().runTo(senses_.chaseTarget.position, distance, reactDistance);
			
			if(Time.time > nextShot) {
				BroadcastMessage("StopFire", SendMessageOptions.DontRequireReceiver);
				nextShot = Time.time + 0.2f;
			}
			
			if(senses_.canSeeTarget && Time.time > triggerReaction_) {
				BroadcastMessage("FireAt", transform.position - transform.up * 10f, SendMessageOptions.DontRequireReceiver);
			}
		} else if(senses_.hearTarget != Vector3.zero) {
			triggerReaction_ = -1;
			GetComponent<AINavigator>().runTo(senses_.hearTarget);
			
			if(Vector3.Distance(transform.position, senses_.hearTarget) < 1f) 
				EnemyLost();
				
		} else if(senses_.seeTarget != Vector3.zero) {
			triggerReaction_ = -1;
			GetComponent<AINavigator>().runTo(senses_.seeTarget);
			
			if(Vector3.Distance(transform.position, senses_.seeTarget) < 1f) 
				EnemyLost();
				
		} else {
			triggerReaction_ = -1;
			EnemyLost();
		}
	}
	
	void EnemyLost() {
		senses_.forget();
		GetComponent<Blackboard>().SendEvent("EnemyLost");
	}
	
}
