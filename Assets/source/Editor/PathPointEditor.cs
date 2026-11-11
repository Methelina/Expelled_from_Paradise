using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathPoint))]
public class PathPointEditor : Editor {
	private bool editMode;
	
	void Start () {
	}
	
	void Update () {
	}
	
	void OnSceneGUI () {
		//  int controlId = GUIUtility.GetControlID(FocusType.Passive);
		
		//  if(Event.current.type == EventType.mouseDown && Event.current.button == 0) {			
		//  	Debug.Log(Event.current.type);
			
			
			
		//  	GUIUtility.hotControl = controlId;
		//  	Event.current.Use();
		//  }
	}
	
	PathPoint AddWP(PathPoint pathpoint) {
		var count = 0;
		
		var pp = pathpoint.parent;
		while(pp != this && pp != null) {
			count++;
			pp = pp.parent;
		}
		
		var go = new GameObject();
		Selection.objects = new GameObject[]{go};
		pathpoint.name = "wp_"+count;
		go.name = "wp_"+(count+1);
		
		var path_point = go.AddComponent<PathPoint>();
		
		go.transform.position = pathpoint.transform.position + Vector3.right;
		go.transform.parent = pathpoint.transform.parent;
		
		path_point.color = pathpoint.color;
		
		Undo.RegisterCreatedObjectUndo(go, "Created waypoint");
		
		return path_point;
	}
	
	override public void OnInspectorGUI () {
		base.OnInspectorGUI();
		Undo.RecordObject(target, "Modify");
	 
		var pathpoint = (PathPoint)target;
		
		GUILayout.Space(20);
		
		AdditionFunctions(pathpoint);
		
		if(pathpoint.connect != null || pathpoint.parent != null) {
			ConnectionFunctions(pathpoint);
		}
		
		//  target.lookAtPoint = EditorGUILayout.Vector3Field ("Look At Point", target.lookAtPoint);
		if (GUI.changed)
			EditorUtility.SetDirty (target);
	}
	
	void AdditionFunctions(PathPoint pathpoint) {
		GUILayout.BeginHorizontal("Box");
		
		if(GUILayout.Button("Add")) 
			AddWP(pathpoint);
		
		if(GUILayout.Button("Add next")) {
			var pp = AddWP(pathpoint);
			pp.parent = pathpoint;
			pathpoint.connect = pp;
		}
		
		if(pathpoint.connect && GUILayout.Button("Add inbetween")) {
			var pp = AddWP(pathpoint);
			
			var next = pathpoint.connect;
			pp.parent = pathpoint;
			pathpoint.connect = pp;
			pp.connect = next;
		}
		
		GUILayout.EndHorizontal();
	}
	
	void ConnectionFunctions(PathPoint pathpoint) {
		GUILayout.BeginHorizontal("Box");
		
		if(pathpoint.connect != null && GUILayout.Button("Unlink connect")) {
			pathpoint.connect = null;
		}
		
		if(pathpoint.parent != null && GUILayout.Button("Unlink parent")) {
			pathpoint.parent = null;
		}
		
		if(pathpoint.parent != null && pathpoint.connect == null && GUILayout.Button("Loop")) {
			var pp = pathpoint.parent;
			while(pp != null && pp != this) {
				if(pp.parent != null)
					pp = pp.parent;
				else
					break;
			}
			
			pathpoint.connect = pp;
			pp.parent = pathpoint;
		}
		
		GUILayout.EndHorizontal();
	}
    
	
}
