using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDisparo : MonoBehaviour
{
    public PlayerEsquivarAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision){
        // check if the object that collided with the floor is a rock
        if (collision.gameObject.CompareTag("Disparo"))
        {
            agent.disparos.Remove(collision.gameObject);
            // destroy the rock object
            Destroy(collision.gameObject);
        }
    }
}