using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        Vector3 move = transform.position;
        anim.SetFloat("horizontal", move.x);
        anim.SetFloat("vertical", move.y);

        if (move.x < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (move.x > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
