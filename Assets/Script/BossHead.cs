using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    [SerializeField]
    GameObject bossBullet;//���� �̻���

    //�ִϸ��̼� �Լ� ����ϱ�
    public void RightDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<BossBullet>().Move(new Vector2(1, -1));
    }
    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<BossBullet>().Move(new Vector2(-1, -1));
    }
    public void DownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);
        go.GetComponent<BossBullet>().Move(new Vector2(0, -1));
    }
}
