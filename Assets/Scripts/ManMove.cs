using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMove : MonoBehaviour
{
    public GameObject body;
    public GameObject hammer;
    public float MaxDistance;
    float RelativeDistance;

    Rigidbody body_rig;
    Rigidbody hammer_rig;
    Transform hammer_anchor;

    // Start is called before the first frame update
    void Start()
    {
        body_rig = body.GetComponent<Rigidbody>();
        hammer_rig = hammer.GetComponent<Rigidbody>();
        hammer_anchor = hammer.transform.GetChild(2);
    }

    private void FixedUpdate()
    {
        HammerControl();
    }

    void HammerControl()
    {
        Vector2 MousePosition = GetConfinedPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        hammer_rig.velocity = (new Vector3(MousePosition.x, MousePosition.y, hammer.transform.position.z) - hammer_anchor.position) * 10;
        if (hammer.GetComponent<CollisionDetection>().IsCollision)
        {
            BodyControl(hammer_rig.velocity);
        }
        Vector3 direction = (new Vector3(MousePosition.x, MousePosition.y, hammer_anchor.position.z) - new Vector3(body.transform.position.x, body.transform.position.y, hammer_anchor.position.z)).normalized;
        hammer.transform.RotateAround(hammer_anchor.position, Vector3.Cross(hammer_anchor.up, direction), Vector3.Angle(hammer_anchor.up, direction));
    }

    void BodyControl(Vector3 velociy)
    {
        body_rig.velocity = -velociy;
    }

    Vector2 GetConfinedPosition(Vector2 mouseposition)
    {
        Vector2 Confined_MousePosition;
        Vector2 body_position = new Vector2(body.transform.position.x, body.transform.position.y);

        RelativeDistance = Vector2.Distance(mouseposition, body_position);

        if (RelativeDistance > MaxDistance)
        {
            Confined_MousePosition = (mouseposition - body_position).normalized * MaxDistance + body_position;
        }
        else
        {
            Confined_MousePosition = mouseposition;
        }
        return Confined_MousePosition;
    }
}
