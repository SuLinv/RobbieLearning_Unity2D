using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int trapsLayer;

    public GameObject deathVFXPrefab;
    // Start is called before the first frame update
    void Start()
    {
        trapsLayer = LayerMask.NameToLayer("Trap");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == trapsLayer)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManage.PlayDeathAudio();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
