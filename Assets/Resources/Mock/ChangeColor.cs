using UnityEngine;
using System.Collections;
/*
public class ChangeColor : MonoBehaviour {

    [SerializeField]
    Color[] m_Colors;

    [SerializeField]
    GameObject m_Target;
    
    GameObject FindLeapHand(GameObject obj) {
        if (!obj.GetComponent<RigidHand>()) {
            return FindLeapHand(obj.transform.parent.gameObject);
        }

        return obj;
    }
    
    Material m_Material;

    void Start() {
        m_Material = m_Target.GetComponent<Renderer>().material;
    }

    // 選択されているカラーのindex
    int m_ColorIndex = 0;
    void Change() {
        m_ColorIndex++;

        if (m_ColorIndex >= m_Colors.Length) {
            m_ColorIndex = 0;
        }

        m_Material.color = m_Colors[m_ColorIndex];
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "leaphand") {
            return;
        }

        GameObject handObj = FindLeapHand(other.gameObject);

        if (!handObj) {
            return;
        }

        RigidHand hand = handObj.GetComponent<RigidHand>();
        if (hand.Handedness != Chirality.Left) {
            return;
        }

        Change();
    }
}
*/