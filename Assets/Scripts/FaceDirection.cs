using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    public Agent agent;
    private SpriteRenderer spriteCharacter;

    void Start() {
        spriteCharacter = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (agent.kinematic.velocity.x > 0) {
            spriteCharacter.flipX = false;
        } else if (agent.kinematic.velocity.x < 0) {
            spriteCharacter.flipX = true;
        }
    }
}
