using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;
public class TowerManager : Photon.MonoBehaviour
{
    [SerializeField] private List<Tower> listOfTowers= new List<Tower>();

    public void AddTower(Tower tower)
    {
        listOfTowers.Add(tower);
    }
    public void RemoveTower(Tower tower)
    {
        listOfTowers.Remove(tower);
        PhotonNetwork.Destroy(tower.gameObject);
    }
    private void Update()
    {
        if (listOfTowers.Count > 0)
        {
            foreach (Tower tower in listOfTowers)
            {
                tower.StartAttack();
            }
        }
    }
}
