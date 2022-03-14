using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 玩家的攻击行为
/// </summary>
public enum ControllerAttack
{
    controllerWalk,
    normalAttack,
    skillAttack
}

/// <summary>
/// 进入状态里的细节状态
/// </summary>
public enum InAttackState
{
    idle,
    walk,
    attack,
    death
}

public class PlayerAttack : MonoBehaviour
{
    //控制玩家的攻击

    public string anim_normalattack;
    private float animtime_normalattack;
    public ControllerAttack controllerAttack;
    public InAttackState inAttackState;
    private float min_distance;//攻击的最小距离
    private Transform Targettrans;//攻击目标的位置
    public float time_attack;//计时器
    public float time;//间隔
    public  float changetmie;


    public bool IsTarget;//是否瞄准敌人
    SkillInfo skillInfo;//先存储技能信息，为第二步坐准备

    private GameObject MissTrans;  //要创建的文本目标物体
    private GameObject MissGo;     //生成的miss物体
    public GameObject Missprefab;  //miss预制体
    private HUDText MissHUDText;          //生成的显示组件
    private UIFollowTarget followTarget;  //跟随的组件
    Renderer Babyrenderer;
    Color Startcolor;

    public GameObject EffectAttackPrefab;//攻击的特效

    public GameObject  []EffectPrefab;
    private Dictionary<string, GameObject> effectDict = new Dictionary<string,GameObject>();

    private PlayerMove playerMove;
    private Animation anim;
    private PlayerStatus playerStatus;




    private void Awake()
    {
        min_distance = 5f;
        animtime_normalattack = 0.83f;
        controllerAttack = ControllerAttack.controllerWalk;
        Targettrans = null;
        inAttackState = InAttackState.idle;
        time = 0.5f;
        time_attack = 0;
        changetmie = 0;
        IsTarget = false;
        playerMove = this.GetComponent<PlayerMove>();
        anim = this.GetComponent<Animation>();
        playerStatus = this.GetComponent<PlayerStatus>();
        MissTrans = transform.Find("PlayerHUDText").gameObject;
        Babyrenderer = transform.Find("Magician_Body").GetComponent<Renderer>();
        Startcolor = Babyrenderer.material.color;
        foreach(GameObject go in EffectPrefab)
        {
            
            effectDict.Add(go.name,go);
        }
    }

    private void Start()
    {
        MissGo = NGUITools.AddChild(HUDTextParent.instance.gameObject, Missprefab);
        MissHUDText = MissGo.GetComponent<HUDText>();
        followTarget = MissGo.GetComponent<UIFollowTarget>();
        followTarget.target = MissTrans.transform;
    }
    private void Update()
    {
        ControllerAnimAttack();
        if (Input.GetMouseButtonDown(0))
        {
            if (skillInfo == null) return;
            if (skillInfo.effectType == EffectType.SingleTarget)
            {
                SkillAttackTarget();
            }
           else if (skillInfo.effectType == EffectType.MultiTarget)
            {
                SkillAttackMultTarget();
            }
           
        }
        
    }

   

    /// <summary>
    /// 控制攻击动画的切换
    /// </summary>
    private void ControllerAnimAttack()
    {
        if (Input.GetMouseButtonDown(0)&&!IsTarget)//按下鼠标左键
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            bool IsColler =Physics.Raycast(ray,out raycastHit);
            
            if (IsColler && raycastHit.collider.tag == Tag.enemy)//点击到敌人
            {
                
                controllerAttack = ControllerAttack.normalAttack;
                Targettrans = raycastHit.collider.transform;
            }
            else
            {
                controllerAttack = ControllerAttack.controllerWalk;
                Targettrans = null;
            }
        }


