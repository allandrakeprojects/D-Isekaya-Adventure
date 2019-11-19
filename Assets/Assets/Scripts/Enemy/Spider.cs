using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {

    }

    public void Damage()
    {
        if (isDead)
            return;
        
        Health--;
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            isDead = true;

            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Coin>().coins = base.coins;
        }
    }

    public override void Movement()
    {

    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
