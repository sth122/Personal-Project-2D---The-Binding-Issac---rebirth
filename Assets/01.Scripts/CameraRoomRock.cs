using UnityEngine;
public class CameraRoomRock : Singleton<CameraRoomRock>
{

    #region Variable
    Vector3 transRoomPos;
    Camera mainCamera;
    private float halfWidth;
    private float halfHeight;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        mainCamera = Camera.main;

        mainCamera.aspect = 17f / 11f;
    }
    protected override void Initialize()
    {
        halfHeight = 8.5f;
        halfWidth = 5.5f;

        transform.position = new Vector3(halfHeight, halfWidth, -10f);
    }


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transRoomPos, 2f * Time.deltaTime);
    }

    public void SetCameraPosition(Transform target)
    {
        transRoomPos = new Vector3(target.position.x + halfHeight, target.position.y + halfWidth, transform.position.z);

    }
}