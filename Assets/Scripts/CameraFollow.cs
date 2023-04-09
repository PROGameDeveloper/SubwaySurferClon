using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Transform m_Transform;
    private Vector3 cameraOffset;
    private Vector3 followPosition;
    [SerializeField] private float rayDistance;
    [SerializeField] private float speedOffset;
    float y;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        m_Transform = GetComponent<Transform>();
        cameraOffset = m_Transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        followPosition = target.position + cameraOffset;
        m_Transform.position = followPosition;
        UpdateCameraOffset();
    }

    private void UpdateCameraOffset()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(target.position, Vector3.down, out hitInfo, rayDistance))
        {
            y = Mathf.Lerp(y, hitInfo.point.y, Time.deltaTime * speedOffset);
        }
        //else y = Mathf.Lerp(y, target.position.y, Time.deltaTime * speedOffset);
        followPosition.y = cameraOffset.y + y;
        m_Transform.position = followPosition;
    }
}
