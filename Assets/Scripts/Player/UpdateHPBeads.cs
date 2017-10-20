using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHPBeads : MonoBehaviour
{
    [SerializeField] private Transform[] beads = new Transform[7];
    [SerializeField] private GameObject currentBead;
    private double percentPerBead;
    private Renderer rend;
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
            rend.material.color = Color.black;
        }
        if (current > 0)
        {
            rend = currentBead.GetComponent<Renderer>();
            float fade = (float)(max - (percentPerBead * ((beads.Length - 1) - beadNum)));
            fade = fade - current;
            fade = (float)((percentPerBead - fade) / percentPerBead);
            Color.RGBToHSV(rend.material.color, out h, out s, out v);
            /* 
            if(fade < .3f)
                fade += .2f;
            else if( fade >.8f){
                    fade -= .25f;
                }
            */
            rend.material.color = Color.HSVToRGB(h, s, fade);
        }
    }
}
