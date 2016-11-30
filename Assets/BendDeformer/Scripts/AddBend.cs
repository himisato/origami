using UnityEngine;
using System.Collections;

public class AddBend : MonoBehaviour {
	private GameObject bendDeformer;
	// Use this for initialization
	void Start () {
		GameObject g = new GameObject();
		bendDeformer = g;
		bendDeformer.name = "MyBend";
		bendDeformer.AddComponent<Bend>();
		bendDeformer.GetComponent<Bend>().moves = new GameObject[1]{this.gameObject};
		bendDeformer.transform.SetParent(this.transform,false);
		if(bendDeformer.GetComponent<Collider>()==null)
		{
			this.gameObject.AddComponent<BoxCollider>();
			bendDeformer.GetComponent<Bend>().length = gameObject.GetComponent<Collider>().bounds.max.y/gameObject.transform.localScale.y;
			Destroy (this.gameObject.GetComponent<BoxCollider>());
		}
		else
			bendDeformer.GetComponent<Bend>().length = gameObject.GetComponent<Collider>().bounds.max.y/gameObject.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
