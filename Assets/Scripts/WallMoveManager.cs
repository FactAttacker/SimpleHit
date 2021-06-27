using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveManager : MonoBehaviour
{
    enum MoveType
    {
        없음,
        위아래,
        좌우,
    }
    [System.Serializable]
    class MoveWallObj
    {
        public GameObject wall;
        public MoveType type;
    }
    [SerializeField]
    MoveWallObj[] moveWallObjs;

    //Vector3 md = new Vector3(0, -1f, 0);
    //Vector3 mu = new Vector3(0, 1f, 0);

    WaitForSeconds waitMoveSpeed1s;
    WaitForSeconds waitMoveSpeed10s;
    WaitForSeconds waitMoveSpeed20s;
    void Start()
    {
        waitMoveSpeed1s = new WaitForSeconds(1f * Time.deltaTime);
        waitMoveSpeed10s = new WaitForSeconds(10f * Time.deltaTime);
        waitMoveSpeed20s = new WaitForSeconds(20f * Time.deltaTime);
        foreach (MoveWallObj obj in moveWallObjs)
        {
            switch (obj.type)
            {
                case MoveType.위아래:
                    StartCoroutine(MoveWallCoroutine(obj.wall));
                break;
            }
        }
    }
    IEnumerator MoveWallCoroutine(GameObject obj)
    {
        //움직임 편차 (다 동일하게 움직이는거 방지)
        switch (Random.Range(1, 4))
        {
            case 1:
                yield return waitMoveSpeed1s;
                break;
            case 2:
                yield return waitMoveSpeed10s;
                break;
            case 3:
                yield return waitMoveSpeed20s;
                break;
        }

        float min = obj.transform.position.y;
        float max = min + 8f;
        bool isUp = true;
        while (true)
        {
            yield return waitMoveSpeed1s;
            if (isUp)
            {
                //올라감
                obj.transform.position += 5f * Time.deltaTime * Vector3.up;
                //현재 y <= y+8
                if (obj.transform.position.y > max) isUp = false;
            }
            else
            {
                //내려감
                obj.transform.position += 5f * Time.deltaTime * Vector3.down;
                if (obj.transform.position.y < min) isUp = true;
            }
            
        }
    }
}