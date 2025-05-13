using System.Collections;
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

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    private Health wizardHealth;

    private float spellSwitchCooldown = 1f;
    private float nextSpellSwitchTime = 0f;

    public int flaskCount = 1;
    public float healAmount = 5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wizardHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (Time.time >= nextSpellSwitchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchSpell(SpellType.Fireball);
            if (Input.GetKeyDown(KeyCode.Alpha2) && ShopManagerScript.Instance.iceBulletUnlocked)
                SwitchSpell(SpellType.IceBullet);
            if (Input.GetKeyDown(KeyCode.Alpha3) && ShopManagerScript.Instance.tornadoUnlocked)
                SwitchSpell(SpellType.Tornado);
            if (Input.GetKeyDown(KeyCode.Alpha4) && ShopManagerScript.Instance.lightningUnlocked)
                SwitchSpell(SpellType.Lightning);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            switch (currentSpell)
            {
                case SpellType.Fireball:
                    if (Time.time >= nextFireballTime)
                    {
                        CastFireball();
                        nextFireballTime = Time.time + fireballCooldown;
                    }
                    break;
                case SpellType.IceBullet:
                    if (Time.time >= nextIceBulletTime && ShopManagerScript.Instance.iceBulletUnlocked)
                    {
                        CastIceBullet();
                        nextIceBulletTime = Time.time + iceBulletCooldown;
                    }
                    break;
                case SpellType.Tornado:
                    if (Time.time >= nextTornadoTime && ShopManagerScript.Instance.tornadoUnlocked)
                    {
                        CastTornado();
                        nextTornadoTime = Time.time + tornadoCooldown;
                    }
                    break;
                case SpellType.Lightning:
                    if (Time.time >= nextLightningTime && ShopManagerScript.Instance.lightningUnlocked)
                    {
                        CastLightning();
                        nextLightningTime = Time.time + lightningCooldown;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && flaskCount > 0 && wizardHealth != null)
        {
            wizardHealth.TakeDamage(-healAmount); // heal
            flaskCount--;
            Debug.Log("Used healing flask. Remaining: " + flaskCount);
        }
    }

    void SwitchSpell(SpellType newSpell)
    {
        currentSpell = newSpell;
        nextSpellSwitchTime = Time.time + spellSwitchCooldown;

        Color flashColor = Color.white;
        switch (newSpell)
        {
            case SpellType.Fireball:
                flashColor = Color.red;
                break;
            case SpellType.IceBullet:
                flashColor = Color.blue;
                break;
            case SpellType.Tornado:
                flashColor = Color.gray;
                break;
            case SpellType.Lightning:
                flashColor = new Color(0.5f, 0f, 0.5f);
                break;
        }

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashColor(flashColor, 0.5f));
    }

    IEnumerator FlashColor(Color flashColor, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
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

    void CastLightning()
    {
        Instantiate(lightningPrefab, lightningSpawnPoint.position, lightningSpawnPoint.rotation, lightningSpawnPoint);
    }
}
