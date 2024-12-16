using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspoPortal : MonoBehaviour
{
    [SerializeField] private float bobSpeed;
    [SerializeField] private float bobHeight;

    private void Update()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.name == "Player")
        {
            SceneManager.LoadScene("CombatAndInspo");
        } 
    }
}
