using UnityEngine;

public class test1 : MonoBehaviour
{
    private Transform player;
    private float pickupRange = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.transform;
    }

    // Update is called once per frame
    void Update()
    {

       float distan=Vector3.Distance(player.position,transform.position);
        if (distan < pickupRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
        }
        if (distan < 0.1f)
        {
            Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(gameObject);
        }
    }
}
