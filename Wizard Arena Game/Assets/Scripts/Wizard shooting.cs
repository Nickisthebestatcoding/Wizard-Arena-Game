using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
    public GameObject iceBulletPrefab;  // New ice bullet prefab

    public Transform firePoint;
    public Transform lightningSpawnPoint;

    public float fireForce = 10f;

    public float fireballCooldown = 0.5f;
    public float lightningCooldown = 1.5f;
    public float iceBulletCooldown = 1f;  // Cooldown for ice bullet

    private float nextFireballTime = 0f;
    private float nextLightningTime = 0f;
    private float nextIceBulletTime = 0f;  // Time for the next ice bullet cast

    private enum SpellType { Fireball, Lightning, IceBullet }  // Added IceBullet to the enum
    private SpellType currentSpell = SpellType.Fireball;

    void Update()
    {
        // Spell switching logic
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpell = SpellType.Lightning;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpell = SpellType.Fireball;

        if (Input.GetKeyDown(KeyCode.Alpha3))  // Press 3 for IceBullet
            currentSpell = SpellType.IceBullet;

        // Casting logic
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentSpell == SpellType.Fireball && Time.time >= nextFireballTime)
            {
                CastFireball();
                nextFireballTime = Time.time + fireballCooldown;
            }
            else if (currentSpell == SpellType.Lightning && Time.time >= nextLightningTime)
            {
                CastLightning();
                nextLightningTime = Time.time + lightningCooldown;
            }
            else if (currentSpell == SpellType.IceBullet && Time.time >= nextIceBulletTime)  // Ice Bullet Cast
            {
                CastIceBullet();
                nextIceBulletTime = Time.time + iceBulletCooldown;
            }
        }
    }

    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    void CastLightning()
    {
        Instantiate(lightningPrefab, lightningSpawnPoint.position, lightningSpawnPoint.rotation, lightningSpawnPoint);
    }

    void CastIceBullet()  // Ice Bullet Cast Method
    {
        GameObject iceBullet = Instantiate(iceBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;  // You can adjust the force if needed
    }
}
