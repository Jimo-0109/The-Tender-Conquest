using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FogMoving : MonoBehaviour {
    public GameObject fog1 ;
    public GameObject fog2 ;
    public Transform fog1_tra;
    public Transform fog2_tra;
    float fog1_speed;
    float fog2_speed;
    // Use this for initialization
    void Start () {
        fog1_speed = 0.07f;
        fog2_speed = 0.05f;
        fog1_tra = fog1.transform;
        fog2_tra = fog2.transform;
    }

	void Update () {
        fog1_tra.position = new Vector3(fog1_tra.position.x + fog1_speed, fog1_tra.position.y, fog1_tra.position.z);
        fog2_tra.position = new Vector3(fog2_tra.position.x + fog2_speed, fog2_tra.position.y, fog2_tra.position.z);
    }
   
}
