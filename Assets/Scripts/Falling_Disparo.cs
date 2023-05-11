using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Disparo: MonoBehaviour
{
    public GameObject Disparo;
    public PlayerEsquivarAgent agent;
    public float spawnRate = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Random.value < spawnRate * Time.deltaTime / 3f){
            SpawnDisparo();
        }
    }

    void SpawnDisparo(){
        GameObject disparo = Instantiate(Disparo, transform.position, Quaternion.identity);
        agent.disparos.Add(disparo.gameObject);
        float zOffset = Random.Range(-4f, 4f);
        disparo.transform.position += new Vector3(0,0,zOffset);

    
    }


}