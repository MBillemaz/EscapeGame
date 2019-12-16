using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


/// <summary>
/// Crée des projectiles
/// </summary>
/// 
public class BowScript : MonoBehaviour {

    [Header("Assets")]
    public GameObject arrowPrefab = null;

    [Header("Bow")]
    public float GrabThreshold = 0.15f;
    public Transform m_Start = null;
    public Transform m_End = null;
    public Transform m_Socket = null;

    private Transform m_PullingHand = null;

    private Arrow m_currentArrow = null;

    private Animator m_Animator = null;

    private float m_PullValue = 0.0f;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnAttachedToHand(Hand attachedHand)
    {
        hand = attachedHand;
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        // Waiting for seconds
        yield return new WaitForSeconds(waitTime);

        // Create arrow
        GameObject arrowObject = Instantiate(arrowPrefab, m_Socket);
    }



    //private Hand hand;

    //private void OnAttachedToHand(Hand attachedHand)
    //{
    //    hand = attachedHand;
    //}


    //private void HandAttachedUpdate(Hand hand)
    //{
    //    // Reset transform since we cheated it right after getting poses on previous frame
    //    transform.localPosition = Vector3.zero;
    //    transform.localRotation = Quaternion.identity;

    //    // Update handedness guess
    //    EvaluateHandedness();

    //    if (nocked)
    //    {
    //        Vector3 nockToarrowHand = (arrowHand.arrowNockTransform.parent.position - nockRestTransform.position); // Vector from bow nock transform to arrowhand nock transform - used to align bow when drawing

    //        // Align bow
    //        // Time lerp value used for ramping into drawn bow orientation
    //        float lerp = Util.RemapNumberClamped(Time.time, nockLerpStartTime, (nockLerpStartTime + lerpDuration), 0f, 1f);

    //        float pullLerp = Util.RemapNumberClamped(nockToarrowHand.magnitude, minPull, maxPull, 0f, 1f); // Normalized current state of bow draw 0 - 1

    //        Vector3 arrowNockTransformToHeadset = ((Player.instance.hmdTransform.position + (Vector3.down * 0.05f)) - arrowHand.arrowNockTransform.parent.position).normalized;
    //        Vector3 arrowHandPosition = (arrowHand.arrowNockTransform.parent.position + ((arrowNockTransformToHeadset * drawOffset) * pullLerp)); // Use this line to lerp arrowHand nock position
    //                                                                                                                                              //Vector3 arrowHandPosition = arrowHand.arrowNockTransform.position; // Use this line if we don't want to lerp arrowHand nock position

    //        Vector3 pivotToString = (arrowHandPosition - pivotTransform.position).normalized;
    //        Vector3 pivotToLowerHandle = (handleTransform.position - pivotTransform.position).normalized;
    //        bowLeftVector = -Vector3.Cross(pivotToLowerHandle, pivotToString);
    //        pivotTransform.rotation = Quaternion.Lerp(nockLerpStartRotation, Quaternion.LookRotation(pivotToString, bowLeftVector), lerp);

    //        // Move nock position
    //        if (Vector3.Dot(nockToarrowHand, -nockTransform.forward) > 0)
    //        {
    //            float distanceToarrowHand = nockToarrowHand.magnitude * lerp;

    //            nockTransform.localPosition = new Vector3(0f, 0f, Mathf.Clamp(-distanceToarrowHand, -maxPull, 0f));

    //            nockDistanceTravelled = -nockTransform.localPosition.z;

    //            arrowVelocity = Util.RemapNumber(nockDistanceTravelled, minPull, maxPull, arrowMinVelocity, arrowMaxVelocity);

    //            drawTension = Util.RemapNumberClamped(nockDistanceTravelled, 0, maxPull, 0f, 1f);

    //            this.bowDrawLinearMapping.value = drawTension; // Send drawTension value to LinearMapping script, which drives the bow draw animation

    //            if (nockDistanceTravelled > minPull)
    //            {
    //                pulled = true;
    //            }
    //            else
    //            {
    //                pulled = false;
    //            }

    //            if ((nockDistanceTravelled > (lastTickDistance + hapticDistanceThreshold)) || nockDistanceTravelled < (lastTickDistance - hapticDistanceThreshold))
    //            {
    //                ushort hapticStrength = (ushort)Util.RemapNumber(nockDistanceTravelled, 0, maxPull, bowPullPulseStrengthLow, bowPullPulseStrengthHigh);
    //                hand.TriggerHapticPulse(hapticStrength);
    //                hand.otherHand.TriggerHapticPulse(hapticStrength);

    //                drawSound.PlayBowTensionClicks(drawTension);

    //                lastTickDistance = nockDistanceTravelled;
    //            }

    //            if (nockDistanceTravelled >= maxPull)
    //            {
    //                if (Time.time > nextStrainTick)
    //                {
    //                    hand.TriggerHapticPulse(400);
    //                    hand.otherHand.TriggerHapticPulse(400);

    //                    drawSound.PlayBowTensionClicks(drawTension);

    //                    nextStrainTick = Time.time + Random.Range(minStrainTickTime, maxStrainTickTime);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            nockTransform.localPosition = new Vector3(0f, 0f, 0f);

    //            this.bowDrawLinearMapping.value = 0f;
    //        }
    //    }
    //    else
    //    {
    //        if (lerpBackToZeroRotation)
    //        {
    //            float lerp = Util.RemapNumber(Time.time, lerpStartTime, lerpStartTime + lerpDuration, 0, 1);

    //            pivotTransform.localRotation = Quaternion.Lerp(lerpStartRotation, Quaternion.identity, lerp);

    //            if (lerp >= 1)
    //            {
    //                lerpBackToZeroRotation = false;
    //            }
    //        }
    //    }
    }

}

