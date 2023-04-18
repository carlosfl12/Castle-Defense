using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage, launchForce;
    public int health;
    public int maxHealth;
    public int goldToGive;
    public float speed = 5;
    public bool oiled;
    public GameObject arrowPrefab;
    public GameObject[] waypoints;
    public Transform waypoint;
    public Castle castle;
    private GameObject shotPosition;
    public Healthbar healthbar;
    private float timeToShot;
    private EnemyGenerator enemyGenerator;
    public SpriteRenderer sprite;
    public ParticleSystem particle;
    public AudioManager audioManager;
    public AudioSource audioClip;
    public bool isTutorial;
    public bool isRetreating;
    public bool isDead;
    public AudioClip deathSound;

    private void Awake() {
        if (isTutorial) {
            return;
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (particle == null) {
            return;
        } else {
            particle.Stop();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        enemyGenerator = GameObject.FindGameObjectWithTag("EnemyGenerator").GetComponent<EnemyGenerator>();
        health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject stopPoint in waypoints) {
            if (stopPoint.name.StartsWith(gameObject.name[0]) && transform.position.y == stopPoint.transform.position.y) {
                waypoint = stopPoint.transform;
            }
        }
        castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
        if (isTutorial) {
            return;
        }
        if (audioClip) {
            audioClip.volume = audioManager.soundsVolume;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorial || isDead) {
            return;
        }
        
        timeToShot += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, waypoint.position);

        if (GameManager.sharedInstance.enemiesDefeated >= 50) {
            isRetreating = true;
            GameManager.sharedInstance.win = true;
            //Hacerlo más optimo
            GameManager.sharedInstance.LoadScene(2);
        }
        
        if (distance > 0.5f && !isRetreating) {
            transform.position += transform.right * speed * Time.deltaTime;
        } else if (distance < 0.5f && !isRetreating){
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (timeToShot > 2) {
                timeToShot = 0;
                Attack();
            }
        }
        if (isRetreating) {
            Retreat();
        }
    }


    void Attack() {
        if (gameObject.name.StartsWith("A")) {
            Shoot();
        }
        castle.RecieveDamage(damage);
    }

    void Shoot() {
        // transform.LookAt(shotPosition.transform.position);
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Vector2 shootTarget = new Vector2(0,0);
        shootTarget = (Vector2)GameManager.sharedInstance.archers[Random.Range(0, GameManager.sharedInstance.archers.Count)].transform.position;
        Vector2 direction = (shootTarget - (Vector2)transform.position).normalized;
        // arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(shootTarget.x, shootTarget.y).normalized * launchForce;
        arrow.GetComponent<Rigidbody2D>().AddForce(direction * launchForce);
    }

    void Retreat() {
        transform.position -= transform.right * speed * Time.deltaTime;
        sprite.flipX = true;
    }
    public void RecieveDamage(int amount) {
        if (isDead) {
            return;
        }
        if (gameObject.name.StartsWith("B") && health <= (maxHealth * 0.5)) {
            particle.Play();
        }
        health -= amount;
        healthbar.SetHealth(health, maxHealth);
        if (audioClip) {
            audioClip.Play();
        }
        if (health <= 0) {
            isDead = true;
            GameManager.sharedInstance.enemiesDefeated++;
            GameManager.sharedInstance.MoveSlider();
            GameManager.sharedInstance.gold += goldToGive;
            GameManager.sharedInstance.RemoveEnemy(gameObject);
            if (gameObject.name.StartsWith("B")) {
                enemyGenerator.hasRam = false;
            }
            Invoke("CallCoroutine", 0.1f);
        }
    }

    void CallCoroutine() {
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        //reproducir sonido de muerte
        if (!gameObject.name.StartsWith("B")) {
            transform.Rotate(new Vector3(0,0,-90), Space.World);
            audioClip.clip = deathSound;
            audioClip.Play();
        }
        //animacion de que se tire al suelo y se gire
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    } 
}
