using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool hasPoweup = false;
    public GameObject powerupIndicator;

    private float powerUpStrength = 15;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);
        powerupIndicator.transform.position = transform.position - new Vector3(0,0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPoweup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPoweup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPoweup)
        {
            GameObject enemyGameObject = collision.gameObject;
            Rigidbody enemyRb = enemyGameObject.GetComponent<Rigidbody>();
            Vector3 pushDirection = enemyGameObject.transform.position - transform.transform.position;

            enemyRb.AddForce(pushDirection * powerUpStrength, ForceMode.Impulse);
        }
    }
}
