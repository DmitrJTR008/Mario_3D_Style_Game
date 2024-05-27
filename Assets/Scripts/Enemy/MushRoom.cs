using UnityEngine;

public class MushRoom : MonoBehaviour
{
    private bool _isDead;
    private float _moveSpeed;
    private Vector3 _directionMove;
    private Transform _eyeTransform;

    private Player _lastPlayer;

    private void Start()
    {
        _directionMove = Random.Range(0, 2) == 0 ? Vector3.back : Vector3.forward;
        _moveSpeed = 1;
        _eyeTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if (_isDead) return;

        transform.position += (_directionMove * (_moveSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Euler(0, _directionMove.z >= 0 ? 0 : -180, 0);

        if (Physics.Raycast(_eyeTransform.position, _eyeTransform.forward, out RaycastHit hit, .1f))
        {
            if (hit.collider.TryGetComponent(out Player player)) return;

            _directionMove *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead) return;

        if (other.TryGetComponent(out Player player))
        {
            float normalDotToDamage = 0.43f;
            _lastPlayer = player;
            
            if (player.IsImmortal()) return;

            Vector3 collisionDirection = other.transform.position - transform.position;
            if (collisionDirection.normalized.y < normalDotToDamage)
                player.AcceptDamage();

            KillEnemy();
        }
    }

    void KillEnemy()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        _lastPlayer.ForceJump(1.5f);
        _isDead = true;
        Invoke("DestroyEnemy", 1);
    }
    void DestroyEnemy()
    {
        gameObject.SetActive(false);
    }
}
