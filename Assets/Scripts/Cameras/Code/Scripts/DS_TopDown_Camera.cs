using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DueShooter.Camera
{
    public class DS_TopDown_Camera : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform m_Target;
        [SerializeField]
        private float m_Height = 10f;
        [SerializeField]
        private float m_Distance = 20f;
        [SerializeField]
        private float m_Angle = 45f;

        [SerializeField]
        private float m_SmoothSpeed = 2f;
        private Vector3 refVelocity;
        #endregion

        #region Methods
        // Start is called before the first frame update
        void Start()
        {
            HandleCamera();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
        }
        #endregion

        #region HelperMethods
        protected virtual void HandleCamera()
        {
            if (!m_Target)
            {
                return;
            }

            // build world position vector
            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            //Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            // build our rotated vector
            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

            // move our position
            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            //Debug.DrawLine(m_Target.position, finalPosition, Color.blue);


            // smooth out the movement transition of the camera
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);

            // focus on the target
            transform.LookAt(flatTargetPosition);


        }

        // draw a gizmo for the camera
        private void OnDrawGizmos()
        {
            if (m_Target)
            {
                Gizmos.DrawLine(transform.position, m_Target.position);
                //Gizmos.DrawSphere(m_Target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
        #endregion
    }
}
