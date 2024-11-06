using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPetScript : MonoBehaviour
{
    public Animator animator;
    public Agent character;
    public Agent target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attack", Vector3.Distance(character.kinematic.position, target.kinematic.position) < 1.5f);
        
    }
}
