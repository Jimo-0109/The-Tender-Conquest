using UnityEngine;
using System.Collections;

public class C_CameraFollow : MonoBehaviour {
    //視窗內人物移動邊界變數
    private Transform right_border, left_border, right_limit, left_limit = null;
    private float BtwTop, BtwBottom, btwfront, btwback, f_trans_time;
    private bool TouchTop, TouchDown, y_axis_change,_b_right,_b_left,_b_camera_fixed;

    //攝影機移動範圍
    private Vector3 range_x,range_y;

    //角色變數
    private Transform target;
    SpriteRenderer sp;
    private C_Player playerclass;
    private Vector3 playertop, playerbottom, FixedPosition, tele_move_vec3;
    bool b_static, b_start_reset;
    Vector3 static_fixed,final_pos; //固定螢幕時讓攝影機慢慢到目的地的變數


    // Use this for initialization
    void Awake()
    {
        
        target = GameObject.Find("Player").transform;
        right_border = this.gameObject.transform.GetChild(0);
        left_border = this.gameObject.transform.GetChild(1);
        right_limit = this.gameObject.transform.GetChild(2);
        left_limit = this.gameObject.transform.GetChild(3);
        range_x = new Vector3(20.4f, 89.0f, 0.0f);
        range_y = new Vector3(16.0f, 27.0f, 0.0f);
        playerclass = target.GetComponent<C_Player>();
        sp = target.transform.GetChild(2).GetComponent<SpriteRenderer>();
        playerbottom = sp.bounds.min;
        playertop = sp.bounds.max;
        FixedPosition = transform.position;
        //紀錄自定義範圍和攝影機範圍的向量差
        BtwTop = transform.position.y - right_border.position.y;
        BtwBottom = transform.position.y - left_border.position.y;
        btwfront = transform.position.x - right_border.position.x;
        btwback = transform.position.x - left_border.position.x;
        TouchTop = TouchDown = y_axis_change = true;
        _b_left = _b_right  = _b_camera_fixed= false;
        b_static = b_start_reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        //抓取角色圖片座標
        playerbottom = sp.bounds.min;
        playertop = sp.bounds.max;
        //transform.position.x * 0.9f + target.transform.position.x * 0.1f

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(playertop.x  + "\n" + right_border.position.x);
            Debug.Log(transform.position);
             }

        //跟隨玩家或固定
        if (!b_static)
        {
            if (!Input.GetKey(KeyCode.Q)) FollowPlayer2();
        }
        else {
            if (b_start_reset) {
                reset();
                return;
            } 
            if (Vector3.Distance(final_pos, transform.position) > 1.0f) transform.position += static_fixed * Time.deltaTime * 15.0f;
            else {
                static_fixed = (new Vector3(target.position.x,target.position.y+3.5f,0) - transform.position).normalized;
                this.Invoke("ChMod", f_trans_time);
                Debug.Log(f_trans_time);
            }
        }
      
