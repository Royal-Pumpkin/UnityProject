using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy {
    public bool bBomb = false;
    public Transform vecBombPosition;
    public Transform TrBoom;
    float fSerchDistance = 5f;

    private void OnEnable()
    {
        StartCoroutine(BombPick());
    }

    public override bool CheckDead()
    {
        if (mStat.hp <= 0)
        {
            GameManager.stGameManager.mStageManager.nInGameGold += gear;
            BombDrop();
            return true;
        }
        else
            return false;
    }

    void BombDrop()
    {
        if (bBomb == false)
            return;

        TrBoom.parent = null;
        TrBoom.position = this.transform.position;
        TrBoom = null;
        bBomb = false;
    }


    IEnumerator BombPick()
    {
        while(true)
        {
            if (bBomb)
                break;

            Collider[] colls = Physics.OverlapSphere(transform.position, fSerchDistance);
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].CompareTag("Bomb"))
                {
                    if (colls[i].transform.parent)
                        continue;

                    TrBoom = colls[i].transform;
                    TrBoom.position = vecBombPosition.position;
                    TrBoom.rotation = vecBombPosition.rotation;
                    TrBoom.SetParent(this.transform);
                    bBomb = true;
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }


}
