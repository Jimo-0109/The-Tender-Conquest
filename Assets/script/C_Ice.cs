using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Ice : MonoBehaviour {
    GameObject character;
    C_Player player;
    

    void Start () {
        character = GameObject.Find("Player");
        player = character.GetComponent<C_Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        //player.InvokeRepeating("GetHurt", 0.5f , 2.0f);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        player.SendMessage("GetHurt",collider.transform.localScale.x);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.CancelInvoke();
    }

}
