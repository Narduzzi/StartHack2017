using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAnimation : MonoBehaviour {
	private Light light;
	public Color color;
	public Color next;
	public Color stocked;
	private float t;
	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
		light = this.GetComponent<Light> ();
		color = Color.blue;
		t = 0;
		next = nextColor ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 colorA = new Vector3 (stocked.r, stocked.g, stocked.b);
		Vector3 colorB = new Vector3 (next.r, next.g, next.b);
		Vector3 lerped = Vector3.Lerp (colorA, colorB, t);
		color = new Color(lerped.x, lerped.y, lerped.z);

		t = t + Time.deltaTime * speed;
		//Debug.LogWarning (t);
		if (t > 1) {
			t = 0;
			color = next;
			next = nextColor ();
			stocked = color;
		}
		light.color = color;
	}

	private Color nextColor(){
		float a = Random.Range (0, 1.0f);
		float b = Random.Range (0, 1.0f);
		float c= Random.Range (0, 1.0f);
		//Debug.LogWarning (a + " " + b + " " + c);
		return new Color (a, b, c);
	}
}
