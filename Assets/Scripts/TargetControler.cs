using UnityEngine;

public class TargetControler : MonoBehaviour
{
    [SerializeField] Transform weapon;
    Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<EnemyController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }
    
    void AimWeapon()
    {
        weapon.LookAt(target);
    }
}
