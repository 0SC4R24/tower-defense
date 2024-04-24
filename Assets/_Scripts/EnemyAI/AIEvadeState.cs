using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEvadeState : BaseState
{
    [SerializeField] private float evadeMaxSpeed;
    [SerializeField] private float steeringMaxSpeed;
    
    public override void Construct()
    {
        aiBehaviour.maxSpeed = evadeMaxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
    }

    public override void Transition()
    {
        if (m_enemyAIStateMotor.stateEnum == AIState.Evade) return;
        base.Transition();
    }

    public override void FixedUpdateState()
    {
        aiBehaviour.Evade(m_enemyAIStateMotor.target.position, m_enemyAIStateMotor.rb, m_enemyAIStateMotor.targetRb);
    }
}
