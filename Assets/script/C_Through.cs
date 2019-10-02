using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Through : MonoBehaviour {

    public GameObject O_wall;
    void Awake() {
        O_wall = GameObject.Find("demo_center06");
    }
    void ThroughStart() //穿透技能開始
    {
        Debug.Log("Through skill on");
        O_wall.GetComponent<BoxCollider2D>().isTrigger = true;      
    }   
    void ThroughEnd() //穿透技能結束
    {
        Debug.Log("Through skill ending");
        O_wall.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void Update () {

        if (Input.GetKey(KeyCode.G))
        {
            ThroughStart();
            this.Invoke("ThroughEnd", 3.0f); //3秒後關閉技能
        }
        
    }
    
}
