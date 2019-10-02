using UnityEngine;
using System.Collections;

public class C_FloatStone : MonoBehaviour {
    public float speed = 1; //下降速度要依重力調整
    public bool up = false; //碰撞開關
    public bool right = false; //碰撞開關
    public bool touch = false;
    public GameObject stone;
    public GameObject player;
    public Vector3 player_tra;
    public Vector3 stone_tra;
    public float distance;
    float vertical_time; //垂直計時器
    float horizontal_time; //水平計時器
    void Start () {
        stone_tra = stone.transform.position;
    }

	void Update () {
        player_tra = player.transform.position;

        if (up)
        {           
            transform.position = new Vector3(transform.position.x, PingPong(vertical_time * speed, stone_tra.y, 29), transform.position.z);
        }
        else if (right)
        {        
            transform.position = new Vector3(PingPong(horizontal_time * speed, stone_tra.x, 83), transform.position.y, transform.position.z);
        }
       
    }
    float PingPong(float t, float minLength, float maxLength) //讓物體在兩座標間移動
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (player_tra.y < this.transform.position.y && touch)//玩家y座標在石頭y座標上面
        {
            Debug.Log("Stand at UpperSide of stone");
            distance = player_tra.y - this.transform.position.y;
            up = true;
            vertical_time += Time.deltaTime; //垂直計時器
        }
        else if (player_tra.y > this.transform.position.y && touch)
        {
            Debug.Log("Stand at DownSide of stone");
            right = true;
            horizontal_time += Time.deltaTime; //水平計時器
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        up = false;
        right = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        touch = true;
    }
    private void CollisionExit2D(Collider2D collision)
    {
        touch = false;      
    }

}
