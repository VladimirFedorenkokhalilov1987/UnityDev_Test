using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject BossBullet;
    public Transform BossBulletSpawnPosition;

    public float timeToNextBossShoot=3;
    private float timeToNextBossShootReset=3;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextBossShootReset = timeToNextBossShoot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(GameObject.FindObjectOfType<FirstPersonController>().transform);

        timeToNextBossShoot -= Time.deltaTime;
        if (timeToNextBossShoot <= 0)
            BossShootThePlayer();

    }

    private void BossShootThePlayer()
    {
        timeToNextBossShoot = timeToNextBossShootReset;
        GameObject bossBullet = BossBullet.Spawn(BossBulletSpawnPosition);
        bossBullet.gameObject.GetComponent<Bullet>().isBossBullet = true;
    }
}
