using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m^s-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 11f;
    [Tooltip("In m")] [SerializeField] float yRange = 7f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;

    [Header("Controll-throw Based")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    public void OnPlayerDeath() // Called by string reference from Collision Handler
    {
        isControlEnabled = false;
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
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPost = transform.localPosition.x + xOffset;
        float clampedXPost = Mathf.Clamp(rawXPost, -xRange, xRange);
        transform.localPosition = new Vector3(clampedXPost, transform.localPosition.y, transform.localPosition.z);


        // Ship move up and down
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPost = transform.localPosition.y + yOffset;
        float clampedYPost = Mathf.Clamp(rawYPost, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPost, clampedYPost, transform.localPosition.z);
    }
}
