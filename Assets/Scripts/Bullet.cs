using System;
using UnityEngine;
using System.Collections;
using StarterAssets;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float shootDistance;
    public float shootSpeed;
    public float bossBulletPower = 25;
    public float bossBulletHitEnemyPower = 50;
    public float ifHitEnemyAddPower = 15;
    public bool isBossBullet;

    private float RicosheteChance = 0.3f;
    private float chanceCalculated;
    private bool isRicosheted;
    private FirstPersonController player;

    void Update()
    {
        if(!isBossBullet)
            StartCoroutine(ShootToEnemy());
        if (isBossBullet)
            StartCoroutine(ShootToPlayer());
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }


    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootToEnemy()
    {
        float travelledDistance = 0;
        while (travelledDistance < shootDistance)
        {
            travelledDistance += shootSpeed * Time.deltaTime;
            transform.position += transform.forward * (shootSpeed * Time.deltaTime);
            yield return 0;
        }
        yield return new WaitForSeconds(2);
        gameObject.Recycle();
        //Recycle this pooled bullet instance
    }

    IEnumerator ShootToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10*Time.deltaTime);
        yield return null;
    }

    IEnumerator Ricoshete()
    {
            transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<Enemy>().transform.position, 10*Time.deltaTime);
            yield return new WaitForSeconds(2);
            gameObject.Recycle();
    }

    private void CalculateRicoshetChanse()
    {
        if (FirstPersonController.Health <= 40)
        {
            RicosheteChance = 0.95f;
        }
        if (FirstPersonController.Health > 40)
        {
            RicosheteChance = 0.3f;
        }
        chanceCalculated = Random.Range(0,1f);

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Enemy" && !isBossBullet)
        {
            CalculateRicoshetChanse();
            

            if (chanceCalculated > RicosheteChance)
            {
                isRicosheted = true;
            }

            other.gameObject.GetComponent<Enemy>().TakeDamage(bossBulletHitEnemyPower);
            other.gameObject.GetComponent<Enemy>().PowerOnKill(ifHitEnemyAddPower);
            GameStats.EnemysKilled++;
            if(!isRicosheted)
                gameObject.Recycle();
            if (isRicosheted && !isBossBullet)
                StartCoroutine(Ricoshete());
        }

      

        
        if (other.gameObject.tag == "Player")
        {
            player.TakePowerDecrease(bossBulletPower);
            gameObject.Recycle();
        }
    }
}