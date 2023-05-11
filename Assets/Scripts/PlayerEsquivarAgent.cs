using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerEsquivarAgent : Agent
{
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] public Material winMaterial;
    [SerializeField] public Material looseMaterial;

    public List<GameObject> disparos;

    BufferSensorComponent mybuffer;
    
    bool death;

    public Vector3 first_position;
    public override void OnEpisodeBegin(){

        transform.position = first_position;

        SetReward(+1f);

        if(death)
        {
            floorMeshRenderer.material= looseMaterial;
        }
        else
        {
            floorMeshRenderer.material= winMaterial;
        }

        death = false;
    }

    public void Start()
    {
        first_position = transform.position;
        mybuffer = GetComponent<BufferSensorComponent>();
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.position);

        foreach(GameObject disparo in disparos)
        {
            Rigidbody disparoRB = disparo.GetComponent<Rigidbody>();
            float[] info = { disparo.transform.position.z, disparo.transform.position.y, disparoRB.velocity.y };
            mybuffer.AppendObservation(info);
        }
    }

    public override void OnActionReceived(ActionBuffers actions){
        float moveZ = actions.ContinuousActions[0];
        float moveSpeed = 6f;
        transform.position += new Vector3(0,0,moveZ)* Time.deltaTime * moveSpeed;
    }
    
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Fall>(out Fall fall)){
            SetReward(-1f);
            death = true;
            EndEpisode();
        }
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Disparo")){
            SetReward(-1f);
            death = true;
            EndEpisode();
        }
    }

}