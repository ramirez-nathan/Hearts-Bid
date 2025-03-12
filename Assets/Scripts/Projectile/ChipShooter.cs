using UnityEngine;

public class ChipShooter : MonoBehaviour
{
    [SerializeField] GameObject chipProjectile;
    [SerializeField] float projectileForce = 100f;
    [SerializeField] float delayBetweenShots = 2.0f;

    float timeElapsed = 0f;

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= delayBetweenShots)
        {
            ShootChip();
            timeElapsed = 0f;
        }
    }

    public void ShootChip()
    {
        this.transform.GetPositionAndRotation(out var pos, out var rot);
        ChipProjectile chip = Instantiate(chipProjectile, pos, rot).GetComponent<ChipProjectile>();
        chip.Fire(projectileForce);
    }
}
