// FingerTouch.cs
// Attached to the rain garden models
// Enable to rotate the model with one finger move
// And scale the model with two fingers pinch

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTouch : MonoBehaviour
{

    [SerializeField]
    private Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField]
    private Vector3 maxScale = new Vector3(5.0f, 5.0f, 5.0f);

    [SerializeField]
    private float rotationSpeedDelay = 0.3f;

    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 lastScale;

    // Initial rotation when model loaded
    private Quaternion initRotation;

    private bool rotateEnd = true;

    // Delta value of finger movement
    private float deltaRotation = 0.0f;
    private float deltaScale = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate back if not using finger rotation
        if (rotateEnd)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, initRotation, 0.1f);
        }
        
        FingerRotation();
        FingerPinch();
    }


    /// <summary>
    /// Rotate using swap with one touch
    /// Apply rotation with finger delta move horizontal distance
    /// </summary>
    private void FingerRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotateEnd = false;
                // We only interested in horizontal direction
                // Then assgin y-axis roation
                // Use same direction as finger moves
                this.transform.Rotate(0f, touch.deltaPosition.x * rotationSpeedDelay, 0f);

                deltaRotation -= touch.deltaPosition.x;
                
            }

            // Rotate back to initial position after finger rotate
            /*if (touch.phase == TouchPhase.Ended)
            {
                rotateEnd = true;

                if (Mathf.Abs(deltaRotation) >= 300)
                {
                    deltaRotation = 0.0f;
                }
            }*/
        }
    }

    /// <summary>
    /// Scale using pinch involves two touches
    /// We need to count both the touches, store it and measure the distance between pinch
    /// and scale gameobject depending on the pinch distance
    /// We also need to ignore if the pinch distanceis small and really large
    /// </summary>
    private void FingerPinch()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // If any one of touches is cancelled or ended then do nothing
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                // Have some notable movement of finger
                /*if (deltaScale > 1.0f || deltaScale < 0.5f)
                {
                    if (GameManager.instance.instructionPhase == InstructionPhase.Scale)
                    {
                        GameManager.instance.instructionPhase = InstructionPhase.Done;
                    }
                }*/

                deltaScale = 0.0f;

                return;
            }

            // Calcuate the initial distance
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = this.transform.localScale;
                lastScale = initialScale;
            }
            else
            {
                // If touch moved
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // If accidentally touched or pinch movement is really small
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                // If Use finger touch is moving both vertically
                // Then it will change the transform position of y-axis
                // Otherwise it will just change the scale of the model
                if (Mathf.Abs(touchZero.deltaPosition.y) > 10.0f && Mathf.Abs(touchOne.deltaPosition.y) > 10.0f)
                {
                    var direction = 1.0f;
                    if (touchZero.deltaPosition.y < 0)
                    {
                        direction *= -1.0f;
                    }
                    const float deltaMoveStep = 0.2f;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + deltaMoveStep * direction, this.transform.position.z);
                }
                else 
                {
                    // Calculate scale factor and apply
                    var factor = currentDistance / initialDistance;
                    deltaScale = factor;
                    this.transform.localScale = scaleCompare(initialScale * factor) ? initialScale * factor : lastScale;
                    lastScale = this.transform.localScale;
                }
            }
        }
    }

    /// <summary>
    /// Truncated the scale within the bounds
    /// </summary>
    /// <param name="newScale"> new scale </param>
    /// <returns> true if newScale is winth the bound </returns>
    private bool scaleCompare(Vector3 newScale)
    {
        if (newScale.x > maxScale.x || newScale.y > maxScale.y || newScale.z > maxScale.z)
        {
            return false;
        }

        if (newScale.x < minScale.x || newScale.y < minScale.y || newScale.z < minScale.z)
        {
            return false;
        }

        return true;
    }
}
