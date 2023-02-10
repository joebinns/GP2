using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public static class UnityUtilities
    {
        public static void GetAllChildren(Transform parent, ref List<Transform> transforms, LayerMask layerMask)
        {
            foreach (Transform child in parent)
            {
                if (layerMask == (layerMask | (1 << child.gameObject.layer)))
                {
                    transforms.Add(child);
 
                    GetAllChildren(child, ref transforms, layerMask);
                }
            }
        } 

        public static IEnumerator ResetRigidbody(Rigidbody rb, Vector3 targetPos, Quaternion targetRot)
        {
            // Reset velocity and angular velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
 
            yield return new WaitForFixedUpdate();
 
            // Reset position and rotation
            rb.transform.position = targetPos;
            rb.transform.rotation = targetRot;
        }
    }
}