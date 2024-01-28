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
    public float bulletLife = 5f; //DONT CHANGE THIS
    public float speed = 5f; //DONT CHANGE THIS 

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float fireRate = 1f; //2 for straight, 0.5-1 for others
    [SerializeField] private int numberOfBullets; //3-4 for straight, 8-10 for spin, 3-6 for spread
    [SerializeField] private float rotationSpeed = 1f; //i put it as 0.2 in the inspector

    [SerializeField] private int firingTime = 5; //time interval between firing modes

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float firingTimer = 0f;
    private int timeInSeconds;
    // Start is called before the first frame update
    void Start(){
        // SpawnerState = (SpawnerType)Random.Range(0, Enum.GetNames(typeof(SpawnerType)).Length);
        SpawnerState = (SpawnerType)Random.Range(1, 4);
        Debug.Log((int)SpawnerType.Spread);
    }

    // Update is called once per frame
    void Update(){
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
            SpawnerState = (SpawnerType)Random.Range(1, 4);
            switch(SpawnerState){
                case SpawnerType.Straight:
                    // numberOfBullets = Random.Range(3,5);
                    numberOfBullets = 1;
                    // fireRate = Random.Range(0.5f,1f);
                    fireRate = 0.25f;
                break;

                case SpawnerType.Spin:
                    numberOfBullets = Random.Range(8,10);
                    fireRate = Random.Range(0.2f,0.3f);
                break;

                case SpawnerType.Spread:
                    numberOfBullets = Random.Range(3,6);
                    fireRate = Random.Range(0.3f,0.5f);
                break;
            }
            firingTimer = 0;
        }

    }

    private IEnumerator FireStraight(float burstTime){
        //burst fire, num bullets is burst counter
        WaitForSeconds wait = new WaitForSeconds(burstTime);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for(int i = 0; i< numberOfBullets; i++){
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, CalculateAngleToMouse(mousePosition)));
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.tag = "Bullet";
            yield return wait;
        }
    }

    private void FireSpin(){
        for(int i=0;i < numberOfBullets; i++){
            if(bullet){
                float angle = i * (360f / numberOfBullets);
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle + transform.eulerAngles.z));;
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.tag = "Bullet";
            }
        }
    }

    private void FireSpread(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        for (int i = 0; i < numberOfBullets; i++){
            if (bullet){
                float angle;
                if(numberOfBullets % 2 == 0){
                    angle = CalculateAngleToMouse(mousePosition) + (i-numberOfBullets/2) * 15f + 7.5f;
                }
                else{
                    angle = CalculateAngleToMouse(mousePosition) + (i-numberOfBullets/2) * 15f;     
                }
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.tag = "Bullet";
            }
        }
    }

    private float CalculateAngleToMouse(Vector3 mousePosition){
        Vector2 direction = mousePosition - transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}