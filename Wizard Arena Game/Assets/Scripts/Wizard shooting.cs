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

    private enum SpellType { Fireball = 0, IceBullet = 1, Tornado = 2, Lightning = 3 }
    private SpellType currentSpell = SpellType.Fireball;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    private Health wizardHealth;

    private float spellSwitchCooldown = 1f;
    private float nextSpellSwitchTime = 0f;

    public int flaskCount = 1;  // Start with one healing flask
    public float healAmount = 5f;

    // Tracks which spells are unlocked (default Fireball is unlocked)
    private bool[] unlockedSpells = new bool[4];

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wizardHealth = GetComponent<Health>();

        // Initially unlock Fireball
        unlockedSpells[(int)SpellType.Fireball] = true;
    }

    void Update()
    {
        if (Time.time >= nextSpellSwitchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && unlockedSpells[(int)SpellType.Lightning])
                SwitchSpell(SpellType.Lightning);

            if (Input.GetKeyDown(KeyCode.Alpha1) && unlockedSpells[(int)SpellType.Fireball])
                SwitchSpell(SpellType.Fireball);

            if (Input.GetKeyDown(KeyCode.Alpha2) && unlockedSpells[(int)SpellType.IceBullet])
                SwitchSpell(SpellType.IceBullet);

            if (Input.GetKeyDown(KeyCode.Alpha3) && unlockedSpells[(int)SpellType.Tornado])
                SwitchSpell(SpellType.Tornado);
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

        if (Input.GetKeyDown(KeyCode.Q) && flaskCount > 0 && wizardHealth != null)
        {
            wizardHealth.TakeDamage(-healAmount); // Negative damage = healing
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
                flashColor = Color.grey;
                break;
            case SpellType.Lightning:
                flashColor = new Color(0.5f, 0f, 0.5f); // Purple
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

    // Call this method when a player buys a book in the shop
    public void UnlockSpell(int spellIndex)
    {
        if (spellIndex >= 0 && spellIndex < unlockedSpells.Length)
        {
            unlockedSpells[spellIndex] = true;
            Debug.Log("Spell unlocked: " + (SpellType)spellIndex);
        }
    }
}
