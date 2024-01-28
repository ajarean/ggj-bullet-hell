//BulletSpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { init, Straight, Spin, Spread } //0, 1, 2, 3
    SpawnerType SpawnerState = SpawnerType.init;

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f; 

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int numberOfBullets;
    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private int firingTime = 20;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float firingTimer = 0f;
    private int timeInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        SpawnerState = SpawnerType.Straight;
        // Debug.Log((int)SpawnerType.init); 0
        // Debug.Log((int)SpawnerType.Straight); 1
        // Debug.Log((int)SpawnerType.Spin); 2
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(SpawnerState == SpawnerType.Spin){
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+rotationSpeed);
        }
        if(timer >= fireRate){
            switch(SpawnerState){
                case SpawnerType.Straight:
                    StartCoroutine(FireStraight(0.1f));
                break;

                case SpawnerType.Spin:
                    FireSpin();
                break;

                case SpawnerType.Spread:
                    FireSpread();
                break;
            }
            timer = 0;
        }

        firingTimer += Time.deltaTime;
        //Debug.Log(firingTimer%60); //THIS GIVES TIME IN SECONDS
        if(firingTimer%60 >= firingTime){
            //assign a new SpawnerType
            //Debug.Log("TIME TO SWITCH");
        }

    }

    private void FireSpin(){
        for(int i=0;i < numberOfBullets; i++){
            if(bullet){
                float angle = i * (360f / numberOfBullets);
                // Debug.Log("angle " + i + " is " + angle);
                // spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
                // spawnedBullet.GetComponent<Bullet>().speed = speed;
                // spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                // spawnedBullet.transform.rotation = transform.rotation;
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle + transform.eulerAngles.z));;
        
                //Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            }
        }
    }

    private void FireSpread()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        for (int i = 0; i < numberOfBullets; i++)
        {
            if (bullet)
            {
                float angle;
                if(numberOfBullets % 2 == 0){
                    angle = CalculateAngleToMouse(mousePosition) + (i-numberOfBullets/2) * 10f + 5f;
                }
                else{
                    angle = CalculateAngleToMouse(mousePosition) + (i-numberOfBullets/2) * 10f;     
                }
                 // Adjust the angle increment as needed
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            }
        }
    }

    private float CalculateAngleToMouse(Vector3 mousePosition)
    {
        Vector2 direction = mousePosition - transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private IEnumerator FireStraight(float burstTime){
        //burst fire, num bullets is burst count
        Debug.Log("firestraight called");
        WaitForSeconds wait = new WaitForSeconds(burstTime);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for(int i = 0; i< numberOfBullets; i++){
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, CalculateAngleToMouse(mousePosition)));
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            //yield return new WaitForSeconds(burstTime);
            yield return wait;
        }
        //this spawns them all in a stack, we need to wait 
    }
}