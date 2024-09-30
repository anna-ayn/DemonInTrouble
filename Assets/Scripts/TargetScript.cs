using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Agent target;
    public float speed;
    public Animator anim;

    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.Translate(move.normalized * speed * Time.deltaTime);

        target.updateKinematicWithTransform();

        anim.SetFloat("horizontal", move.x);
        anim.SetFloat("vertical", move.y);

        if (move.x < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (move.x > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
