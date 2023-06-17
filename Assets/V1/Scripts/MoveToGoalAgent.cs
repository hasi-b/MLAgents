using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    float moveX;
    float moveY;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Transform targetTransform;


    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
        base.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
        base.CollectObservations(sensor);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        Debug.Log(moveX+","+moveY);
        transform.position += new Vector3(moveX,0f,moveY)*Time.deltaTime*moveSpeed;
        base.OnActionReceived(actions);
    }

    private void OnTriggerEnter(Collider other)
    {
        SetReward(1f);
        EndEpisode();
    }

}
