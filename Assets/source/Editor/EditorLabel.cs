#if UNITY_EDITOR
	using UnityEngine;
	using UnityEditor;
	using System.Collections;

	public class EditorLabel : MonoBehaviour {
		public string text;
		public Vector3 offset;

		private static GUIStyle style;
		private static GUIStyle Style{
			get{
				if(style == null){
					style = new GUIStyle( EditorStyles.largeLabel );
					style.alignment = TextAnchor.MiddleCenter;
					style.normal.textColor = new Color(0.9f,0.9f,0.9f);
					style.fontSize = 32;
				}
				return style;
			}

		}


		void OnDrawGizmos(){
			float dist = (Camera.current.transform.position - transform.position).magnitude;

			float fontSize = Mathf.Lerp(64, 12, dist/10f);
			
			Style.fontSize = (int)fontSize;

			Vector3 wPos = transform.position - offset;

			Vector3 scPos = Camera.current.WorldToScreenPoint(wPos);
			if(scPos.z <= 0){
				return;
			}

		
			Handles.BeginGUI();


			scPos.y = Screen.height - scPos.y; // Flip Y


			Vector2 strSize = Style.CalcSize(new GUIContent(text));

			Rect rect = new Rect(0f, 0f, strSize.x + 6,strSize.y + 4);
			rect.center = scPos - Vector3.up*rect.height*0.5f;
			GUI.color = new Color(0f,0f,0f,0.8f);
			GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
			GUI.color = Color.white;
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
			GUI.Label(rect, text, Style);
			GUI.color = Color.white;

			Handles.EndGUI();
		}
	}
#endif
