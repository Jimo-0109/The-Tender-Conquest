using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_VirtualPlayer : MonoBehaviour {
    Rigidbody2D rigidbodyphy = null;
    GameObject character;

    C_Player player;
    // Use this for initialization
    void Awake()
    {
        character = GameObject.Find("Player");
        player = character.GetComponent<C_Player>();
        rigidbodyphy = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E)) transform.position = player.between_virtuall_vec3;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))//虛像消失
        {
            Destroy(gameObject, 0f);
        }


        if (Input.GetKeyUp(KeyCode.J))
        {
            Destroy(gameObject, 0f);
        }

    }
}
