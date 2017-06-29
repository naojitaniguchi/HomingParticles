using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSource : MonoBehaviour {
	public float radius ;
	public float intervalTime ;
	public int particleNumMax ;
	public int particleNumInTime ;
	public GameObject particleObject ;
	public GameObject targetObject ;
	int particleCount = 0 ;
	float elapsedTime = 0.0f ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( particleCount > particleNumMax ){
			return ;
		}
		elapsedTime += Time.deltaTime ;
		if ( elapsedTime < intervalTime ){
			return ;
		}
		for ( int i = 0 ; i < particleNumInTime ; i ++ ){
			Vector3 pos = Random.insideUnitSphere ;
			float radiusRandom = Random.Range( 0.5f, 1.0f );

			GameObject generatedObject = Instantiate( particleObject,
				new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z ),
				Quaternion.identity);
			generatedObject.GetComponent<PaticleMover>().targetObject = targetObject ;
			Vector3 targetPos = new Vector3( gameObject.transform.position.x + pos.x * radiusRandom,
				gameObject.transform.position.y + pos.y * radiusRandom, 
				gameObject.transform.position.z + pos.z * radiusRandom);
			generatedObject.GetComponent<PaticleMover>().explosionTargetPos = targetPos ;
			
			particleCount ++;
		}
		elapsedTime = 0.0f ;
		
	}
}
