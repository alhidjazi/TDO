using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]

    public float range = 15f;
    private Transform target;
    private Enemy targertEnemy;

    [Header("Use Bullets(default)")]

    public GameObject bulletPrefab;
   
    private float fireCountdown = 0f;
    public float fireRate = 1f;

    [Header("Use Laser")]

    public bool useLaser;
    public int damageOverTime = 30;
    //pr ralentir l'ennemi par laser de 50%
    public float slowAmount = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity setup fields")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    private float turnSpeed = 6.5f;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        //enemy les plus proche
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targertEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target  == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
               lineRenderer.enabled = false;
               impactEffect.Stop();
               impactLight.enabled = false;
            }
            return;

        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            //la foction qui nous permet de tirer
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        
    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targertEnemy.TakeDammage(damageOverTime * Time.deltaTime);
        targertEnemy.Slow(slowAmount);

        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        impactEffect.transform.position = target.position + dir.normalized * 1.5f;
    }
    void Shoot()
    {
        //Debug.Log("Tir effectue");
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