        if (controllerAttack == ControllerAttack.normalAttack)//进入一般攻击状态
        {
            if (Targettrans == null) { controllerAttack = ControllerAttack.controllerWalk;return; }
            float distance = Vector3.Magnitude(transform.position-Targettrans.position);

            if (playerStatus.currentHP <= 0) {inAttackState=InAttackState.death; anim.CrossFade("Death"); return; }

            if (distance <= min_distance)  //进入攻击范围
            {
                inAttackState = InAttackState.idle;
                anim.CrossFade("Idle");
                time_attack += Time.deltaTime;
                if (time_attack >= time)
                {
                    transform.LookAt(Targettrans);
                    inAttackState = InAttackState.attack;
                    anim.CrossFade(anim_normalattack);
                    changetmie += Time.deltaTime;
                    if (changetmie > animtime_normalattack)
                    {
                        Instantiate(EffectAttackPrefab,Targettrans.position,Quaternion.identity);
                        if (Targettrans.GetComponent<WolfBaby>() == null)
                        {
                            if (Targettrans.GetComponent<WolfNormal>() == null)
                            {
                                Targettrans.GetComponent<WolfBoss>().Hurt(GetAttackPoint());
                            }else
                                Targettrans.GetComponent<WolfNormal>().Hurt(GetAttackPoint());
                        }
                        else
                        Targettrans.GetComponent<WolfBaby>().Hurt(GetAttackPoint());
                        time_attack = 0;
                        changetmie = 0;
                    }
                
                }
                
            }
           
            else   //走进范围再攻击
            {
                inAttackState = InAttackState.walk;
                anim.CrossFade("Run");
                playerMove.simpleMove(Targettrans.position);
            }
        }
    } 
    /// <summary>
    /// 攻击多少伤害
    /// </summary>
    /// <returns></returns>
    public int GetAttackPoint()
    {
        int allattackpoint;
        allattackpoint=playerStatus.attack + playerStatus.Attack_plus+EquipmentUI.instance.attack_pius;
        return allattackpoint;
    }




    /// <summary>
    /// 显示受伤效果,并更新血条显示
    /// </summary>
    /// <param name="attacked"></param>
     public void HuDTextShow(int attacked)
    {
        StartCoroutine(ShowBabyred());
        MissHUDText.Add("-"+PlayerAttacked(attacked) , Color.red, 1);
      
    }


    /// <summary>
    /// 角色受到的伤害
    /// </summary>
    public int PlayerAttacked(int attacked)
    {
        int trueattacked =(attacked-(playerStatus.defense+playerStatus.defense_plus+EquipmentUI.instance.defense_pius));
        Debug.Log(trueattacked);
        if (trueattacked <= 0) { trueattacked = 1; }
        playerStatus.ReduceHp(trueattacked);
        return trueattacked;
    }


    /// <summary>
    /// 使用技能
    /// </summary>
    public void UseSkill(SkillInfo skillInfo)
    {
        if (playerStatus.hearType == HearType.Magician)
        {
            if (playerStatus.hearType != HearType.Magician) return;
            if (skillInfo.effectType == EffectType.Passive)
            {
                UsePassiveSkill(skillInfo);  return;
            }
            if (skillInfo.effectType == EffectType.Buff)
            {
                UseBuffSkill(skillInfo);
            }
            if (skillInfo.effectType == EffectType.SingleTarget)
            {
                ChooseTarget(skillInfo);
            }
            if (skillInfo.effectType == EffectType.MultiTarget)
            {
                ChooseTarget(skillInfo);
            }
        }




        if (playerStatus.hearType == HearType.Swordman)
        {
            if (playerStatus.hearType != HearType.Swordman) return;
        }

    }


    /// <summary>
    /// 使用增加的技能（指加生命或蓝条）
    /// </summary>
    /// <param name="skillInfo"></param>
    private void UsePassiveSkill(SkillInfo skillInfo)
    {
        if (skillInfo.effectProerty == EffectProerty.HP)
        {
            int mp = 0;
            playerStatus.ChangeMpHp(mp,skillInfo.ProertyValue);
            HeadUI.instance.UpdateShow();
        }
        if (skillInfo.effectProerty == EffectProerty.MP)
        {
            int hp = 0;
            playerStatus.ChangeMpHp(skillInfo.ProertyValue,hp);
            HeadUI.instance.UpdateShow();
        }
        StartCoroutine(EffectPassive(skillInfo));
    }


    /// <summary>
    /// 使用增益技能（指短暂加攻击或防御等）
    /// </summary>
    /// <param name="skillInfo"></param>
    private void UseBuffSkill(SkillInfo skillInfo)
    {
      
        switch (skillInfo.effectProerty)
        {
            case EffectProerty.Attack:
                playerStatus.AllTakeAttack(skillInfo.ProertyValue / 100f);
                break;
            case EffectProerty.Defense:
                playerStatus.AllTakeDefense(skillInfo.ProertyValue/100f);
                break;
            case EffectProerty.AttackSpeed:
                playerStatus.AllTakeAttackSpeed(skillInfo.ProertyValue / 100f);
                ChangeattackSpeed(skillInfo.ProertyValue/100f);
                break;        
        }

        HeadUI.instance.UpdateShow();
        StartCoroutine(EffectPassive(skillInfo));
        StartCoroutine(EffectBuff(skillInfo));
    }


    /// <summary>
    /// 使用技能攻击单个敌人，第一步瞄准敌人
    /// </summary>
    private void  ChooseTarget(SkillInfo skillInfo)
    {
        this.skillInfo = skillInfo;
        IsTarget = true;
        CursorManager.instance.ChangeCursor_lockTarget();
        controllerAttack = ControllerAttack.skillAttack;
        HeadUI.instance.UpdateShow();

    }

    /// <summary>
    ///  使用技能攻击单个敌人，第二步选中并造成伤害
    /// </summary>
    private void SkillAttackTarget()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        bool Iscoller=Physics.Raycast(ray,out raycastHit);
        if (IsTarget&& raycastHit.collider.tag == Tag.enemy)
        {
            
            switch (raycastHit.collider.gameObject.name)
            {
                case "WolfBaby(Clone)":
                    raycastHit.collider.GetComponent<WolfBaby>().Hurt(playerStatus.AllTakeAttack(1)*(int)(skillInfo.ProertyValue/100f));
                    break;
                case "WolfBoss(Clone)":
                    raycastHit.collider.GetComponent<WolfBoss>().Hurt(playerStatus.AllTakeAttack(1) * (int)(skillInfo.ProertyValue / 100f));
                    break;
                case "WolfNormal(Clone)":
                    raycastHit.collider.GetComponent<WolfNormal>().Hurt(playerStatus.AllTakeAttack(1) * (int)(skillInfo.ProertyValue / 100f));
                    break;
            }
            
            StartCoroutine(EffectTarget(skillInfo, raycastHit.collider.transform.position));
            IsTarget = false;
            skillInfo = null;
            CursorManager.instance.ChangeCursor_normal();
        }
        else if(IsTarget)
        {
            CursorManager.instance.ChangeCursor_normal();
            StartCoroutine(EffectTarget(skillInfo,raycastHit.collider.transform.position));
            IsTarget = false;
            skillInfo = null;
        }
        
    }

    /// <summary>
    /// 攻击多个目标
    /// </summary>
    private void SkillAttackMultTarget()
    {
        if (skillInfo.effectType == EffectType.MultiTarget)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            bool Iscoller = Physics.Raycast(ray, out raycastHit,13);
            if (IsTarget &&Iscoller)
            {
                StartCoroutine(EffectMultTarget(skillInfo, raycastHit.collider.transform.position+Vector3.up*0.5f));
                IsTarget = false;
                skillInfo = null;
                CursorManager.instance.ChangeCursor_normal();
            }
            else if(IsTarget)
            {
                CursorManager.instance.ChangeCursor_normal();
                IsTarget = false;
                skillInfo = null;
            }
        }

    }

    /// <summary>
    /// 变回原来属性
    /// </summary>
    /// <param name="skillInfo"></param>
    /// <returns></returns>
    IEnumerator EffectBuff(SkillInfo skillInfo)
    {
        yield return new WaitForSeconds(skillInfo.EffectTime);
        switch (skillInfo.effectProerty)
        {
            case EffectProerty.Attack:
                playerStatus.AllTakeAttack(100f / skillInfo.ProertyValue);
                break;
            case EffectProerty.Defense:
                playerStatus.AllTakeDefense(100f / skillInfo.ProertyValue);
                break;
            case EffectProerty.AttackSpeed:
                playerStatus.AllTakeAttackSpeed(100f / skillInfo.ProertyValue);
                ChangeattackSpeed(100f / skillInfo.ProertyValue);
                break;
        }
    }


    IEnumerator EffectMultTarget(SkillInfo skillInfo, Vector3 position)
    {
        controllerAttack = ControllerAttack.skillAttack;
        Debug.Log(skillInfo.animname);
        anim.CrossFade(skillInfo.animname);
        yield return new WaitForSeconds(skillInfo.animtime);
        controllerAttack = ControllerAttack.controllerWalk;
        GameObject go = null;
        effectDict.TryGetValue(skillInfo.effectname, out go);
        if (go != null)
        {
            GameObject GO=Instantiate(go, position, Quaternion.identity);
            GO.GetComponent<Sword>().attack = (int)(skillInfo.ProertyValue / 100f);
        }
    }


        /// <summary>
        /// 指定位置生成特效
        /// </summary>
        /// <param name="skillInfo"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        IEnumerator EffectTarget(SkillInfo skillInfo,Vector3 position)
    {
            controllerAttack = ControllerAttack.skillAttack;
            Debug.Log(skillInfo.animname);
            anim.CrossFade(skillInfo.animname);
            yield return new WaitForSeconds(skillInfo.animtime);
            controllerAttack = ControllerAttack.controllerWalk;
            GameObject go = null;
            effectDict.TryGetValue(skillInfo.effectname, out go);
            if (go != null)
            {
                Instantiate(go,position, Quaternion.identity);
            }
        
    }

    /// <summary>
    /// 特效的显示
    /// </summary>
    /// <returns></returns>
    IEnumerator EffectPassive(SkillInfo skillInfo)
    {
        controllerAttack = ControllerAttack.skillAttack;
        Debug.Log(skillInfo.animname);
        anim.CrossFade(skillInfo.animname);
        yield return new WaitForSeconds(skillInfo.animtime);
        controllerAttack = ControllerAttack.controllerWalk;
        GameObject go = null;
        effectDict.TryGetValue(skillInfo.effectname,out go);
        if (go!= null){
            Instantiate(go, transform.position,Quaternion.identity);
        }
    }
    


    /// <summary>
    /// 攻速的改变
    /// </summary>
    public void ChangeattackSpeed(float speedrate)
    {
       
        this.time *= speedrate;
        animtime_normalattack *= speedrate;
    }


    public void Animdeath()
    {
        anim.CrossFade("Death");
    }
    IEnumerator ShowBabyred()
    {
        Babyrenderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        Babyrenderer.material.color = Startcolor;

    }
}
