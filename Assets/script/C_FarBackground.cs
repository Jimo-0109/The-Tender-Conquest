using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FarBackground : MonoBehaviour {

    float bk_f_x,bk_f_y;
    GameObject camera;
    public float limit_r, limit_l, limit_t, limit_b;
    float camera_r, camera_l, trans_value;
    float  camera_t, camera_b, trans_value_y;
    // Use this for initialization
    void Awake () {
        camera = GameObject.Find("Main Camera");
        //limit_l = 37.4f; limit_r = 64.5f;
        camera_l = 20.4f; camera_r = 89.0f;
        //limit_t = 25.2f; limit_b = 17.6f;
        camera_t = 27.0f; camera_b = 16.0f;
        trans_value = (limit_r - limit_l)/(camera_r - camera_l);
        bk_f_x = limit_l;
        bk_f_y = limit_b;
        trans_value_y = (limit_t - limit_b) / (camera_t - camera_b);
    }
	
	// Update is called once per frame
	void Update () {
        FarBackgroundMove();
	}

    void FarBackgroundMove()
    {
        if (camera.transform.position.x <= camera_l) bk_f_x = limit_l;
        else if (camera.transform.position.x >= camera_r) bk_f_x = limit_r;
        else
        {
                bk_f_x = limit_l + (camera.transform.position.x - camera_l) * trans_value;
        }

        if (camera.transform.position.y <= camera_b) bk_f_y = limit_b;
        else if (camera.transform.position.y >= camera_t) bk_f_y = limit_t;
        else
        {
            if(camera.transform.localScale.y>0)bk_f_y = limit_b + (camera.transform.position.y - camera_b) * trans_value_y;
        }
        transform.position = new Vector3(bk_f_x, bk_f_y, transform.position.z);
    }
}
