//BulletSpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int numberOfBullets = 1;

    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin){
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+1f);
        }
        if(timer >= fireRate){
            Fire();
            timer = 0;
        }
    }

    private void Fire(){
        for(int i=0;i < numberOfBullets; i++){
            if(bullet){
                float angle = i * (360f / numberOfBullets);
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, angle));
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.transform.rotation = transform.rotation;
                spawnedBullet.tag = "Bullet";
            }
        }

    }
}
