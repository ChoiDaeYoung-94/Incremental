using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TEST");

        if (collision.CompareTag("Monster"))
            Managers.PoolM.PushToPool(collision.gameObject);
    }
}
