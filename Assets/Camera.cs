using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x=player.position.x;
        float y=player.position.y;
        float z=transform.position.z;
        transform.position=new Vector3(x,y,z);
    }
}
