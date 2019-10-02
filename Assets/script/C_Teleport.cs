using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Teleport : MonoBehaviour {

    public GameObject O_virtualplayer = null; //虛像
    C_Player player;
    bool tele_skill = false;
    GameObject camera, temp;
    Transform spine_ani;
    Animator animator;



    void Start () {
        temp = null;
        player = this.GetComponent<C_Player>();
        camera = GameObject.Find("Main Camera");
        spine_ani = transform.GetChild(0);
        animator = this.gameObject.GetComponent<Animator>();
    }
    void Teleport()
    {
        if (player.direction)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tele_skill = true;
                player.b_use_skill = true;
                if(temp==null) temp = Instantiate(O_virtualplayer, transform.position + new Vector3(8f, 0f, 0), Quaternion.identity);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                spine_ani.gameObject.SetActive(false);
                animator.Play("TeleSide");
                tele_skill = false;
                //transform.position = transform.position + new Vector3(5f, -0.5f, 0);
                player.b_use_skill = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tele_skill = true;
                player.b_use_skill = true;
                if (temp == null) temp = Instantiate(O_virtualplayer, transform.position + new Vector3(-8f, 0f, 0), Quaternion.identity);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                spine_ani.gameObject.SetActive(false);
                animator.Play("TeleSide");
                tele_skill = false;
                //transform.position = transform.position + new Vector3(-8f, -0.5f, 0);
                player.b_use_skill = false;
            }
        }
    }
    void Update () {
        Teleport();
        if (tele_skill) camera.SendMessage("TeleMove");
        if (Input.GetKeyUp(KeyCode.Q)) {
            Destroy(temp);
            temp = null;
        }
    }

    void DoingTele() {
        if (player.direction) transform.position = transform.position + new Vector3(8f, 0f, 0);
        else transform.position = transform.position + new Vector3(-8f, 0f, 0);
        spine_ani.gameObject.SetActive(true);
        camera.SendMessage("AfTele");
    }

}
