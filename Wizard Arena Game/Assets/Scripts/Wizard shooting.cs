using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;

    public Transform firePoint;           // Used for fireball
    public Transform lightningSpawnPoint; // New one for lightning

    public float fireForce = 10f;

    private enum SpellType { Fireball, Lightning }
    private SpellType currentSpell = SpellType.Fireball;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpell = SpellType.Lightning;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpell = SpellType.Fireball;

        if (Input.GetButtonDown("Fire1"))
            CastSpell();
    }

    void CastSpell()
    {
        if (currentSpell == SpellType.Fireball)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = firePoint.up * fireForce;
        }
        else if (currentSpell == SpellType.Lightning)
        {
            GameObject lightning = Instantiate(lightningPrefab, lightningSpawnPoint.position, lightningSpawnPoint.rotation, lightningSpawnPoint);
        }
    }
}
