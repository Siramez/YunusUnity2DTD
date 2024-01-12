using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TowerTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text fireRateText;
    [SerializeField] private TMP_Text distanceText;

    public void SetTowerAttributes(ElectricTowerAttack electricTower)
    {
        // Update the tooltip content based on the tower's attributes
        damageText.text = "Damage: " + electricTower.bulletDamage.ToString();
        fireRateText.text = "Fire Rate: " + electricTower.fireRate.ToString();
        distanceText.text = "Distance: " + electricTower.Distance.ToString();
    }

    internal void SetTowerAttributes(Tower electricTower)
    {
        throw new NotImplementedException();
    }
}
