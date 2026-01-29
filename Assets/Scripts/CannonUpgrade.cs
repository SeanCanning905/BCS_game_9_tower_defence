using UnityEngine;

public class CannonUpgrade : MonoBehaviour
{
    [SerializeField] ParticleSystem cannonBall;

    bool isCannon = true;

    void Start()
    {
        cannonBall = GetComponentInChildren<ParticleSystem>();
    }
    void OnMouseDown()
    {
        var trailsModule = cannonBall.trails;
        trailsModule.enabled = true;
    }
}
