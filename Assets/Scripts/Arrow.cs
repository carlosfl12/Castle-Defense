using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector2 mousePosition;
    Player player;
    public AudioManager audioManager;
    public AudioSource audioClip;
    public enum ArrowType {
        Normal,
        Fire
    }
    public static ArrowType arrowType;
    public int arrowDamage = 3;
    public bool fireArrow = false;
    public float launchForce;
    // Start is called before the first frame update
    void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        Destroy(gameObject, 3f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioClip.volume = audioManager.soundsVolume;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (arrowType) {
            case ArrowType.Fire:
                fireArrow = true;
                break;
            case ArrowType.Normal:
                fireArrow = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.name.Contains("2")) {
            if (other.gameObject.CompareTag("Castle")) {
                Destroy(gameObject);
            }
            return;
        }
        
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.oiled && fireArrow) {
                enemy.RecieveDamage(45);
            } else {
                if (enemy.audioClip) {
                    enemy.audioClip.Play();
                }
                if ((arrowDamage + (int)launchForce / 10) < 1) {
                    enemy.RecieveDamage(1);
                } else {
                    enemy.RecieveDamage(arrowDamage + (int)launchForce / 10);
                }
            }
            Destroy(gameObject);
            GameManager.sharedInstance.SaveShot(mousePosition, other.gameObject.transform.position);
        }
    }
}
