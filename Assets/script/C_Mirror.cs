using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Mirror : MonoBehaviour {

    GameObject character;
    Rigidbody2D mirrorbody;
    //Rigidbody2D playerbody;
    C_Player player;

    // Use this for initialization
    void Awake()
    {
        character = GameObject.Find("Player");
        player = character.GetComponent<C_Player>();

        mirrorbody = GetComponent<Rigidbody2D>();
        //        playerbody =character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        transform.position = player.between_cilling_vec3;
    }


    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))//鏡子消失
        {
            Destroy(gameObject, 0f);
        }
    }

}
