using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PlayerAniEvent : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void StickHit() {
        Debug.Log("normal hit");
        transform.parent.SendMessage("NormalHitOver");
    }
    void JumpOver() {
        //transform.parent.SendMessage("JumpEnd");
    }
}
