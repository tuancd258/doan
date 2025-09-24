using UnityEngine;

public class MoveEntity : MonoBehaviour
{ public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToplayer { get; private set; }
    [SerializeField]
    private float _PlayerAwarenessDistance;
    private Transform _Player;
    private void Awake()
    {
        _Player= FindAnyObjectByType<Move>().transform;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        Vector2 directionToPlayer = _Player.position - transform.position;
        DirectionToplayer = directionToPlayer.normalized;
        if (directionToPlayer.magnitude <= _PlayerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
