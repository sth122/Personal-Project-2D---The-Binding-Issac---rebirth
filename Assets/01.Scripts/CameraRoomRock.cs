using UnityEngine;
public class CameraRoomRock : Singleton<CameraRoomRock>
{

    #region Variable
    Vector3 transRoomPos;
    Camera mainCamera;
    private float halfWidth;
    private float halfHeight;
    #endregion

    private void Awake()
    {
        mainCamera = Camera.main;

        mainCamera.aspect = 17f / 11f;

        halfHeight = 8f;
        halfWidth = 5.5f;
        
        transform.position = new Vector3(halfHeight, -halfWidth, -10f);
    }

    protected override void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transRoomPos, 2f * Time.deltaTime);
    }

    public void SetCameraPosition(Transform target)
    {
        transRoomPos = new Vector3(target.position.x + halfHeight, target.position.y - halfWidth, transform.position.z);

    }
}