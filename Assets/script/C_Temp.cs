using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void change(Vector3 pos) {
        transform.position = pos;
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}
