using UnityEngine;
using Bomberman.Level;

using Bomberman.AI;
public class DestructibleWall : DestructibleObject
{

    protected GridPoint gridPositionOccuped;
    private Animator animator;

    private readonly int collisionAnimationId = Animator.StringToHash("Collision");
    private readonly int dieAnimationId = Animator.StringToHash("Die");

    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private ParticleSystem smokeDieParticles;


    private void Awake()
    {
        InitializeWallComponents();
    }


    protected void InitializeWallComponents()
    {
        TryGetComponent(out animator);
    }


    public void SetGridPoint(GridPoint point)
    {
        gridPositionOccuped = point;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        PlayCollisionEffects();
        if (life <= 0)
            EndWall();
    }

    protected virtual void PlayCollisionEffects()
    {
        Camera.main.GetComponent<CameraBehaviour>().CameraShake(0.08f, 0.12f);
        animator.SetTrigger(collisionAnimationId);
        collisionParticles.Play();
    }

    protected void EndWall()
    {
        GridController.LeaveFreeGrid(gridPositionOccuped.index);
        animator.SetTrigger(dieAnimationId);
        smokeDieParticles.Play();
    }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }
}


