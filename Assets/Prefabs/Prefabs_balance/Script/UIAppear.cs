using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour {
    [SerializeField] private Image customImage;
    [SerializeField] private Text customText;

   /* void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || Input.GetMouseButton(0))
        {

            {
                customImage.enabled = true;
                customText.enabled = true;
            }
        }
    }
    */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            if (Input.GetMouseButton(0))
            {
                customImage.enabled = true;
                customText.enabled = true;
            }
            if (Input.GetMouseButton(1))
            {
                customImage.enabled = false;
                customText.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            {
                customImage.enabled = false;
                customText.enabled = false;
            }
        }
    }
}
