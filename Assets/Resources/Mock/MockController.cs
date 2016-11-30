using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MockController : MonoBehaviour {

    [SerializeField]
    GameObject[] m_Targets;

    List<Vector3> m_Positions    = new List<Vector3>();
    List<Quaternion> m_Rotations = new List<Quaternion>();

	// Use this for initialization
	void Start () {
        foreach (GameObject target in m_Targets) {
            m_Positions.Add(target.transform.position);
            m_Rotations.Add(target.transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            int i = 0;
            foreach (GameObject target in m_Targets) {
                Vector3 pos    = m_Positions[i];
                Vector3 newPos = target.transform.position;
                newPos.x = pos.x;
                newPos.y = pos.y;
                newPos.z = pos.z;
                target.transform.position = newPos;

                Quaternion rot    = m_Rotations[i];
                Quaternion newRot = target.transform.rotation;
                newRot.x = rot.x;
                newRot.y = rot.y;
                newRot.z = rot.z;
                newRot.w = rot.w;
                target.transform.rotation = newRot;
                i++;
            }
        }
	}
}
