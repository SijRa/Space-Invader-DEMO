using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 StartPosition = new Vector3(-8, -10, -10);

    float LerpDistance = 4f;
    float LerpTime = 100f;
    float CameraSpeed = 10f;

    Vector2 panLimit = new Vector2(9,11);

    float PanBoarderThickness = 20f;

    // Use this for initialization
    void Start()
    {
        transform.position = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = transform.position;
        Vector3 pos = transform.position;
        Vector3 targetPosition = new Vector3(pos.x,pos.y,pos.z);
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBoarderThickness)
        {
            pos.y += CameraSpeed * Time.deltaTime;
            targetPosition = new Vector3(pos.x,pos.y+LerpDistance,pos.z);
            
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= PanBoarderThickness)
        {
            pos.y -= CameraSpeed * Time.deltaTime;
            targetPosition = new Vector3(pos.x, pos.y - LerpDistance, pos.z);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBoarderThickness)
        {
            pos.x += CameraSpeed * Time.deltaTime;
            targetPosition = new Vector3(pos.x + LerpDistance, pos.y + LerpDistance, pos.z);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= PanBoarderThickness)
        {
            pos.x -= CameraSpeed * Time.deltaTime;
            targetPosition = new Vector3(pos.x - LerpDistance, pos.y, pos.z);
        }
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
        Vector3.Lerp(transform.position,targetPosition,LerpTime);
        transform.position = pos;
    }
}
