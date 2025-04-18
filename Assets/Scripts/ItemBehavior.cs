using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected");    
        }
        gameManager.Items += 1;

        gameManager.PrintLootReport();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}