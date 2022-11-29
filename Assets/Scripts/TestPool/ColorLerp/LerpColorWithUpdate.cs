using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpColorWithUpdate : MonoBehaviour 
{
	public float lerpDuration = 5; // in seconds
	public Color targetColor = Color.clear; // target color to lerp
	Color tempTargetColor;
	// these are only for debugging (to display time and lerp values)
	public Slider slider;
	public Text textTime;

	Material material; // reference to the object material
	Color originalColor; // take original color (so can later reset to it)
	Color tempOriginalColor;

	float lerpTimer = 0;
	bool isLerping = false;
	bool lerpFinished = false;

	float timeCounter=0;
	bool forward = true;
	bool backward = false;

	void Start () 
	{
		material = GetComponent<Renderer>().material;
		originalColor = material.color; // take original color
		ToggleLerp();
		tempTargetColor = targetColor;
		tempOriginalColor = originalColor;
	}

	// main loop
	void Update () 
	{
		// press space to run/pause lerp
		if (Input.GetKeyDown (KeyCode.Space)) ToggleLerp();

		if (isLerping) UpdateLerp();
	}


	// actual lerp is happening here
	void UpdateLerp()
	{
		lerpTimer += Time.deltaTime/lerpDuration;

		// the actual color lerp and setting material color
		material.color = Color.Lerp (tempOriginalColor, tempTargetColor, lerpTimer);

		// for debugging only, display info
		timeCounter += Time.deltaTime;
		//slider.value = lerpTimer;
		//textTime.text = "Lerp: "+lerpTimer+" ( "+Mathf.Round(lerpTimer*100)+"% ) "+" | Time: "+timeCounter;

		// has lerp reached 1? then its finished
		if (lerpTimer >= 1 && forward) 
		{
			tempOriginalColor = targetColor;
			tempTargetColor = originalColor;
			backward = true;
			forward = false;
			lerpTimer = 0;
			//material.color = Color.Lerp(targetColor, originalColor, lerpTimer);
			//isLerping = false;
			//lerpFinished = true;
		}
		if (lerpTimer >= 1 && backward)
		{
			tempOriginalColor = originalColor;
			tempTargetColor = targetColor;
			backward = false;
			forward = true;
			lerpTimer = 0;
			//material.color = Color.Lerp(targetColor, originalColor, lerpTimer);
			//isLerping = false;
			//lerpFinished = true;
		}
	}

	// call this to toggle lerp running/paused
	void ToggleLerp()
	{
		isLerping = !isLerping;

		// if lerp was finished, reset lerp & material
		if (lerpFinished) 
		{
			ResetLerp();
		}
	}

	// reset lerp values and set original material
	void ResetLerp()
	{
		lerpFinished = false;
		lerpTimer = 0;
		timeCounter = 0;
		ResetMaterial();
	}

	void ResetMaterial()
	{
		material.color = originalColor; // restore original color
	}

}
