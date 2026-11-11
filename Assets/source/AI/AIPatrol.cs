using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class AIPatrol : MonoBehaviour {
	public PathPoint path;
	
	public bool holdPosition = false;
	
	private Senses senses_;
	
	private PathPoint myCurrentPoint;
	
	void Start () {
		senses_ = GetComponent<Senses>();
	}
	
	void OnEnable() {
		if(path == null) {
			if(holdPosition) {
				var pathpoint = SceneController.NewGameObject("hold_position_point_"+name);
				path = pathpoint.AddComponent<PathPoint>();
				path.transform.position = transform.position;
				pathpoint.BroadcastMessage("NPCArrive", gameObject);
			} else {
				GetComponent<Blackboard>().SendEvent("NoPath");
			}
		}
	}
	
	void Update () {
		if(senses_.seeTarget != Vector3.zero || senses_.hearTarget != Vector3.zero) {
			GetComponent<Blackboard>().SendEvent("EnemyInSight");
		} else {
			if(myCurrentPoint == null) {
				myCurrentPoint = path;
				return;
			}
			
			if(Vector3.Distance(myCurrentPoint.position, transform.position) <= myCurrentPoint.arriveDistance) {
				if(!holdPosition) myCurrentPoint.BroadcastMessage("NPCArrive", gameObject);
				myCurrentPoint = myCurrentPoint.connect;
			} else {
				GetComponent<AINavigator>().runTo(myCurrentPoint.position, 0f, 0f);
			}
		}
	}
}
