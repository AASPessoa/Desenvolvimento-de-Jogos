using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform downEdge;
    [SerializeField] private Transform upEdge;

    private Transform player;
    private float halfWidth;
    private float halfHeight;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(FindAnyObjectByType<GameControlle>().jogoativo == false){
            return;
        }
        
        float cameraLeft = transform.position.x - halfWidth;
        float cameraDown = transform.position.y - halfHeight;
        float regionLeft = cameraLeft + halfWidth * 0.8f;
        float regionRight = cameraLeft + halfWidth * 1.1f;
        float regionUp = cameraDown + halfHeight * 1.5f;
        float regionDown = cameraDown + halfHeight * 0.5f;
        float dx = 0;
        float dy = 0;

        if(player.transform.position.x < regionLeft)
        {
            dx = player.transform.position.x - regionLeft;
        }
        else if(player.transform.position.x > regionRight)
        {
            dx = player.transform.position.x - regionRight;
        }

        if(player.transform.position.y < regionDown)
        {
            dy = player.transform.position.y - regionDown;
        }
        else if(player.transform.position.y > regionUp)
        {
            dy = player.transform.position.y - regionUp;
        }

        float x = Mathf.Clamp(transform.position.x + dx, leftEdge.position.x + halfWidth, rightEdge.position.x - halfWidth);
        float y = Mathf.Clamp(transform.position.y + dy, downEdge.position.y + halfHeight, upEdge.position.y - halfHeight);
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }

    
}
