using UnityEngine;
public class CameraRoomRock : Singleton<CameraRoomRock>
{

    #region Variable
    [SerializeField] Transform target;
    Vector3 transRoomPos;
    Camera camera;
    #endregion

    private void Awake()
    {
        camera = Camera.main;
    }

    protected override void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        transRoomPos = new Vector3(target.position.x + 8, target.position.y - 5.5f, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, transRoomPos, 2f * Time.deltaTime);
    }
}