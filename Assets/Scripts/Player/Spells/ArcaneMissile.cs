﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Spell {

    [Header("ArcaneMissile Attributes")]
    [SerializeField] private float arcAngle;
    [SerializeField] private float arcHeight;
    [SerializeField] private float speed;
    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(ArcaneMissile))
            {
                Debug.Log("ArcaneMissile spell casted...");
                player.GetBeads().SpendMana(MPCost);
                GameObject target;
                ProjectileArc pa;
                target = player.GetTarget();
                if (GamManager.singleton.IsLeftHanded())
                {
                    pa = projectile.Instantiate<ProjectileArc>();
                    pa.transform.position = player.leftHandPosition.position;
                }
                else
                {
                    pa = projectile.Instantiate<ProjectileArc>();
                    pa.transform.position = player.rightHandPosition.position;
                }
                pa.SetDieTime(10.0f);
                pa.SetDamage(damage);
                if (!target)
                {
                    Rigidbody rb = pa.GetComponent<Rigidbody>();
                    rb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    rb.GetComponent<Rigidbody>().AddForce(player.spellDirection.forward * speed, ForceMode.Impulse);
                }
                else
                {
                    pa.SetSpeed(speed);
                    pa.SeekTargetArc(target, arcHeight, arcAngle, true);
                }
            }
        }
    }

    public override void DeactivateSpell()
    {

    }
}
