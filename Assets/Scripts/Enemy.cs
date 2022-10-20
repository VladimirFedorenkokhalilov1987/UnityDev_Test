using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;
    public float EnemyDamage;
    public float speed;
    public float bossSpeed;
    public bool isBoss=true;
    public bool CanBeRicosheted=true;

    private GameObject Target;
    private FirstPersonController player;
    
    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        player = Target.GetComponent<FirstPersonController>();
    }

    IEnumerator PreSpawn()
    {
        if (!isBoss)
        {
            this.transform.Translate(2*Vector3.up * Time.deltaTime);
            yield return new WaitForSeconds(1);
        }
        float step =  speed * Time.deltaTime;
        if (isBoss)
            step = bossSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
    }

    private void Update()
    {
        StartCoroutine(PreSpawn());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.TakeDamage(EnemyDamage);
            this.gameObject.Recycle();
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            Die();
        }
    }

    public void PowerOnKill(float amount)
    {
        if (FirstPersonController.Power < 100)
        {
            FirstPersonController.Power += amount;
            player.PowerText.text = FirstPersonController.Power.ToString();
        }

        if (FirstPersonController.Power >= 100)
        {
            FirstPersonController.Power = 100;
            player.PowerText.text = FirstPersonController.Power.ToString();
            player.UltimateButton.SetActive(true);
        }
    }

    void Die()
    {
        gameObject.Recycle();
    }
}