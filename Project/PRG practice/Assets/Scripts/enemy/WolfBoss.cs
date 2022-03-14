using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss : MonoBehaviour
{
    //小狼怪物

    Animation anim;
    CharacterController Controller;
    Renderer Babyrenderer;
    public string anim_idle;
    public string anim_walk;

    public string anim_death;
    public anim_baby State;  //将要播放的动画
    //public string anim_now;//现在的动画

    public float Speed;//行走速度

    private int babyexp;//狼的经验
                        //public float Distance;//可以移动的距离

    //public bool IsStartPosition;   //是否回归初始点(初始为否)


    public int hp;
    public bool IsMiss;
    Color Startcolor;


    //public bool IsMove;//是否待机
    //private anim_baby anim_Is;//待机的动画
    public float time;//存储上一次的时间
    public float time_attack;//计时器，用于存储攻击时间的

    private float attacktime = 2f;
    private float attacktimer = 0f;


    public Vector3 Playerpositon;  //玩家的向量
    public float magnitude;//距离初始点的距离
    public float attackSpeed;//攻击速度
    public string anim_attack;
    public float time_animattack;//攻击动画的时间
    public float maxdistance_attck;//最近攻击距离
    public float mindistance_attack;//最远攻击距离
    public int hurt_attack;//攻击的伤害
    private Transform playertrans;//攻击的目标
    private PlayerStatus player;
    private EnemySpawn enemySpawn;

    public int enemyattack=40;
    private float atttime = 2f;
    private float atttimer = 0f;
    private PlayerAttack playerAttack;

    private GameObject MissTrans;  //要创建的目标物体
    private GameObject MissGo;     //生成的miss物体
    public GameObject Missprefab;  //miss预制体
    private HUDText MissHUDText;          //生成的显示组件
    private UIFollowTarget followTarget;  //跟随的组件
    private void Awake()
    {
        time = 0f;
        Speed = 0.7f;
        hurt_attack = 40;
        babyexp = 100;
        hp = 500;
        maxdistance_attck = 8f;
        mindistance_attack = 4f;
        //Distance = 5f;
        //StartPosition = transform.position;
        //IsStartPosition = false;
        //IsMove =true;
        State = anim_baby.idle;
        //anim_Is = anim_baby.idle;
        anim = this.GetComponent<Animation>();
        Controller = this.GetComponent<CharacterController>();
        Babyrenderer = transform.Find("Wolf_Boss").GetComponent<Renderer>();
        Startcolor = Babyrenderer.material.color;
        MissTrans = transform.Find("BuilderMiss").gameObject;

        player = GameObject.FindWithTag(Tag.player).GetComponent<PlayerStatus>();
        playerAttack = GameObject.FindWithTag(Tag.player).GetComponent<PlayerAttack>();
        playertrans = null;/* player.GetComponent<Transform>();*/




    }
    private void Start()
    {
        MissGo = NGUITools.AddChild(HUDTextParent.instance.gameObject, Missprefab);
        MissHUDText = MissGo.GetComponent<HUDText>();
        followTarget = MissGo.GetComponent<UIFollowTarget>();
        followTarget.target = MissTrans.transform;
        //followTarget.uiCamera = UICamera.current.GetComponent<Camera>();
        enemySpawn = this.gameObject.GetComponentInParent<EnemySpawn>();

    }
    private void Update()
    {
        player = GameObject.FindWithTag(Tag.player).GetComponent<PlayerStatus>();


        StateSwitch();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hurt(10);
        }

    }


    private void OnDestroy()
    {

        enemySpawn.countReduce();
        player.ShowUpdateGrade(babyexp);
        NPC_Bar.instance.addKillcount();
        Destroy(MissHUDText.gameObject);


    }


    private void OnMouseEnter()
    {
        CursorManager.instance.ChangeCursor_attack();
    }

    private void OnMouseExit()
    {
        CursorManager.instance.ChangeCursor_normal();
    }




    /// <summary>
    /// 狼的待机动画变换
    /// </summary>
    public void Anim_Is_Baby()
    {
        //if (IsMove==false) return;

        //判断是待机状态，判断是否超出巡逻范围 
        //Vector3 position = transform.position - StartPosition;
        //magnitude = position.magnitude;
        //if (magnitude >= Distance) //超过，往起始点走
        //{
        //     IsMove = true;
        //    IsStartPosition = true;
        //    anim_now =anim_baby.walk;
        //Vector3 po = new Vector3(0, 0, StartPosition.z);
        //    transform.LookAt(StartPosition);
        //}
        //   if (IsStartPosition == true && magnitude <= 0.2f) //往起点走并且距离小于0.2，不用再走向起点
        //   {
        //       IsStartPosition = false;
        //       IsMove = true;
        //   }


        if (Time.time > time)   //随机时间切换待机动画
        {
            float randomtime = Random.Range(1, 3);

            time = Time.time + randomtime;
            if (State == anim_baby.idle)
            {
                State = anim_baby.walk;
                anim.CrossFade(anim_walk);
                transform.Rotate(transform.up * Random.Range(0, 360));


            }
            else
            {
                State = anim_baby.idle;
                anim.CrossFade(anim_idle);
            }
        }


        if (State == anim_baby.walk)
        {
            Controller.SimpleMove(transform.forward * Speed);
        }
    }



    /// <summary>
    /// 狼的攻击
    /// </summary>
    public void Baby_attack()
    {
        if (playertrans != null)
        {
            Playerpositon = player.transform.position;
            Vector3 position = transform.position - Playerpositon;
            magnitude = position.magnitude;
            if (magnitude >= maxdistance_attck) //超过最远距离，直接变为巡逻
            {
                atttimer = 0f;
                playertrans = null;
                State = anim_baby.idle;

            }
            else if (magnitude < mindistance_attack)//小于最小距离，直接攻击
            {
                anim.CrossFade(anim_attack);
                transform.LookAt(playertrans);
                atttimer += Time.deltaTime;
                if (atttimer >= atttime)
                {
                    atttimer = 0f;
                    anim.CrossFade(anim_idle);
                    playerAttack.HuDTextShow(enemyattack);
                    //player.ReduceHp(hurt_attack);
                }
            }
            else //再行走一段距离在攻击
            {
                atttimer = 0f;
                transform.LookAt(playertrans);
                Controller.SimpleMove(transform.forward * Speed);
                anim.CrossFade(anim_walk);
            }
        }
        else
        {
            State = anim_baby.idle;
        }
    }

    /// <summary>
    /// 狼的状态
    /// </summary>
    public void StateSwitch()

    {
        Playerpositon = player.transform.position;
        Vector3 position = transform.position - Playerpositon;
        magnitude = position.magnitude;
        if (magnitude <= maxdistance_attck) //判断是否获得玩家的位置信息
        {
            playertrans = player.GetComponent<Transform>();
            State = anim_baby.attack;
           
        }



        if (State == anim_baby.death)  //状态为死亡
        {
            anim.CrossFade(anim_death);
        }
        else if (State == anim_baby.attack)//状态为攻击
        {

            Baby_attack();
        }
        else  //巡逻
        {
            Anim_Is_Baby();
        }





    }

    /// <summary>
    /// 造成伤害
    /// </summary>
    public void Attack()
    {
        attacktimer += Time.deltaTime;
        if (attacktimer >= attacktime)
        {
            attacktimer = 0;
            playerAttack.HuDTextShow(enemyattack);
        }
    }



    /// <summary>
    /// 受到攻击
    /// </summary>
    /// <param name="attack"></param>
    public void Hurt(int attack)
    {
        int miss = Random.Range(1, 101);
        if (miss >= 80)
        {
            IsMiss = true;
        }
        else IsMiss = false;

        if (!IsMiss)   //被玩家攻击到
        {
            hp -= attack;
            StartCoroutine(ShowBabyred());
            MissHUDText.Add("-" + attack.ToString(), Color.red, 1);

            //ShowBabyred();
            if (hp <= 0)
            {
                State = anim_baby.death;
                Destroy(this.gameObject, 0.5f);
            }
        }
        else MissHUDText.Add("Miss", Color.gray, 1);

    }

    IEnumerator ShowBabyred()
    {
        Babyrenderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        Babyrenderer.material.color = Startcolor;

    }




}
