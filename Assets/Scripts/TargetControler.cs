using Unity.Mathematics;
using UnityEngine;

public class TargetControler : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 1f;

    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<EnemyController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        // ClampRotation();
    }
    
    void AimWeapon()
    {
        if (target == null) return;

        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if(targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void FindClosestTarget()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        Transform closestTarget = null;
        float maxRange = Mathf.Infinity;

        foreach (EnemyController enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxRange)
            {
                closestTarget = enemy.transform;
                maxRange = targetDistance;
            }
        }

        target = closestTarget;
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

    //void ClampRotation()
    //{
    //    float clampedX = Mathf.Clamp(transform.rotation.x, 0f, 0f);
    //    float clampedY = Mathf.Clamp(transform.rotation.y, -360f, 360f);
    //    float clampedZ = Mathf.Clamp(transform.rotation.z, 0f, 0f);

    //    transform.rotation = new Quaternion.Euler //(clampedX, clampedY, clampedZ);
    //}
}
