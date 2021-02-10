using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In m^s-1")][SerializeField] float xSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 11f;
    [Tooltip("In m")] [SerializeField] float yRange = 7f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        // transform.localRotation = Quaternion.Euler(0f, 90f, 0); //Set rotation to 90°
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void ProcessTranslation()
    {
        // Ship moving left and right
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPost = transform.localPosition.x + xOffset;
        float clampedXPost = Mathf.Clamp(rawXPost, -xRange, xRange);
        transform.localPosition = new Vector3(clampedXPost, transform.localPosition.y, transform.localPosition.z);


        // Ship move up and down
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * xSpeed * Time.deltaTime;
        float rawYPost = transform.localPosition.y + yOffset;
        float clampedYPost = Mathf.Clamp(rawYPost, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPost, clampedYPost, transform.localPosition.z);
    }
}
