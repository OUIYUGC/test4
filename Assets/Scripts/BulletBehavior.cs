using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Destroy(this.gameObject, onscreenDelay); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
