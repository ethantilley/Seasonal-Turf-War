using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script responible for camera shaking.
/// 
/// </summary>
public class CameraController : MonoBehaviour
{
    // singletone pattern
    #region Singleton Instance
    public static CameraController instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);

        }
        else
        {
            instance = this;
        }
    }
    #endregion
    // Storing properties
    #region Properties
    [System.Serializable]
    public class Properties
    {
        [Header("Angle for the cameras initial diraction"), Tooltip("Angle for the cameras initial diraction to shake towards")]
        public float angle;
        [Header("Distance the shake moves the camera"), Tooltip("Controls how far the shake can move the camera")]
        public float strength = 0.5f;
        [Header("Speed of the Camera During Shake"), Tooltip("Higher this value is, the faster the camera moves to a shake position")]
        public float speed = 5;
        [Header("Camera Shake Time"), Tooltip("How long the camera has to shake")]
        public float duration = 1;
        [Range(0, 1), Header("Shake Randomness"), Tooltip("Higher this is the more random the shaking feels")]
        public float noisePercent;
        [Range(0, 1), Header("Smoothout Over Time Amount"), Tooltip("Percent on how quickly the strength of the shake decreases over time.")]
        public float dampingPercent;
        [Range(0, 1), Header("Rotation Shake Amount"), Tooltip("Controls how much the movement of the shake effects the cameras rotation")]
        public float rotationPercent;
    }
    #endregion

    const float maxAngle = 10f;

    public Properties properties;

    IEnumerator currentShakeCorutine;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))

        {
            StartShake(properties);
        }
    }
    /// <summary>
    /// Checking if theirs another Coroutine running 
    /// stops the one already ruunning if so
    /// 
    /// </summary>
    /// <param name="properties"></param>
    public void StartShake(Properties properties)
    {
        if (currentShakeCorutine != null)
            StopCoroutine(currentShakeCorutine);

        currentShakeCorutine = Shake(properties);

        StartCoroutine(currentShakeCorutine);
    }

    /// <summary>
    /// this Coroutine is where the camera 
    /// </summary>
    /// <param name="properties">
    /// this is past through containing the vaules used to determin how the camera will preform 
    /// </param>
    /// <returns></returns>
    IEnumerator Shake(Properties properties)
    {
        float completionPercent = 0;
        float movePercent = 0;
        float angle_radians = properties.angle * Mathf.Deg2Rad - Mathf.PI;
        Vector3 previousWayPoint = Vector3.zero;
        Vector3 currentWayPoint = Vector3.zero;
        float moveDistance = 0;

        Quaternion targetRotation = Quaternion.identity;
        Quaternion previousRotation = Quaternion.identity;

        do
        {
            if (movePercent >= 1 || completionPercent == 0)
            {
                //setting waypoints using the 'properties'values and doing some math to them.
                //
                float dampingFactor = DampingCurve(completionPercent, properties.dampingPercent);
                float noiseAngle = (Random.value - .5f) * Mathf.PI;
                angle_radians += Mathf.PI + noiseAngle * properties.noisePercent;
                currentWayPoint =  new Vector3(Mathf.Cos(angle_radians), Mathf.Sin(angle_radians)) * properties.strength * dampingFactor;
                previousWayPoint = transform.localPosition;

                moveDistance = Vector3.Distance(currentWayPoint, previousWayPoint);
                
                targetRotation = Quaternion.Euler(new Vector3(currentWayPoint.y, currentWayPoint.x).normalized * properties.rotationPercent * dampingFactor * maxAngle);
                previousRotation = transform.localRotation;

                // setting move percent back to '0' so it can go back from the previous waypoin to the new current waypoint;
                movePercent = 0;
            }
            //
            completionPercent += Time.deltaTime / properties.duration;
            // Creates a constant speed , so the further the camera has to move - the slower the move percent should increase
            // and faster it travels - the faster movepercent should increase.
            movePercent += Time.deltaTime / moveDistance * properties.speed;
            // Actualy Moving the camera using a lerp from point to point using move percent
            transform.localPosition = Vector3.Lerp(previousWayPoint, currentWayPoint, movePercent);
            transform.localRotation =  Quaternion.Slerp(previousRotation, targetRotation, movePercent);
            // wait for a frame between each while loop 
            yield return null;

            // made to do while so it runs this atleast once and it will keep running this if move distance is grater than 0
        } while (moveDistance > 0);

    }
    /// <summary>
    /// A damping curve that changes depening on the 'completionPercent'
    /// srinks the radious in which the camera can move over time.
    /// </summary>
    /// <param name="x">
    /// AKA - CompletionPercent
    /// </param>
    /// <param name="dampingPercent"></param>
    /// <returns></returns>
    float DampingCurve(float x, float dampingPercent)
    {
        x = Mathf.Clamp01(x);
        float a = Mathf.Lerp(2, .25f, dampingPercent);
        float b = 1 - Mathf.Pow(x, a);
        return b * b * b;

    }

}
