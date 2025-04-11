using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightbehavior : MonoBehaviour
{
    public float moveSpeed = 2.1f;
    public float chaseRange = 15.0f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public float damage = 1f;

    private Transform wizardTransform;
    private Animator animator;
    private float lastAttackTime;



    // Start is called before the first frame update
    private void Start()
    {
        GameObject wizard = GameObject.FindGameObjectWithTag("Wizard");

        if (wizard != null)
        {
            wizardTransform = wizard.transform;
        }
        else
        {
            Debug.LogError("Wizard not found in scene. Make sure it's tagged as 'Wizard'.");
        }

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (wizardTransform == null) return;

        Vector3 direction = wizardTransform.position - transform.position;
        direction.z = 0f;

        float distanceToWizard = direction.magnitude;

        if (distanceToWizard <= attackRange)
        {
            // Stop moving & attack
            animator.SetBool("isAttacking", true);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                // Attack animation will trigger damage via animation event
            }
        }
        else if (distanceToWizard <= chaseRange)
        {
            animator.SetBool("isAttacking", false);

            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 270;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

    }
    public void DealDamage()
    {
        if (wizardTransform == null) return;

        float distance = Vector3.Distance(transform.position, wizardTransform.position);
        if (distance <= attackRange)
        {
            Health wizardHealth = wizardTransform.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }
        }
    }
}
