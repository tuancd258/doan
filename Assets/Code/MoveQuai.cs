using UnityEngine;

public class MoveQuai : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody2D rb;
    private MoveEntity _PlayerAware;
    private Vector2 _targetDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _PlayerAware = GetComponent<MoveEntity>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_PlayerAware != null && _PlayerAware.AwareOfPlayer)
            _targetDirection = _PlayerAware.DirectionToplayer;
        else
            _targetDirection = Vector2.zero;
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero) return;

        float angle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f;
        float step = rotationSpeed * Time.deltaTime;
        float newAngle = Mathf.LerpAngle(rb.rotation, angle, step);
        rb.rotation = newAngle;
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
            rb.linearVelocity = Vector2.zero;
        else
            rb.linearVelocity = transform.up * speed;
    }
}
