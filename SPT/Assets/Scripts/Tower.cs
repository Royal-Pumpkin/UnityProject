using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    //타워 작동상태
    enum eTowerState {NULL=-1,IDLE,ATTACK,TOWERCONTROL}
    

    eTowerState mTowerState;
    public BuildManager.eTowerType mTowerType;

    //타워 구성요소
    public Transform Tr_InTowerCamera;
    public Transform Head;
    public Collider mCollider;

    //스틱컴포넌트
    FixedJoystick teststick;

    //나중에 각각 타워의 스텟이 나오면 바꿔줄것
    public int nAtk = 10;
    public float fSerchDistance = 20f;
    public float fFireCoolDown = 0f;
    public float fFireRate = 0.5f;
    public int nPrice;
    public float fAtkArea = 5f;

    public float fCameraSpeed = 5f;

    public GameObject FindEnemyobj = null;
    Enemy FindEnemy = null;

    float CurAngleX;
    float CurAngleY;
    float CurAngleZ;

    //타워기본행동
    public void DefaultTowerAct()
    {
        

        if (mTowerState != eTowerState.TOWERCONTROL)
        {
            FindShootEnemy(GameManager.stGameManager.GetEnemyList());

            return;
        }


        ControlMode(GameManager.stGameManager.GetEnemyList());
    }

    //타워 카메라 이동
    public void TowerGetin(Camera _MainCamera)
    {
        //이벤트 메니저 TowerPick 함수에서 예외처리해서 해결했는데 예외처리 안했을 때 컨트롤하고있는 타워를 한번 더 클릭할 시 카메라 각도가 이상하게 입력됨
        //원인 예상: 정해진 transform과 함수에서 설정하는 값변화 에서 생기는 문제같음
        mCollider.enabled = false;
        _MainCamera.orthographic = false;
        _MainCamera.transform.position = Tr_InTowerCamera.position;
        _MainCamera.transform.rotation = Tr_InTowerCamera.rotation;
        GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.TOWER);
        mTowerState = eTowerState.TOWERCONTROL;
        CurAngleX = Camera.main.transform.eulerAngles.x;
        CurAngleY = Camera.main.transform.eulerAngles.y;
        CurAngleZ = Camera.main.transform.eulerAngles.z;
    }

    public void TowerGetout(Camera _MainCamera)
    {
        mCollider.enabled = true;
        _MainCamera.orthographic = true;
        // _MainCamera.transform.position = 원래 카메라 포지션;
        // _MainCamera.transform.rotation = 원래 카메라 포지션;
        GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.NOMAL);
        mTowerState = eTowerState.IDLE;
        GameManager.stGameManager.mControlTower = null;
    }

    void IntowerCameraMove()
    {
        //나중에 타워 생설될 때 혹은 오브젝트on될 때로 변경
        teststick = GameManager.stGameManager.mStick;


        CurAngleY += teststick.Horizontal * fCameraSpeed;
        Quaternion tempQut = Quaternion.Euler(CurAngleX, CurAngleY, CurAngleZ);
        Head.rotation = tempQut;
        Camera.main.transform.rotation = tempQut;
        CurAngleX += - teststick.Vertical * fCameraSpeed;

        CurAngleX = ClampAngle(CurAngleX, -7f, 60f);


        tempQut = Quaternion.Euler(CurAngleX, CurAngleY, CurAngleZ);
        Head.rotation = tempQut;
        Camera.main.transform.rotation = tempQut;

        Camera.main.transform.position = Head.position - (tempQut * Vector3.forward * 5f) + (Vector3.up * 5f);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void SingleShot(Enemy _enemy,int _atk)
    {
        _enemy.mStat.hp -= _atk;
    }

    void MultiShot(Enemy _targetEnemy,int _atk,float _atkarea)
    {
        Transform _tartgetTr = _targetEnemy.transform;

        Collider[] colls = Physics.OverlapSphere(_tartgetTr.position, _atkarea);
        Enemy[] enemys = new Enemy[colls.Length];
        int enemysarridx=0;
        

        for(int i=0;i<colls.Length;i++)
        {
            if(colls[i].GetComponent<Enemy>())
            {
                enemys[enemysarridx] = colls[i].GetComponent<Enemy>();
                enemysarridx++;
            }
        }

        for(int i=0;i<enemysarridx;i++)
        {
            enemys[i].mStat.hp -= _atk;
        }

        if(_targetEnemy.CheckDead())
        {
            FindEnemyobj = null;
            mTowerState = eTowerState.IDLE;
        }

        for(int i=0;i<enemysarridx;i++)
        {
            if (enemys[i].CheckDead())
            {
                enemys[i].gameObject.SetActive(false);
            }
        }
        
    }

    void SlowShot(Enemy _targetEnemy, int _atk, float _sercharea)
    {
        Transform _tartgetTr = _targetEnemy.transform;

        Collider[] colls = Physics.OverlapSphere(transform.position, _sercharea);
        Enemy[] enemys = new Enemy[colls.Length];
        int enemysarridx = 0;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].GetComponent<Enemy>())
            {
                enemys[enemysarridx] = colls[i].GetComponent<Enemy>();
                enemysarridx++;
            }
        }

        for (int i = 0; i < enemysarridx; i++)
        {
            enemys[i].mStat.hp -= _atk;
            enemys[i].DeBuffSet(Enemy.eDebuffName.SLOW);
        }

        if (_targetEnemy.CheckDead())
        {
            FindEnemyobj = null;
            mTowerState = eTowerState.IDLE;
        }

        for (int i = 0; i < enemysarridx; i++)
        {
            if (enemys[i].CheckDead())
            {
                enemys[i].gameObject.SetActive(false);
            }
        }


    }



    void FindShootEnemy(List<GameObject> _ListEnemy)
    {
        if (_ListEnemy.Count > 0)
        {
            if (mTowerState == eTowerState.IDLE)
            {

                //기획자가 원하는 대로 하기 위해서 overlapsphere를 사용했는데
                //getcomponent도 많이 사용했고 필요없는 collider도 함께 검색되어 메모리 사용이
                //많을 것으로 예상된다 어느것이 더 메모리를 덜 사용하는지는 추후에 테스트
                Collider[] colls = Physics.OverlapSphere(transform.position, fSerchDistance);
                int nComparenum = GameManager.stGameManager.nListFieldidx;

                if(colls.Length == 0)
                {
                    mTowerState = eTowerState.IDLE;
                    return;
                }

                


                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].GetComponent<Enemy>())
                    {
                        if (colls[i].transform.GetComponent<Enemy>().MonsterIdx <= nComparenum)
                        {
                            FindEnemyobj = colls[i].gameObject;
                            FindEnemy = colls[i].transform.GetComponent<Enemy>();
                            nComparenum = FindEnemy.MonsterIdx;
                        }
                    }
                }


                //float fShortestDistance = Mathf.Infinity;

                //for (int i = 0; i < _ListEnemy.Count; i++)
                //{
                //    float fDistanceToEnemy = Vector3.Distance(this.transform.position, _ListEnemy[i].transform.position);
                //    if (fDistanceToEnemy < fShortestDistance)
                //    {
                //        fShortestDistance = fDistanceToEnemy;
                //        FindEnemyobj = _ListEnemy[i];
                //        FindEnemy = FindEnemyobj.GetComponent<Enemy>();
                //    }
                //}

                if (FindEnemyobj != null)
                {
                    mTowerState = eTowerState.ATTACK;
                }
            }


            //switch문 넣어서 tower종류마다 공격방식을 달리하기

            

            if (mTowerState == eTowerState.ATTACK)
            {
                if (FindEnemyobj == null || FindEnemyobj.activeSelf == false)
                {
                    mTowerState = eTowerState.IDLE;
                    return;
                }

                Head.transform.LookAt(FindEnemyobj.transform);
                if (fFireCoolDown <= 0f)
                {
                    switch (mTowerType)
                    {
                        case BuildManager.eTowerType.NULL:
                            return;
                        case BuildManager.eTowerType.A:
                            SingleShot(FindEnemy, nAtk);

                            if (FindEnemy.CheckDead())
                            {
                                FindEnemyobj.SetActive(false);
                                FindEnemyobj = null;
                                mTowerState = eTowerState.IDLE;
                            }
                            break;
                        case BuildManager.eTowerType.B:
                            MultiShot(FindEnemy, nAtk, fAtkArea);


                            break;
                        case BuildManager.eTowerType.C:
                            SlowShot(FindEnemy, nAtk, fSerchDistance);

                            break;
                        default:
                            return;
                    }


                   
                    fFireCoolDown = 1f / fFireRate;
                }

                fFireCoolDown -= Time.deltaTime;

            }
        }
    }

    bool ControlMode(List<GameObject> _ListEnemy)
    {
        IntowerCameraMove();
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));


        RaycastHit hitobj;
        
        if (Input.GetMouseButtonDown(0))
        {
            //Physics.BoxCast(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0)),
            //    transform.lossyScale/2,
            //    Camera.main.transform.forward,
            //    out hitobj,
            //    Camera.main.transform.rotation,
            //    Mathf.Infinity);
            Camera curCamera = Camera.main;



            //Physics.Raycast(ray, out hitobj, fSerchDistance);
            Physics.BoxCast(curCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)), new Vector3(1, 1, 1), curCamera.transform.forward, out hitobj, curCamera.transform.rotation, fSerchDistance);
            //Debug.DrawRay(ray.origin, ray.direction * fSerchDistance, Color.red, 5f);
            Debug.DrawRay(curCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)), curCamera.transform.forward*fSerchDistance, Color.red, 5f);
            //Debug.DrawRay(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)), transform.forward*fSerchDistance, Color.red, 5f);
            //Debug.Log("Towerhit" + hitobj.transform.name);


            if (hitobj.transform == null)
            {
                return false;
            }


            //여기도 타워마다 능력을 달리해줘야함
            
            if (hitobj.transform.gameObject.CompareTag("Enemy"))
            {
                Enemy HitEnemy = hitobj.transform.GetComponent<Enemy>();

                SingleShot(HitEnemy,nAtk);

                if (HitEnemy.CheckDead())
                {
                    HitEnemy.gameObject.SetActive(false);
                    
                }
                return true;
            }
            else if(hitobj.transform.gameObject.CompareTag("Boss"))
            {
                //보스 때리는 코드 생각 작동만 생각했을 때 아래처럼
                Boss HitBoss = hitobj.transform.parent.transform.GetComponent<Boss>();
                //Debug.Log("" + HitBoss.mStat.hp + "/" + hitobj.transform.name);
                if(hitobj.transform == HitBoss.Head)
                {
                    SingleShot(HitBoss, nAtk*2);
                }
                else if(hitobj.transform == HitBoss.Body)
                {
                    SingleShot(HitBoss, nAtk);
                }
                else if(hitobj.transform == HitBoss.Leg)
                {
                    SingleShot(HitBoss, nAtk);
                    HitBoss.nvAgent.speed -= 2;
                }
                

                if (HitBoss.CheckDead())
                {
                    HitBoss.gameObject.SetActive(false);
                }
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fSerchDistance);
        if (FindEnemyobj)
            Gizmos.DrawWireSphere(FindEnemyobj.transform.position, fAtkArea);

    }


}
