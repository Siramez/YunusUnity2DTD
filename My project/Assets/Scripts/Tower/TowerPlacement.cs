using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public Tower placedTower;
    public bool IsOccupied;
    void Update()
    {
        if (placedTower != null && placedTower.Placed)
        {

            IsOccupied = true;
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (placedTower != null)
        {
            return;
        }
        Tower tower = collision.GetComponent<Tower>();
        if (tower != null&&!IsOccupied && (LevelManager.Instance.Gold - tower.Tower_Cost)>=0)
        {
            tower.SetPlacePosition (transform.position);
            placedTower = tower;
           
        } 
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (placedTower == null)
        {
            return;
        }
        placedTower.SetPlacePosition (null);
        placedTower = null;
    }
}
