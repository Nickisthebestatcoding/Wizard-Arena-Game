using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
    public GameObject iceBulletPrefab;
    public GameObject tornadoPrefab;

    public Transform firePoint;
    public Transform lightningSpawnPoint;
    public float fireForce = 10f;

    public float fireballCooldown = 0.5f;
    public float lightningCooldown = 1.5f;
    public float iceBulletCooldown = 1f;
    public float tornadoCooldown = 2f;

    private float nextFireballTime = 0f;
    private float nextLightningTime = 0f;
    private float nextIceBulletTime = 0f;
    private float nextTornadoTime = 0f;

    private enum SpellType { Fireball = 0, IceBullet = 2, Tornado = 3, Lightning = 4 }
    private SpellType currentSpell = SpellType.Fireball;

    private ShopManagerScript shopManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManagerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && IsSpellUnlocked(SpellType.Lightning))
            currentSpell = SpellType.Lightning;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Fireball is always unlocked
            currentSpell = SpellType.Fireball;

        if (Input.GetKeyDown(KeyCode.Alpha2) && IsSpellUnlocked(SpellType.IceBullet))
            currentSpell = SpellType.IceBullet;

        if (Input.GetKeyDown(KeyCode.Alpha3) && IsSpellUnlocked(SpellType.Tornado))
            currentSpell = SpellType.Tornado;

        if (Input.GetButtonDown("Fire1"))
        {
            if (!IsSpellUnlocked(currentSpell)) return;

            switch (currentSpell)
            {
                case SpellType.Fireball:
                    if (Time.time >= nextFireballTime)
                    {
                        CastFireball();
                        nextFireballTime = Time.time + fireballCooldown;
                    }
                    break;
                case SpellType.Lightning:
                    if (Time.time >= nextLightningTime)
                    {
                        CastLightning();
                        nextLightningTime = Time.time + lightningCooldown;
                    }
                    break;
                case SpellType.IceBullet:
                    if (Time.time >= nextIceBulletTime)
                    {
                        CastIceBullet();
                        nextIceBulletTime = Time.time + iceBulletCooldown;
                    }
                    break;
                case SpellType.Tornado:
                    if (Time.time >= nextTornadoTime)
                    {
                        CastTornado();
                        nextTornadoTime = Time.time + tornadoCooldown;
                    }
                    break;
            }
        }
    }

    bool IsSpellUnlocked(SpellType spell)
    {
        if (spell == SpellType.Fireball) return true;
        return shopManager.spellsUnlocked[(int)spell];
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

    void CastIceBullet()
    {
        GameObject iceBullet = Instantiate(iceBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    void CastTornado()
    {
        GameObject tornado = Instantiate(tornadoPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }
}
