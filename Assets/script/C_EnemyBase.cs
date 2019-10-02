using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyBase : MonoBehaviour {
    Rigidbody2D enemy_body;
    public LayerMask mask;
    Vector3 respawn_location_vec3;
    Animator enemy_animator;
    Transform t_attackarea, t_detect;
    RaycastHit2D ray_seeplayer,ray_detect;
    GameObject player;
    CircleCollider2D hit_area;
    bool b_toofar,b_attack, b_to_right = false;
    public int i_HP,i_mode;
    float f_distance, f_ramble_left, f_ramble_right, f_ramble_wait, f_face_way,f_atk_blank;
    public float f_ramble_dis, f_speed,f_trace_dis,f_sight_dis,f_player_dis;
    bool b_see_it,b_ramble_return;
    // Use this for initialization
    void Awake()
    {
        enemy_body = gameObject.GetComponent<Rigidbody2D>();
        respawn_location_vec3 = transform.position;
        enemy_animator = gameObject.GetComponent<Animator>();
        t_attackarea = gameObject.transform.GetChild(1);
        t_detect = gameObject.transform.GetChild(2);
        player = GameObject.Find("player");
        hit_area = gameObject.GetComponent<CircleCollider2D>();
        f_ramble_left = respawn_location_vec3.x - f_ramble_dis;
        f_ramble_right = respawn_location_vec3.x + f_ramble_dis;
        b_see_it = b_ramble_return = false;
        f_face_way = transform.localScale.x;
        f_ramble_wait = f_atk_blank =  0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //沒看到玩家進入待機
        if (!seePlay()) {
            behaviorMode();
        }
    }

    //追逐視野內玩家
    bool seePlay()
    {
        //碰到玩家的raycast
        if (!b_see_it) b_see_it = ray_seeplayer = Physics2D.Raycast(transform.position, (new Vector3(-1 * transform.localScale.x, 0, 0) + transform.up * 0.3f),f_sight_dis, mask);
        Debug.DrawLine(transform.position, transform.position + (new Vector3(-1 * transform.localScale.x, 0, 0) + transform.up*0.3f).normalized * f_sight_dis);
        Vector3 walkto_vec3;
        //判斷有無碰到地板
        ray_detect = Physics2D.Linecast(transform.position, t_detect.transform.position, 1 << LayerMask.NameToLayer("ground"));
        f_distance = Mathf.Abs(transform.position.x - respawn_location_vec3.x) ;
        if (f_distance >f_trace_dis) b_toofar = true;
        else b_toofar = false;
        if (b_attack && !Wait(ref f_atk_blank, 2.0f)) return true;
        else {
            f_atk_blank = 0;
            b_attack = false;
        }
        if (b_see_it && ray_detect)
        {
            f_player_dis = Vector2.Distance(transform.position, ray_seeplayer.transform.position);
            if (!b_toofar && f_player_dis<5.0f)
            {
                walkto_vec3 = new Vector3(ray_seeplayer.transform.position.x - transform.position.x, 0, 0);
                transform.localScale = new Vector3(1.0f * Mathf.Sign(transform.position.x - ray_seeplayer.transform.position.x), 1, 1);
                if (Mathf.Abs((ray_seeplayer.transform.position.x - transform.position.x)) < 1.5f)
                {
                    enemy_body.velocity = new Vector3(0, 0, 0);
                    enemy_animator.Play("EnemyAttack");
                    b_attack = true;
                    return (true);
                }
                enemy_body.velocity = new Vector3(walkto_vec3.normalized.x * f_speed*1.4f, enemy_body.velocity.y,0);
                return true;
            }
            else {
                transform.localScale = new Vector3(1.0f *Mathf.Sign(transform.position.x - respawn_location_vec3.x) ,1,1);
                b_see_it = false;
                return false;
            } 
        }
        else {
            b_see_it = false;
            return false;
        }
    }

    void behaviorMode() {
        Vector3 walkto_vec3;
        Vector3 pos_vec3 = transform.position;
        switch (i_mode) {
            case 0:
                if (Mathf.Abs(respawn_location_vec3.x - pos_vec3.x) >0.5f)
                {
                    if (!Wait(ref f_ramble_wait, 1.2f)) {
                        enemy_body.velocity = new Vector3(0, enemy_body.velocity.y, 0);
                        return;
                    } 
                    transform.localScale = new Vector3(Mathf.Sign(pos_vec3.x - respawn_location_vec3.x) ,1,1);
                    walkto_vec3 = new Vector3(f_face_way, 0, 0);
                }
                else {
                    f_ramble_wait = 0.0f;
                    transform.localScale = new Vector3(f_face_way, 1, 1);
                    walkto_vec3 = Vector3.zero;
                } 
                enemy_body.velocity = new Vector3(walkto_vec3.normalized.x * f_speed, enemy_body.velocity.y, 0);
                break;

            case 1:
                if (pos_vec3.x < f_ramble_left)
                {
                    if (!(Wait(ref f_ramble_wait, 1.5f))) {
                        enemy_body.velocity = new Vector3(0,enemy_body.velocity.y,0);
                        return;
                    }
                    b_ramble_return = true;
                    transform.localScale = new Vector3(-1, 1, 1);
                    walkto_vec3 = new Vector3(1 , 0, 0);
                    b_to_right = true;
                }
                else if (pos_vec3.x > f_ramble_right)
                {
                    if ((!Wait(ref f_ramble_wait, 1.5f))) {
                        enemy_body.velocity = new Vector3(0,enemy_body.velocity.y,0);
                        return;
                    }
                    transform.localScale = new Vector3(1, 1, 1);
                    walkto_vec3 = new Vector3(-1, 0, 0);
                    b_to_right = false;
                }
                else {
                    f_ramble_wait = 0.0f;
                    if (b_to_right) {
                        walkto_vec3 = new Vector3(1, 0, 0);
                    }
                    else walkto_vec3 = new Vector3(-1, 0, 0);
                }
                enemy_body.velocity = new Vector3(walkto_vec3.normalized.x * f_speed, enemy_body.velocity.y, 0);
                break;
        }
    }

    bool Wait(ref float f_current_time, float f_total_time ) {
        if ( f_current_time < f_total_time)
        {
            f_current_time += Time.deltaTime;
            //Debug.Log("wait" + f_current_time);
            return false;
        }
        else {
            return true;
        } 
        
    }

    public void Attackarea(){
        hit_area.enabled = true;
        b_attack = true;
    }

    void AttackOver() {
        hit_area.enabled = false;
    }

    public void GetHurt() {
        i_HP--;
    }

    //四邊形面積
    private float multiarea(Vector3 point, Vector3 ver1, Vector3 ver2)
    {
        Vector3 tri1, tri2;
        float area;
        tri1 = ver1 - point;
        tri2 = ver2 - point;
        area = (tri1.x * tri2.y - tri1.y * tri2.x);
        return (area);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.tag == "Player" && b_attack)
        {
            hit_area.enabled = false;
            collision.gameObject.SendMessage("GetHurt",transform.localScale.x);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            b_attack = false;
        }
    }
}
