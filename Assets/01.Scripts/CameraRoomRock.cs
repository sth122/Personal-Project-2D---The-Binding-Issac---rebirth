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
        isDDOL = false;
        base.Awake();

        halfWidth = 8.5f;
        halfHeight = 5.5f;

        mainCamera = Camera.main;

        mainCamera.aspect = 17f / 11f;
    }

    private void Start()
    {
        mainCamera.transform.position = new Vector3(halfWidth, halfHeight, -10f);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transRoomPos, 1f);
    }

    public void SetCameraPosition(Transform target)
    {
        transRoomPos = new Vector3(target.position.x + halfWidth, target.position.y + halfHeight, -10f);
    }
}