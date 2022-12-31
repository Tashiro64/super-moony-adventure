using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
    [SerializeField] private Transform target;
    [SerializeField] [Range(0.01f,1f)] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 newOffset;
    private Vector3 velocity = Vector3.zero;
    public static GameObject CameraBoundaries;

    void Start()
    {

        CameraBoundaries = GameObject.Find("/Globals/CameraBoundaries");

    }

    void FixedUpdate()
    {

        if(!Config.fnc_DeadCoroutine){
            if(Movement.facingDirection == 1){
                newOffset = new Vector3(offset.x, offset.y, transform.position.z);
            } else {
                newOffset = new Vector3((offset.x-0.6f), offset.y, transform.position.z);
            }

            Vector3 desiredPosition = target.position + newOffset;

            desiredPosition = new Vector3( Mathf.Clamp(desiredPosition.x, CameraBoundaries.transform.position.x, 1000f) , desiredPosition.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
    }

}