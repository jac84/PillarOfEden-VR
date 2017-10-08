using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMP : MonoBehaviour, IHealth
{

    [SerializeField] private int healthPoints;
    [SerializeField] private int maxHealthPoints;
    [SerializeField] private int manaPoints;
    [SerializeField] private int maxManaPoints;

    public bool canTakeDamage = true;

    public void TakeDamage(int amount)
    {
        healthPoints = healthPoints > amount ? healthPoints - amount : 0;
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

	}
	public void RegenerateMana(int amount)
	{

	}
	public void RegenerateHealth(int amount)
	{
		
	}
}
