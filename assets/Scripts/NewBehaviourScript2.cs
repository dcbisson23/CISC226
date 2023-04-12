using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{


    public float moveSpeed = 200;
    public float jumpStrength = 400;
    public float upwardMoveForce = 100;
    public float yClingForce = 100;
    public float layerChangeFrameTime = 5;

    private SpriteRenderer spi;
    private CircleCollider2D playerCollider;
    private Rigidbody2D rb2d;

    private ContactFilter2D contactFilter;
    private List<ContactPoint2D> currentSurfaces = new List<ContactPoint2D>();
    private int floorContacts;
    private ContactPoint2D primaryContact;
    private Vector2 surfaceNorm;
    private float surfaceAngle;

    private bool jumpReady = false;
    private bool isGrounded = false;
    private bool grabFlag = false;
    private bool canGrab = false;
    private int grabCooldown = 5;
    private int grabCDCounter = 0;
    private int coyoteTime = 3;
    private int layerSwapCooldown = 10;
    private int layerSwapCDCounter = 0;
    private float layerSwapDistance = 2;
    private float layerSwapTime = 0;
    private float newLayerTransform;
    private float xIN = 0f;
    private float yIN = 0f;
    private Vector2 currentVelocity;
    private Vector2 normVelocity;
    public int currentLayer;
    private int minLayer = 6;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        spi = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        currentLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        if (newLayerTransform != 0)
        {return;}
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpReady == true)
            {
                Vector2 jumpDirection;
                if (grabFlag == true)
                {
                    Destroy(gameObject.GetComponent<HingeJoint2D>());
                    grabFlag = false;
                    canGrab = false;
                    grabCDCounter = grabCooldown;
                    jumpDirection = (surfaceNorm).normalized;
                }
                else
                {
                    jumpDirection = (normVelocity + 5 * Vector2.up).normalized;
                }

                rb2d.AddForce(jumpDirection * jumpStrength, ForceMode2D.Impulse);
                isGrounded = false;
                jumpReady = false;
            }
        }
        xIN = Input.GetAxis("Horizontal");
        if (Mathf.Abs(xIN) > 0.05f)
        {
            if (isGrounded)
            {
                rb2d.AddForce(surfaceNorm * upwardMoveForce, ForceMode2D.Force);

            }
            rb2d.AddForce(xIN * Vector2.left * Vector2.Perpendicular(surfaceNorm) * moveSpeed, ForceMode2D.Force);
        }
        yIN = Input.GetAxis("Vertical");
        if (Mathf.Abs(yIN) > 0.05f)
        {
        }
        if (Input.GetAxis("Fire3") > 0.05f && canGrab && floorContacts > 0)
        {
            if (grabFlag == false)
            {
                HingeJoint2D hj2d = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
                hj2d.connectedBody = primaryContact.rigidbody;
                hj2d.enableCollision = true;
            }

            grabFlag = true;
        }

        float zIN = Input.GetAxis("Fire2");
        if (Mathf.Abs(zIN) > 0.05f && newLayerTransform == 0 && layerSwapCDCounter == 0)
        {
            Vector2 newLocation = rb2d.position + rb2d.velocity * layerChangeFrameTime * Time.fixedDeltaTime;
            int newLayer = currentLayer + (int) Mathf.Sign(zIN);
            int layerMask = LayerMask.GetMask((LayerMask.LayerToName(newLayer)));
            Collider2D hit = Physics2D.OverlapCircle(newLocation, playerCollider.radius * 2, newLayer);
            if (newLayer >= minLayer && newLayer <= minLayer + 5 && hit == null)
            {
                gameObject.layer = 3;
                layerSwapTime = layerChangeFrameTime;
                layerSwapCDCounter = layerSwapCooldown;
                currentLayer = newLayer;
                grabFlag = false;
            }
        }

        if (Input.GetAxis("Fire3") < 0.05f || !grabFlag)
        {
            if (Input.GetAxis("Fire3") < 0.05f && grabFlag)
            {
                grabCDCounter = grabCooldown;
            }
            Destroy(gameObject.GetComponent<HingeJoint2D>());
        }


    }
    void FixedUpdate()
    {
        currentVelocity = rb2d.velocity;
        normVelocity = currentVelocity.normalized;
        float velocityAngle = Vector2.SignedAngle(Vector2.right, normVelocity);

        layerSwapCDCounter = (int) Mathf.Max(0, layerSwapCDCounter - 1);
        grabCDCounter = (int) Mathf.Max(0, grabCDCounter - 1);

        contactFilter.SetNormalAngle(velocityAngle - 175, velocityAngle + 175);

        floorContacts = rb2d.GetContacts(contactFilter, currentSurfaces);

        if (currentLayer != gameObject.layer)
        {
            float target = (float) (currentLayer - 6) * layerSwapDistance * -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.SmoothDamp(transform.position.z, target, ref newLayerTransform, layerSwapTime * Time.deltaTime));
            if (transform.position.z >= target - 0.05f && transform.position.z <= target + 0.05f)
            {

                gameObject.layer = currentLayer;
                newLayerTransform = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y, target);
            }
            else
            {return;}
        }
        if (floorContacts > 0)
        {
            if (grabCDCounter == 0) {canGrab = true;}
            primaryContact = currentSurfaces[0];
            surfaceNorm = primaryContact.normal;
            surfaceAngle = Vector2.SignedAngle(Vector2.right, surfaceNorm);
            foreach (ContactPoint2D surface in currentSurfaces)
            {
                float newAngle = Vector2.SignedAngle(Vector2.right, surface.normal);
                if (Mathf.Abs(newAngle - velocityAngle) < Mathf.Abs(surfaceAngle - velocityAngle))
                {
                    primaryContact = surface;
                    surfaceNorm = primaryContact.normal;
                    surfaceAngle = newAngle;
                }
            }
            coyoteTime = 5;
        }
        if (coyoteTime >= 0)
        {
            coyoteTime -= 1;
            isGrounded = true;
            jumpReady = true;
        }
        else
        {
            if (!grabFlag)
            {
                isGrounded = false;
                jumpReady = false;
                primaryContact = new ContactPoint2D();
                surfaceNorm = new Vector2(0, 1);
                surfaceAngle = Vector2.SignedAngle(Vector2.right, surfaceNorm);
            }
            canGrab = false;

        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }
}
