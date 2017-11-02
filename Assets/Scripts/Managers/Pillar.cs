using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Pillar : MonoBehaviour, IHealth
{
    [Header("HealthManagement")]
    [SerializeField] private float MaxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private float invTime;
    [SerializeField] private Image HealthBar;
    private bool canTakeDamage = true;
    private void Start()
    {
        currentHp = MaxHp;
    }
    public void TakeDamage(float amount,Vector3 origin)
    {
        if (canTakeDamage)
        {
            currentHp = currentHp - amount <= 0 ? 0 : currentHp - amount;
            HealthBar.fillAmount = ((currentHp / MaxHp));
            StartCoroutine(Invincible(invTime));
        }
    }
    private IEnumerator Invincible(float waitTime)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(waitTime);
        canTakeDamage = true;
    }
}
