using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Camera cam;
    PlayerMotor motor;

    public LayerMask movementMask;

    Vector3 playerPoint, currentPoint;

    public Interactable focus;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        playerPoint = GameObject.Find("Player").transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                motor.MoveToPoint(hit.point);
                currentPoint = hit.point;

                RemoveFocus();
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

            Debug.Log(playerPoint);
        }

        if (playerPoint == currentPoint)
        {
            Debug.Log(playerPoint.y - .9f);
        }

        if ((playerPoint.x == currentPoint.x) && (playerPoint.z == currentPoint.z)) {
            motor.stopMovement();
        }


    }


    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus) {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();

        }

        focus = null;
        motor.StopFollowingTarget();
    }

}
