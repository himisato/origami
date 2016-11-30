using UnityEngine;
using System.Collections;

public class TuruMove : MonoBehaviour {
	
	[SerializeField]
	public float MoveSpeed = 0.5f;

	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("y", 0.1, "time", MoveSpeed, "easeType", "easeInOutSine", "loopType", "pingPong"));
	}
}