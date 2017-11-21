using UnityEngine;
using System.Collections;

public class Potion : Item
{
    [SerializeField] private float strength;
    [SerializeField] private bool healByValue;
    public override void UseItem()
    {
        if (healByValue)
            HealByValue(strength);
        else
            HealByPercentage(strength);
    }

    private void HealByPercentage( float percentage)
    {
    }
    private void HealByValue(float amount)
    {

    }
}
