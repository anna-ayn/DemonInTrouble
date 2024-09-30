using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTeleport : MonoBehaviour
{
    public Agent agent;
    private float boundaryX = 9f;
    private float boundaryY = 5f;

    void Update()
    {
        // Verifica si el agente se ha salido del área de juego
        if (agent.kinematic.position.x > boundaryX)
        {
            TeleportToLeft();
        }
        else if (agent.kinematic.position.x < -boundaryX)
        {
            TeleportToRight();
        }

        if (agent.kinematic.position.y > boundaryY)
        {
            TeleportToBottom();
        }
        else if (agent.kinematic.position.y < -boundaryY)
        {
            TeleportToTop();
        }

        agent.updateTransform();
    }

    void TeleportToLeft()
    {
        agent.kinematic.position = new Vector3(-boundaryX, agent.kinematic.position.y, 0f);
    }

    void TeleportToRight()
    {
        agent.kinematic.position = new Vector3(boundaryX, agent.kinematic.position.y, 0f);
    }

    void TeleportToTop()
    {
        agent.kinematic.position = new Vector3(agent.kinematic.position.x, boundaryY, 0f);
    }

    void TeleportToBottom()
    {
        agent.kinematic.position = new Vector3(agent.kinematic.position.x, -boundaryY, 0f);
    }
}