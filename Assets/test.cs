using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.V)) {
            animator.SetBool("run", true);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            animator.SetBool("run", false);
        }
    }
}
