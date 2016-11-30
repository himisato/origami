using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bend : MonoBehaviour
{
	public float curvature = 0;
	public float length = 1;
	public float amount = 1;
	public bool showGizmo = true;

	public GameObject[] moves;
	List<Mesh> meshes = new List<Mesh>();
	List<Transform> transforms = new List<Transform>();
	List<Vector3[]> origVerts = new List<Vector3[]>();
	List<Vector3[]> verts  = new List<Vector3[]>();

	//5 is the default number of segments in the bend
	//decrease this number to improve performance
	static int numBendSegments = 5;
	Vector3[] origPts = new Vector3[numBendSegments]; 
	Vector3[] pts = new Vector3[numBendSegments];
	Vector3[] normals = new Vector3[numBendSegments];
	Vector3[] binormals = new Vector3[numBendSegments];
	Vector3[] tangents = new Vector3[numBendSegments];

	// Use this for initialization
	void Start ()
	{
		ProjectTargets();
	}
	
	// LateUpdate happens after all Update functions
	void LateUpdate ()
	{

		Deform();
	}
	
	void ProjectTargets()
	{
		meshes.Clear();
		transforms.Clear();
		origVerts.Clear();

			
			for(int i = 0; i < moves.Length; i++)
			{
				meshes.Add(moves[i].GetComponent<MeshFilter>().mesh);
				transforms.Add(moves[i].transform);
				origVerts.Add(meshes[i].vertices);
				

	
			}
	
	}
	void UpdatePts(){
		origVerts.Clear ();
		for(int i=0; i<moves.Length;i++)
		{
			origVerts.Add (meshes[i].vertices);
		}
	}
	
	void Deform()
	{
		Mesh mesh;
		Transform xform;
		Vector3[] restPts;
		Vector3[] pts;
	
		
		for(int m = 0; m < meshes.Count; m++)
		{
			mesh = meshes[m];
			xform = transforms[m];
			restPts = origVerts[m];

			pts = new Vector3[restPts.Length];
			
			for(int i = 0; i < restPts.Length; i++)
			{
				Vector3 wsPt=xform.TransformPoint (restPts[i]);
				//Project pt onto bend line segment to get u param

				float u = PtLineProject(wsPt, transform.TransformPoint(new Vector3(0,0,0)), transform.TransformPoint(new Vector3(0,1*length,0)));
				u = Mathf.Min(1.0F, Mathf.Max(u, 0.0F));

				//Evaluate P, N, BiN, T at u at rest
				//P is the world space projected rest point on the bend
				Vector3 P, N, BiN, T;
				Vector3 bendEnd = xform.TransformPoint (0,length,0);// - xform.TransformPoint (0,0,0);
				float tmp = Vector3.Dot (wsPt,bendEnd);
				tmp = Mathf.Min (Mathf.Max (tmp,0),bendEnd.magnitude);
			
				P = transform.TransformPoint(new Vector3(0,0,0) + u * (new Vector3(0,1*length,0) - new Vector3(0,0,0)));
		

				N = transform.TransformDirection(new Vector3(1,0,0));
				BiN = transform.TransformDirection(new Vector3(0,0,-1));
				T = transform.TransformDirection(new Vector3(0,1,0));
				

				float dN, dBiN, dT;
				dN = PtLineProject(wsPt, P, P+N);
				dBiN = PtLineProject(wsPt, P, P+BiN);
				dT = PtLineProject(wsPt, P, P+T);


				
				//Evaluate posed P', N', BiN', T'
				Vector3 P2, N2, BiN2, T2;
				float x, y, z;

				if( curvature != 0)
				{
					x = (Mathf.Cos(u * curvature/Mathf.PI) * Mathf.PI/curvature - Mathf.PI/curvature) * length;
					y = (Mathf.Sin(u * curvature/Mathf.PI) * Mathf.PI/curvature) * length;
					z = 0;
					P2 = transform.TransformPoint(new Vector3(x,y,z));
					//??
					N2 = transform.TransformDirection(Vector3.Normalize(new Vector3(x,y,z) - new Vector3(-Mathf.PI/curvature*length,0,0)));
					if(curvature < 0)
						N2 *= -1;
					BiN2 = BiN;
					T2 = Vector3.Cross(N2, BiN2);
				}
				else
				{
					P2 = P;
					N2 = N;
					BiN2 = BiN;
					T2 = T;
				}
				
				//Put pt at P'+dN*N', P'+dBiN*BiN', P'+dT*T'
				pts[i] = Vector3.Lerp (restPts[i],xform.InverseTransformPoint(P2 + dN*N2 + dBiN*BiN2 + dT*T2), amount);

			}

			mesh.vertices = pts;
		
			if(xform.GetComponent<MeshCollider>()!=null)
			{
				xform.GetComponent<MeshCollider>().sharedMesh=mesh;
			}
		}
	}
	
	void DrawBend()
	{
		for(int i = 0; i < origPts.Length; i++)
		{
			float u = (float)i/(float)(origPts.Length-1);
			//Puts the bend in the same space as the mesh
			origPts[i] = transform.TransformPoint(new Vector3(0,u*length,0));
			normals[i] = transform.TransformDirection(new Vector3(0,1,0));

		

			float x, y, z;
			Vector3 pt, normal, binormal, tangent;

			if(curvature != 0)
			{
				x = (Mathf.Cos(u * curvature/Mathf.PI) * Mathf.PI/curvature - Mathf.PI/curvature) * length;
				y = (Mathf.Sin(u * curvature/Mathf.PI) * Mathf.PI/curvature) * length;
				z = 0;
				pt = transform.TransformPoint(new Vector3(x,y,z));
				normal = transform.TransformDirection(Vector3.Normalize(new Vector3(x,y,z) - new Vector3(-Mathf.PI/curvature*length,0,0)));
				if(curvature < 0)
				{
					normal *= -1;
				}
			}
			else
			{
				pt = origPts[i];
				normal = transform.TransformDirection(new Vector3(1,0,0));
			}
			binormal = transform.TransformDirection(new Vector3(0,0,-1));
			tangent = Vector3.Cross (normal, binormal);
			
			pts[i] = pt;
			normals[i] = normal;
			binormals[i] = binormal;
			tangents[i] = tangent;
		}

		for(int i = 0; i < pts.Length; i++)
		{
	
			Gizmos.color = Color.red;
			Gizmos.DrawLine (pts[i], pts[i]+.1F*normals[i]*length);
			
			Gizmos.color = Color.green;
			Gizmos.DrawLine (pts[i], pts[i]+.1F*tangents[i]*length);
			
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (pts[i], pts[i]+.1F*binormals[i]*length);
			
			Gizmos.color = new Color(1,1,1,.5F);
			if(i < pts.Length-1)
				Gizmos.DrawLine(pts[i], pts[i+1]);
		}
	}




	float PtLineProject(Vector3 pt, Vector3 start, Vector3 end)
	{
		//start and end point of bend
		float u = 0.0F;
		Vector3 AC = pt - start;
		Vector3 AB = end - start;
		u = Vector3.Dot(AC, AB)/Mathf.Pow(Vector3.Distance(start, end),2.0F);
		return u;
	}
	
	void OnDrawGizmos()
	{
		if(showGizmo)
			DrawBend();
	}
}
