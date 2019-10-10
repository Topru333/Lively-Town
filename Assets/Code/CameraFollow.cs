using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 3.5f;
    public float add_x = 7, add_y = 7;

    void LateUpdate () {
        Vector3 desiredPosition = target.position + new Vector3(0, add_x, -add_y);
        Vector3 smootherdPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smootherdPosition;
        transform.LookAt(target);
        //Vector3 displacementFromTarget = SmoothVector(target.position + new Vector3(0, add_x, -add_y) - transform.localPosition);
        //Vector3 directionToTarget = displacementFromTarget.normalized;
        //Vector3 velocity = directionToTarget * speed;

        //transform.Translate(velocity * Time.deltaTime, Space.World);

        //transform.LookAt(target);
    }

    private Vector3 SmoothVector (Vector3 vector) {
        float n = 1f;
        return new Vector3(Mathf.Abs(vector.x) < n ? 0 : vector.x, Mathf.Abs(vector.y) < n ? 0 : vector.y, Mathf.Abs(vector.z) < n ? 0 : vector.z);
    }
}
