using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager Instance;


    public SteamVR_TrackedObject trackedObject;

    private GameObject currentArrow;

    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;

    public GameObject arrowPrefab;

    private bool isAttached = false;
    // Use this for initialization
    void Start () {
        if (Instance == null)
            Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        this.AttachArrow();
	}

    public void AttachArrow()
    {
        if (currentArrow == null){
            StartCoroutine(CreateArrow(2.0f));
        }
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        // Waiting for seconds
        yield return new WaitForSeconds(waitTime);

        // Create arrow
        currentArrow = Instantiate(arrowPrefab);
        currentArrow.transform.parent = trackedObject.transform;
        currentArrow.transform.position = new Vector3(0f, 0f, .342f);
        currentArrow.transform.localRotation = Quaternion.identity;
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStartPoint.transform.position;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Fire()
    {
        float dist = (stringStartPoint.transform.position - trackedObject.transform.position).magnitude;

        currentArrow.transform.parent = null;
        //currentArrow.GetComponent<Arrow>().Fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 25f * dist;
        r.useGravity = true;

        currentArrow.GetComponent<Collider>().isTrigger = false;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }

    private void PullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - trackedObject.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f);

           // var device = SteamVR_Controller.Input((int)trackedObject.index);
            //if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    Fire();
            //}
        }
    }
}
