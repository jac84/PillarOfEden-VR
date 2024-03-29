﻿using UnityEngine;
using System.Collections;

public class UpdateBracelet : MonoBehaviour
{

    [SerializeField] private Transform[] beads = new Transform[7];
    [SerializeField] private GameObject currentBead;
    private double percentPerBead;
    private Renderer rend;
    [SerializeField] private Material originalColor;
    void Awake()
    {
        if (beads.Length == 0)
        {
            beads = transform.GetComponentsInChildren<Transform>();
        }
        currentBead = beads[1].gameObject;
    }
    public void UpdateBeads(float current, float max)
    {
        float h = 0;
        float s = 0;
        float v = 0;
        percentPerBead = max / (beads.Length - 1);
        int beadNum = Mathf.CeilToInt((beads.Length - 1) / (max / current));
        //Turn beads black
        currentBead = beads[beadNum].gameObject;
        for (int i = beadNum + 1; i <= beads.Length - 1; i++)
        {
            rend = beads[i].GetComponent<Renderer>();
            Color.RGBToHSV(originalColor.color, out h, out s, out v);
            rend.material.color = Color.HSVToRGB(h, s, 0);
        }
        if (current > 0)
        {
            rend = currentBead.GetComponent<Renderer>();
            float fade = (float)(max - (percentPerBead * ((beads.Length - 1) - beadNum)));
            fade = fade - current;
            fade = (float)((percentPerBead - fade) / percentPerBead);
            Color.RGBToHSV(originalColor.color, out h, out s, out v);
            rend.material.color = Color.HSVToRGB(h, s, fade);
        }
    }
}
