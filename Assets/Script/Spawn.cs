using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float ss = -2;//몬스터 생성 x값 처음
    public float es = 2;//x값 끝
    public float StartTime = 1;//시작
    public float SpawnStop = 10;//생성 끝나는 시간

    [SerializeField]
    private GameObject[] monsters;
    bool swi1 = true;
    bool swi2 = true;
    private bool bossSpawned = false;

    [SerializeField]
    GameObject textBossWarning;

    private void Awake()
    {
        textBossWarning.SetActive(false);
    }
    void Start()
    {
        StartCoroutine("RandomSpawn1");
        Invoke("Stop", SpawnStop);
    }

    IEnumerator RandomSpawn1()
    {
        while (swi1)
        {
            yield return new WaitForSeconds(StartTime);
            float x = Random.Range(ss,es);
            Vector2 r = new Vector2(x, transform.position.y);

            Instantiate(monsters[0],r,Quaternion.identity);
        }
    }
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            yield return new WaitForSeconds(StartTime+2);
            float x = Random.Range(ss, es);
            Vector2 r = new Vector2(x, transform.position.y);

            Instantiate(monsters[1], r, Quaternion.identity);
        }
    }

    private void Stop()
    {
        swi1 = false;
        StopCoroutine("RandomSpawn1");
        //두번째 몬스터
        StartCoroutine("RandomSpawn2");
        Invoke("Stop2", SpawnStop + 20);
    }

    private void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");
    }

    public void BossSpawn()
    {
        textBossWarning.SetActive(true);
        Instantiate(monsters[2], transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (!swi2 && !bossSpawned)
        {
            int monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;
            if (monsterCount <= 0)
            {
                bossSpawned = true;
                BossSpawn();
            }
        }
    }
}