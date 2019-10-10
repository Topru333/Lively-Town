using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float walk_speed = 6f;
    public float run_speed = 6f;

    float Speed {
        get {
            if (Input.GetKey(KeyCode.LeftShift)) {
                return run_speed;
            } else {
                return walk_speed;
            }
        }
    }

    Vector3 movement;
    Animator anim;
    Rigidbody rigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake () {
        floorMask = LayerMask.GetMask("Floor");
        anim = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Animating(h, v);
        Turning();
    }

    void Move (float h, float v) {
        movement.Set(h, 0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);
    }

    void Turning () {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigidbody.MoveRotation(newRotation);
        }
    }

    void Animating (float h, float v) {
        bool walk = (h != 0f || v != 0f);
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", Input.GetKey(KeyCode.LeftShift));

        anim.SetBool("SimpleAttack", Input.GetMouseButton(0));
        
    }
}
