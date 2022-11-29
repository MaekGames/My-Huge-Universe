using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
	public float speed;
	public float jetpackForce;
	public float rotSpeed;
	public float maxYangle;
	public float terminalVelocity = 45f;
	public float terminalVelocityY = 40f;

	public float MinPitch = -180f;
	public float MaxPitch = 180f;

	public Camera playerCam;
	public ParticleSystem jetpackParticles;
	public bool Sprinting;
	public bool onGround;
	public float MovementInputSmooth; // 1.0f - no movement cuz its so smoothed, 0.0f = full movement; no smooth...//
	public float BoostSpeedSmooth;    //<---Same here//
	public float BoostMult = 20.0f;

	public bool IsUnderwater = false;

	public float MouseSpeedMult = 1.5f;
	public float MouseLerp = 0.5f;

	//private Vector2 MouseMove = new Vector2(0, 0);
	//private Vector2 oldMouseMove = new Vector2(0, 0);

	public bool IsMoving = false;

	public bool CanSprint = true;

	//private float oldBoost = 0;
	//private float currentBoost = 0;

	private Vector2 prevInput = new Vector2(0, 0);
	private Vector2 currentInput = new Vector2(0, 0);

	public bool Jetpack = false;
	public bool CanUseJetpack = true;

	public float gravityConstant = 9.81f;
	public void StartJetpack()
	{

		//Jetpack = Input.GetButton("JetPack");

		if (CanUseJetpack)
		{
			/*if (Input.GetButtonDown("JetPack"))
			{
				//jetpackParticles.Play();
			}
			else if (Input.GetButton("JetPack"))
			{*/
				//Jetpack = true;
				Vector3 r = playerCam.transform.rotation.eulerAngles;
				float y = (135 + r.x) % 360;
				r = new Vector3(0f, (y > 135) ? 90 - (y % 90) : y, -(90 - y));
				GetComponent<Rigidbody>().AddRelativeForce(r.normalized * jetpackForce);
			/*}
			else
			{
				Jetpack = false;
				//jetpackParticles.Stop();
			}*/
		}
		else
		{
			//Jetpack = false;
			//jetpackParticles.Stop();
		}


		// attempt 1
		//if (!onGround && !Jetpack)
		//{
			GetComponent<Rigidbody>().drag = 0.05f;
			Vector3 v = GetComponent<Rigidbody>().velocity;
			v = v.normalized * Mathf.Clamp(v.magnitude, -terminalVelocity, terminalVelocity);
			GetComponent<Rigidbody>().velocity = v;
		//}
		//else
		//{
			//GetComponent<Rigidbody>().drag = 1f;
		//}

		//attempt 2
		/*if (rigidbody.velocity.y < -0.1f) {
			rigidbody.drag = 0f;
			Vector3 v = rigidbody.velocity;
			rigidbody.velocity = new Vector3(v.x, Mathf.Clamp(v.y, -terminalVelocityY, 0), v.z);
		}
		else {
			rigidbody.drag = 1f;
		}*/


		//Debug.Log(rigidbody.velocity);

		/*rigidbody.AddForce(rigidbody.mass*Physics.gravity);
		if (!onGround) {
			float idealDrag = 1 / terminalVelocity;
			rigidbody.drag = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);
		}
		else {
			rigidbody.drag = 1f;
		}*/

		GravityUpdate();
	}

	void GravityUpdate()
	{

		/*if (GravityObject.globalGravityOn)
		{
			GetComponent<Rigidbody>().useGravity = false;

			Vector3 toCenter = GravityObject.mainObject.GetGravityFor(transform.position);
			GetComponent<Rigidbody>().AddForce(-toCenter.normalized * GravityObject.mainObject.gravityForce * gravityConstant);

			Quaternion lookAt = Quaternion.LookRotation(transform.forward, toCenter);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.smoothDeltaTime);
			//transform.rotation = lookAt;
		}*/
	}
}
