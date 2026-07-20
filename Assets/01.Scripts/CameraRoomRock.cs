using UnityEngine;
using System.Collections;
public class CameraRoomRock : Singleton<CameraRoomRock>
{
    
    #region Variable
    [SerializeField] Transform target;
    Camera camera;
    Coroutine cameraShake;
    Vector3 targetPos;
    Vector3 velocity;
    Vector3 originPos;
    #endregion

    private void Start()
    {
        camera = Camera.main;
        velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    public void CameraStop()
    {
        if (cameraShake != null)
        {
            StopCoroutine(cameraShake);
        }
        transform.position = originPos;
    }
}