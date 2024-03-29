using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject bomb;
    private GameObject currentItem;
    private bool droppingItem = false;

    public void Start()
    {
        currentItem = bomb;
    }

    public void DropItem()
    {
        if (droppingItem || currentItem == null)
        {
            return;
        }
        
        print("drop item");
        // Drop an item
        // Instantiate the item prefab
        Instantiate(currentItem, transform.position, Quaternion.identity);
        currentItem = null;
    }
}
