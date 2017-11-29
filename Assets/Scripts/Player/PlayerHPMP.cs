using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMP : MonoBehaviour, IHealth
{

    [SerializeField] private float healthPoints;
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private float manaPoints;
    [SerializeField] private float maxManaPoints;
    [Space(10)]
    [SerializeField] private float invTime;
    [SerializeField] private UpdateBracelet hPBeads;
    [SerializeField] private UpdateBracelet manaBeads;
    [SerializeField] private float hpRegenRatePerSec;
    [SerializeField] private float manaRegenRatePerSec;

    [Space(10)]
    [SerializeField] private float shieldDamageMitigationPercentage;

    public bool canTakeDamage = true;
    private float genTime = 0;
    private void Awake()
    {
        healthPoints = maxHealthPoints;
        manaPoints = maxManaPoints;
    }
    public void TakeDamage(float amount,Vector3 origin)
    {
        if (canTakeDamage)
        {
            if(GamManager.singleton.player.GetShieldActivated())
            {
                Vector3 direction = origin - transform.position;
                float dot = Vector3.Dot(direction, GamManager.singleton.mainVRCamera.transform.forward);
                if (dot > .8)
                {
                    float d = amount * (shieldDamageMitigationPercentage / 100);
                    healthPoints = healthPoints > d ? healthPoints - d : 0;
                    hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
                    return;
                }
            }
            healthPoints = healthPoints > amount ? healthPoints - amount : 0;
            hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
            //Check If Dead
            if (healthPoints <= 0)
            {
                GamManager.singleton.GameOver();
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
    public bool SpendMana(float amount)
    {
        if (amount > manaPoints)
            return false;
        manaPoints = manaPoints > amount ? manaPoints - amount : 0;
        manaBeads.UpdateBeads(manaPoints, maxManaPoints);
        return true;
    }
    private void RegenerateMana(float amount)
    {
        manaPoints = manaPoints + amount >= maxManaPoints ? maxManaPoints : manaPoints + amount;
        manaBeads.UpdateBeads(manaPoints, maxManaPoints);
    }
    private void RegenerateHealth(float amount)
    {
        healthPoints = healthPoints + amount >= maxHealthPoints ? maxHealthPoints : healthPoints + amount;
        hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
    }
    private IEnumerator Invincible(float waitTime)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(waitTime);
        canTakeDamage = true;
    }
    public void SetHp(float Hp)
    {
        healthPoints = Hp;
    }
    public float GetMana()
    {
        return manaPoints;
    }
    public void ResetBeads()
    {
        healthPoints = maxHealthPoints;
        manaPoints = maxManaPoints;
        hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
        manaBeads.UpdateBeads(manaPoints, maxManaPoints);
    }
}
