using System.Collections;
using UnityEngine;

public class TornadoProjectile : MonoBehaviour
{
    public float speed = 2f;
    public float knockbackDistance = 3f;
    public float knockbackSpeed = 10f;  // How fast the wizard is pushed
    public float stunAfterSlide = 0.3f; // How long to wait after slide before re-enabling movement

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            WizardScript wizard = other.GetComponent<WizardScript>();
            if (wizard != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                StartCoroutine(SlideWizardBack(wizard, knockbackDirection));
            }
        }

        Destroy(gameObject); // Destroy the tornado on collision
    }

    private IEnumerator SlideWizardBack(WizardScript wizard, Vector2 direction)
    {
        wizard.canMove = false;

        Vector3 startPos = wizard.transform.position;
        Vector3 targetPos = startPos + (Vector3)(direction * knockbackDistance);

        float distance = Vector3.Distance(startPos, targetPos);
        float elapsed = 0f;

        while (Vector3.Distance(wizard.transform.position, targetPos) > 0.05f)
        {
            wizard.transform.position = Vector3.MoveTowards(wizard.transform.position, targetPos, knockbackSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to final position
        wizard.transform.position = targetPos;

        yield return new WaitForSeconds(stunAfterSlide);
        wizard.canMove = true;
    }
}
