using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private int player;

    public GameObject orbFXPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == player)
        {
            Instantiate(orbFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManage.PlayOrbAudio();
        }
    }
}
