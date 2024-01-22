using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace PGGE
{

    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        LayerMask mask = LayerMask.GetMask("Obstruction");

        public void RepositionCamera()
        {
            RaycastHit hit;
            Vector3 p1 = mCameraTransform.position;
            Vector3 p2 = new Vector3(mPlayerTransform.position.x - mCameraTransform.position.x, 0, mPlayerTransform.position.z - mCameraTransform.position.z).normalized;
            float distance = (Vector3.Distance(p1, p2)) * 0.2f; //Sets the distance between the camera and the player so that the ray doesn't over extend in front of the character
                                                                //and repositioning the camera which results in the jittery shaking when the character turns next to the wall
            
            if (Physics.Raycast(p1, p2, out hit, distance, mask))
            {
                mCameraTransform.position = hit.point;
            }
            //draw ray from player to camera
            //if ray hits opaque object
            //get point at which cam hit the object
            //move cam there.
        }

       
        public abstract void Update();
    }
}
