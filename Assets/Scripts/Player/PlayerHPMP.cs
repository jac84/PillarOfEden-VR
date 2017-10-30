using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMP : MonoBehaviour, IHealth
{

    [SerializeField] private float healthPoints;
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private float manaPoints;
    [SerializeField] private float maxManaPoints;

    [SerializeField] private float invTime;
    [SerializeField] private UpdateBracelet hPBeads;
    [SerializeField] private UpdateBracelet manaBeads;
    [SerializeField] private float hpRegenRatePerSec;
    [SerializeField] private float manaRegenRatePerSec;

    public bool canTakeDamage = true;
    private float genTime = 0;

    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            Debug.Log("Player Took Damage");
            healthPoints = healthPoints > amount ? healthPoints - amount : 0;
            hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
            //Check If Dead
            if (healthPoints <= 0)
            {

            }
            StartCoroutine(Invincible(invTime));
        }
    }
    public void UpdateHPMP()
    {
        if(canTakeDamage)
        {
            if (genTime <= Time.time)
            {
                if (healthPoints <= maxHealthPoints)
                {
                    RegenerateHealth(hpRegenRatePerSec);
                    hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
                }
                if (manaPoints <= maxHealthPoints)
                {
                    RegenerateMana(manaRegenRatePerSec);
                    manaBeads.UpdateBeads(manaPoints, maxManaPoints);
                }
                genTime = Time.time + 1.0f;
            }
        }

    }
    public void SetCanTakeDamage(bool t)
    {
        canTakeDamage = t;
    }
    public int GetPercentageHP()
    {
        return (int)((healthPoints / maxHealthPoints) * 100);
    }
    public int GetPercentageMP()
    {
        return (int)((manaPoints / maxManaPoints) * 100);
    }
    public void SpendMana(float amount)
    {
        manaPoints = manaPoints > amount ? manaPoints - amount : 0;
        manaBeads.UpdateBeads(manaPoints, maxManaPoints);
    }
    private void RegenerateMana(float amount)
    {
        manaPoints = manaPoints + amount >= maxManaPoints ? maxManaPoints : manaPoints + amount;
    }
    private void RegenerateHealth(float amount)
    {
        healthPoints = healthPoints + amount >= maxHealthPoints ? maxHealthPoints : healthPoints + amount;
    }
    private IEnumerator Invincible(float waitTime)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(waitTime);
        canTakeDamage = true;
    }
}
