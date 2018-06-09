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
    public int nTier = 1;
    public float fSerchDistance = 20f;
    public float fFireCoolDown = 0f;
    public float fFireRate = 0.5f;
    public int nPrice;
    public float fAtkArea = 5f;
    public float fSlow = 2f;
    

    public float fCameraSpeed = 5f;

    public GameObject FindEnemyobj = null;
    Enemy FindEnemy = null;

    float CurAngleX;
    float CurAngleY;
    float CurAngleZ;

    //테스트용
    Vector3 testTr;

    //타워기본행동
    public void DefaultTowerAct()
    {
        

        if (mTowerState != eTowerState.TOWERCONTROL)
        {
            FindShootEnemy(GameManager.stGameManager.mStageManager.nListFieldidx);

            return;
        }


        ControlMode();
    }

    //타워 카메라 이동
    public void TowerGetin(Camera _MainCamera)
    {
        GameManager.stGameManager.mControlTower = this;
        GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.TOWER);
        GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.CONTROLSCENE);

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
        float tempDownDam = 0;
        tempDownDam = 1 - (_enemy.mStat.def / (_enemy.mStat.def + 20));
        _enemy.mStat.hp -= _atk * (int)tempDownDam;
    }

    void MultiShot(Transform _targetEnemy,Vector3 _targetPostion,int _atk,float _atkarea)
    {

        Collider[] colls = Physics.OverlapSphere(_targetPostion, _atkarea);
        Enemy[] enemys = new Enemy[colls.Length];
        int enemysarridx=0;

        //캐싱해서 쓸 수 있도록
        if (mTowerState == eTowerState.TOWERCONTROL)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].CompareTag("Bomb"))
                {
                    Bombboom(colls[i].transform);
                }
            }
        }

        for (int i=0;i<colls.Length;i++)
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

        //이것도 캐싱해서 쓸 수 있도록
        if (_targetEnemy.GetComponent<Enemy>())
        {
            Enemy tempEnemy = _targetEnemy.GetComponent<Enemy>();
            if (tempEnemy.CheckDead())
            {
                if (mTowerState != eTowerState.TOWERCONTROL)
                {
                    FindEnemyobj = null;
                    mTowerState = eTowerState.IDLE;
                }
            }
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
            enemys[i].DeBuffSet(Enemy.eDebuffName.SLOW,fSlow);
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



    void FindShootEnemy(int _ListEnemy)
    {
        if(this.gameObject.activeSelf == false)
        {
            return;
        }
        


        if (_ListEnemy > 0)
        {
            //기획자가 원하는 대로 하기 위해서 overlapsphere를 사용했는데
            //getcomponent도 많이 사용했고 필요없는 collider도 함께 검색되어 메모리 사용이
            //많을 것으로 예상된다 어느것이 더 메모리를 덜 사용하는지는 추후에 테스트
            Collider[] colls = Physics.OverlapSphere(transform.position, fSerchDistance);
            int nComparenum = GameManager.stGameManager.mStageManager.nListFieldidx;

            if (colls.Length == 0)
            {
                mTowerState = eTowerState.IDLE;
                return;
            }


            //사거리 밖으로 나갔을 떄 IDLE상태로 돌려줘야함

            for (int i = 0; i < colls.Length; i++)
            {
                Enemy tempEnemy = colls[i].GetComponent<Enemy>();
                if (colls[i].CompareTag("Enemy"))
                {
                    if (tempEnemy.MonsterIdx <= nComparenum)
                    {
                        FindEnemyobj = colls[i].gameObject;
                        FindEnemy = tempEnemy;
                        nComparenum = FindEnemy.MonsterIdx;
                    }
                }
            }


            if (mTowerState == eTowerState.IDLE)
            {

                


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

                //switch (FindEnemy.mEnemyType)
                //{
                //    case Enemy.eEnemyType.NULL:
                //        break;
                //    case Enemy.eEnemyType.GOBLIN:
                //        FindEnemy = FindEnemy.GetComponent<Goblin>();
                //        break;
                //    default:
                //        break;
                //}


                if (fFireCoolDown <= 0f)
                {
                    switch (mTowerType)
                    {
                        case BuildManager.eTowerType.NULL:
                            return;
                        case BuildManager.eTowerType.AA:
                        case BuildManager.eTowerType.AB:
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
                            //위에 검색하는기능이 아래 함수에 검색이랑 겹침
                            MultiShot(FindEnemyobj.transform,FindEnemyobj.transform.position, nAtk, fAtkArea);


                            break;
                        case BuildManager.eTowerType.C:
                            //위에 검색하는기능이 아래 함수에 검색이랑 겹침
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

    bool ControlMode()
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


            switch (mTowerType)
            {
                case BuildManager.eTowerType.NULL:
                    break;
                case BuildManager.eTowerType.AA:
                case BuildManager.eTowerType.AB:
                case BuildManager.eTowerType.A:
                    if (hitobj.transform.gameObject.CompareTag("Enemy"))
                    {
                        Enemy HitEnemy = hitobj.transform.GetComponent<Enemy>();
                        switch (HitEnemy.mEnemyType)
                        {
                            case Enemy.eEnemyType.NULL:
                                break;
                            case Enemy.eEnemyType.GOBLIN:
                                HitEnemy = hitobj.transform.GetComponent<Goblin>();
                                break;
                            default:
                                break;
                        }

                        SingleShot(HitEnemy, nAtk);

                        if (HitEnemy.CheckDead())
                        {
                            HitEnemy.gameObject.SetActive(false);
                        }
                        return true;
                    }
                    else if (hitobj.transform.CompareTag("Boss"))
                    {
                        //보스 때리는 코드 생각 작동만 생각했을 때 아래처럼
                        Boss HitBoss = hitobj.transform.parent.transform.GetComponent<Boss>();
                        //Debug.Log("" + HitBoss.mStat.hp + "/" + hitobj.transform.name);
                        if (hitobj.transform == HitBoss.Head)
                        {
                            SingleShot(HitBoss, nAtk * 2);
                        }
                        else if (hitobj.transform == HitBoss.Body)
                        {
                            SingleShot(HitBoss, nAtk);
                        }
                        else if (hitobj.transform == HitBoss.Leg)
                        {
                            SingleShot(HitBoss, nAtk);
                            HitBoss.DeBuffSet(Enemy.eDebuffName.SLOW, fSlow);
                        }

                        if (HitBoss.CheckDead())
                        {
                            HitBoss.gameObject.SetActive(false);
                        }
                        return true;
                    }
                    else if(hitobj.transform.CompareTag("Bomb"))
                    {
                        Debug.Log("dd");
                        Bombboom(hitobj.transform);
                    }
                    

                    break;
                case BuildManager.eTowerType.B:
                    testTr = hitobj.point;
                    MultiShot(hitobj.transform,hitobj.point, nAtk, fAtkArea);


                    break;
                case BuildManager.eTowerType.C:
                    break;
                default:
                    break;
            }

            
        }

        return false;
    }

    public bool TowerUpgrade(BuildManager.eTowerType _upgradetype)
    {
        mTowerType = _upgradetype;
        int _upatk = 0; //리스트에 저장된 상승률
        
        switch(nTier)
        {
            case 1:
            {
                    switch (_upgradetype)
                    {
                        case BuildManager.eTowerType.NULL:
                            return false;

                        case BuildManager.eTowerType.AA:
                            _upatk += 100;
                            break;
                        case BuildManager.eTowerType.AB:
                            _upatk += 10;
                            break;
                        case BuildManager.eTowerType.BA:

                            break;
                        case BuildManager.eTowerType.BB:

                            break;
                        case BuildManager.eTowerType.CA:

                            break;
                        case BuildManager.eTowerType.CB:

                            break;
                        default:
                            return false;
                    }
                    break;
            }
            default:
                return false;
                
        }


        ++nTier;
        StatUp(_upatk);
        return true;
    }

    void StatUp(int _upatk)
    {
        //각종 능력치 업그레이드 수치는 매개변수로 받아서 사용
        nAtk += _upatk;
        ++nTier;
    }


    void Bombboom(Transform _boomTr)
    {
        Collider[] colls = Physics.OverlapSphere(_boomTr.position, 20f);

        if(colls.Length == 0)
        {
            return;
        }


        Enemy[] tempEnemy = new Enemy[colls.Length];
        int enemysarridx = 0;

        for (int i=0;i < colls.Length;i++)
        {
            if (colls[i].CompareTag("Enemy"))
            {
                tempEnemy[enemysarridx] = colls[i].GetComponent<Enemy>();
                enemysarridx++;
            }
        }

        for(int i=0;i<enemysarridx;i++)
        {
            SingleShot(tempEnemy[i], 30);
            if (tempEnemy[i].CheckDead())
            {
                tempEnemy[i].gameObject.SetActive(false);
            }
        }
        _boomTr.gameObject.SetActive(false);
        _boomTr = null;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fSerchDistance);
        if (FindEnemyobj)
            Gizmos.DrawWireSphere(FindEnemyobj.transform.position, fAtkArea);
        
            Gizmos.DrawWireSphere(testTr, fAtkArea);
    }


}
