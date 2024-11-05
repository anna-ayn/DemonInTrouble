using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerScript : MonoBehaviour
{
    public Animator animator;
    public Agent character;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("running", character.kinematic.velocity.magnitude > 0);
        
    }
}
