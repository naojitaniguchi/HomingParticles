using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleMover : MonoBehaviour {
	public Vector3 explosionTargetPos ;
	public float startSpeed ;
	public float homingAccel ;
	public float explotionRate ;
	public float homingLimit ;
	public GameObject targetObject ;

	enum MovingMode{
		EXPLOTION,
		EXPLOTION_HOMING,
		HOMING,
	};

	MovingMode mode = MovingMode.EXPLOTION ;
	float explosionTime ;
	float timeFromExplotion = 0.0f ;
	Vector3 homingDir ;
	Vector3 explotionDir ;
	float homingSpeed ;

	// Use this for initialization
	void Start () {
		Vector3 diff ;
		diff = explosionTargetPos - gameObject.transform.position ;
		explosionTime = diff.magnitude / startSpeed ;

		// Debug.Log( explosionTime );

		explotionDir = explosionTargetPos - gameObject.transform.position ;
		explotionDir.Normalize();
		homingSpeed = 0.0f ;
	}

	void updateExplosion(){
		float deltaMove = startSpeed * Time.deltaTime ;
		transform.position = new Vector3( transform.position.x + explotionDir.x * deltaMove,
			transform.position.y + explotionDir.y * deltaMove,
			transform.position.z + explotionDir.z * deltaMove);
	}

	void updateExplosionHoming(){
		homingSpeed += homingAccel ;
		float homingSpeedRate = ( ( timeFromExplotion / explosionTime ) - explotionRate ) / ( 1.0f - explotionRate );
		float explotsionSpeedRate = 1.0f - homingSpeedRate ;

		//Debug.Log( explotsionSpeedRate );
		//Debug.Log( homingSpeedRate );

		float explotionSpeedNow = startSpeed * explotsionSpeedRate * Time.deltaTime;
		float homingSpeedNow = homingSpeed * homingSpeedRate * Time.deltaTime;
		transform.position = new Vector3( transform.position.x + explotionDir.x * explotionSpeedNow + homingDir.x * homingSpeedNow,
			transform.position.y + explotionDir.y * explotionSpeedNow + homingDir.z * homingSpeedNow,
			transform.position.z + explotionDir.z * explotionSpeedNow + homingDir.z * homingSpeedNow);
	}

	void updateHoming(){
		homingSpeed += homingAccel ;
		if ( homingDir.magnitude < homingLimit ){
			Destroy( this.gameObject );
		}
		float deltaMove = homingSpeed * Time.deltaTime ;
		transform.position = new Vector3( transform.position.x + homingDir.x * deltaMove,
			transform.position.y + homingDir.y * deltaMove,
			transform.position.z + homingDir.z * deltaMove);
		
	}

	// Update is called once per frame
	void Update () {
		if ( targetObject == null ){
			return ;
		}
		homingDir = targetObject.transform.position - gameObject.transform.position ;

		switch ( mode ){
		case MovingMode.EXPLOTION:
			updateExplosion();
			break;
		case MovingMode.EXPLOTION_HOMING:
			updateExplosionHoming();
			break;
		case MovingMode.HOMING:
			updateHoming();
			break;
		}

		//Debug.Log( mode );
		//Debug.Log( gameObject.transform.position );
		// Debug.Log( homingDir.magnitude );

		timeFromExplotion += Time.deltaTime ;
		if ( mode == MovingMode.EXPLOTION && timeFromExplotion > explosionTime * explotionRate ){
			mode = MovingMode.EXPLOTION_HOMING ;
		}
		if ( mode == MovingMode.EXPLOTION_HOMING && timeFromExplotion > explosionTime ){
			mode = MovingMode.HOMING ;
		}

	}
}
