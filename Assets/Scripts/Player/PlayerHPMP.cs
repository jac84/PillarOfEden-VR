using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMP : MonoBehaviour, IHealth
{

    [SerializeField] private int healthPoints;
    [SerializeField] private int maxHealthPoints;
    [SerializeField] private int manaPoints;
    [SerializeField] private int maxManaPoints;

    [SerializeField] private UpdateBracelet hPBeads;
    [SerializeField] private UpdateBracelet manaBeads;

    public bool canTakeDamage = true;

    public void TakeDamage(int amount)
    {
        healthPoints = healthPoints > amount ? healthPoints - amount : 0;
        hPBeads.UpdateBeads(healthPoints, maxHealthPoints);
    }
    public int GetPercentageHP()
    {
        return (int)((healthPoints / maxHealthPoints) * 100);
    }
    public int GetPercentageMP()
    {
        return (int)((manaPoints / maxManaPoints) * 100);
    }
	public void SpendMana(int amount)
	{
        manaPoints -= amount;
        manaBeads.UpdateBeads(manaPoints, maxManaPoints);
	}
	public void RegenerateMana(int amount)
	{
        manaPoints += amount;
	}
	public void RegenerateHealth(int amount)
	{
		healthPoints += amount;
	}
}