        //限制視窗可移動範圍
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, range_x.x, range_x.y), Mathf.Clamp(transform.position.y, range_y.x, range_y.y), transform.position.z);
    }

    //固定螢幕在特定位置
    public void SetScreen(Vector3 pos, float temp_time) {
        b_static = true;
        final_pos = pos;
        static_fixed = (pos - transform.position).normalized;
        f_trans_time = temp_time;
    }

    //RESET
    void reset()
    {
        if (Vector3.Distance(new Vector3(target.position.x, target.position.y + 3.5f, 0.0f), transform.position) > 1.0f)
        {
            transform.position += static_fixed * Time.deltaTime * 15.0f;
        }
        else {
            b_static = false;
            b_start_reset = false;
        } 
        
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
    void ChMod() {
        b_start_reset = true;
    }
    //Q鍵瞬移完的校正
    void AfTele() {
        if (target.localScale.x > 0)
            {
            transform.position = new Vector3(target.transform.position.x+0.3f + (transform.position.x - left_limit.position.x), transform.position.y, transform.position.z);
        }
        else
            {
            transform.position = new Vector3(target.transform.position.x - 0.3f + (transform.position.x - right_limit.position.x), transform.position.y, transform.position.z);
        }

        Debug.Log("player" + playertop.x);
    }

    //跟隨玩家
    void FollowPlayer1()
    {
        if (!playerclass.b_isground)  //玩家離地
        {
            //玩家y軸超出範圍
            if (playertop.y > right_border.position.y && transform.position.y < playertop.y + BtwTop)
            {
                //讓攝影機移到目前玩家位置加一開始紀錄的向量差
                transform.position = new Vector3(transform.position.x, playertop.y + BtwTop, transform.position.z);
                y_axis_change = true;  //往上超出邊界
            }
            else if (playerbottom.y < left_border.position.y && transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, playerbottom.y + BtwBottom, transform.position.z);
                y_axis_change = true;  //往下超出邊界
            }
        }
        else //玩家在地板上，讓攝影機回到玩家身上
        {
            Vector3 back_player;
            //正向時
            if (Mathf.Abs(target.transform.position.y + 2.1f - this.transform.position.y) > 0.1f && !playerclass.b_upside)
            {
                //玩家與攝影機的向量差並單位化
                back_player = (target.transform.position + new Vector3(0, 1.8f, 0) - this.transform.position).normalized;
                //慢慢讓攝影機加到位置
                this.transform.position += new Vector3(0, back_player.y * 0.5f, 0);
            }
            //倒向時
            else if (Mathf.Abs(target.transform.position.y - 2.1f - this.transform.position.y) > 0.3f && playerclass.b_upside)
            {
                back_player = (target.transform.position + new Vector3(0, -2.1f, 0) - this.transform.position).normalized;
                this.transform.position += new Vector3(0, back_player.y * 0.5f, 0);
            }
        }



        //超出左右邊界
        if (playertop.x > right_border.position.x && target.transform.localScale.x >0)
        {
            transform.position = new Vector3(playertop.x + btwfront, transform.position.y, transform.position.z);
        }
        else if (playerbottom.x < left_border.position.x && target.transform.localScale.x <0)
        {
            transform.position = new Vector3(playerbottom.x + btwback, transform.position.y, transform.position.z);
        }
    }

    void FollowPlayer2()
    {
        if (!playerclass.b_isground)  //玩家離地
        {
            //玩家y軸超出範圍
            if (playertop.y > right_border.position.y && transform.position.y < playertop.y + BtwTop) //前面是判斷人物去推邊界，後面是不要讓超過邊界往回調也跟著移動
            {
                //讓攝影機移到目前玩家位置加一開始紀錄的向量差
                transform.position = new Vector3(transform.position.x, playertop.y + BtwTop, transform.position.z);
                y_axis_change = true;  //往上超出邊界
            }
            else if (playerbottom.y < left_border.position.y && transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, playerbottom.y + BtwBottom, transform.position.z);
                y_axis_change = true;  //往下超出邊界
            }
        }
        else //玩家在地板上，讓攝影機回到玩家身上
        {
            Vector3 back_player;
            //正向時
            if (Mathf.Abs(target.transform.position.y + 2.7f - this.transform.position.y) > 0.1f && !playerclass.b_upside)
            {
                //玩家與攝影機的向量差並單位化
                back_player = (target.transform.position + new Vector3(0, 3.5f, 0) - this.transform.position).normalized;
                //慢慢讓攝影機加到位置
                this.transform.position += new Vector3(0, back_player.y * 0.5f, 0);
            }
            //倒向時
            else if (Mathf.Abs(target.transform.position.y - 2.1f - this.transform.position.y) > 0.3f && playerclass.b_upside)
            {
                back_player = (target.transform.position + new Vector3(0, -2.1f, 0) - this.transform.position).normalized;
                this.transform.position += new Vector3(0, back_player.y * 0.5f, 0);
            }
        }

        //左右
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (!_b_camera_fixed) _b_camera_fixed = true;
        }
        else {
            _b_left = false;
            _b_right = false;
            _b_camera_fixed = false;
        }

        Vector3 _VBt;
        float camer_speed = 0.1f;
        if (_b_camera_fixed)
        {
            camer_speed = 0.1f;
        }
        else camer_speed = 0.0f;

        //超出左右邊界
        if (playertop.x > left_limit.position.x && target.transform.localScale.x > 0)
            {
            if (_b_right)
                {
                transform.position = new Vector3(playertop.x + (transform.position.x - left_limit.position.x), transform.position.y, transform.position.z);
            }
            else
             {
                _VBt = new Vector3(playertop.x - left_limit.position.x, 0, 0).normalized;
                transform.position += _VBt * camer_speed;
              } 
            }
        else if (playertop.x <= left_limit.position.x && target.transform.localScale.x > 0) {
                _b_camera_fixed = false;
                _b_right = true;
            }

            if (playerbottom.x < right_limit.position.x && target.transform.localScale.x < 0)
            {
             if (_b_left)
                {
                transform.position = new Vector3(playerbottom.x + (transform.position.x - right_limit.position.x), transform.position.y, transform.position.z);
                }
            else {
                _VBt = new Vector3(playerbottom.x - right_limit.position.x, 0, 0).normalized;
                transform.position += _VBt * camer_speed;
                }
        }
            else if (playerbottom.x >= right_limit.position.x && target.transform.localScale.x < 0) {
                _b_camera_fixed = true;
                _b_left = true;
            }
        

    }


    public void TeleMove() {
        tele_move_vec3 = new Vector3(target.transform.position.x+ (Mathf.Sign(target.transform.localScale.x) * 2.5f), transform.position.y, transform.position.z);
        Vector3 btw_tele = (tele_move_vec3 - transform.position).normalized*0.5f;
        if (Vector3.Distance(transform.position, tele_move_vec3) > 1.0f) {
            transform.position += btw_tele;
        }
    }
    public void ResetPos() {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y,transform.position.z) ;
    }
}
