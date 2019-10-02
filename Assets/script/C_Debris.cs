using UnityEngine;
using System.Collections;

public class C_Debris : MonoBehaviour {
    float f_time;
    Rigidbody2D body;
	// Use this for initialization
	void Awake () {
        f_time = 0;
        body = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (f_time < 5.0f) f_time += Time.deltaTime;
        else Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "enemy") {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
        if (collider.tag == "floor")
        {
            body.velocity = Vector3.zero;
        }
    }
}
