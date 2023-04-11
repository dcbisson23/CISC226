using UnityEngine;

namespace GearFactory
{
    /// <summary>
    /// Copy rotation of other gear. The amount (and sign)
    /// of source rotation can be tailored by 
    /// rotationMultiplier.
    /// 
    /// It can be both connected to one of the gears or
    /// used as a separate object.
    /// </summary>
    public class CopyKinematicInverseRotation : MonoBehaviour
    {
        public GearBase masterGear;
        public GearBase slaveGear;
        public float rotationMultiplier = 1.0f;
        public bool IsUpdating = true;

        private Rigidbody2D masterRB2D;
        private Rigidbody2D slaveRB2D;
        private float previousGearARotation;

        void Start()
        {
            masterRB2D = masterGear.GetComponent<Rigidbody2D>();
            slaveRB2D = slaveGear.GetComponent<Rigidbody2D>();
            previousGearARotation = GetMasterZRotation();
        }

        void FixedUpdate()
        {
            if (IsUpdating && CheckGears())
            {
                previousGearARotation = GetMasterZRotation();
                slaveRB2D.angularVelocity = previousGearARotation * rotationMultiplier * -1;
            }
        }

        //shortcut
        private float GetMasterZRotation()
        {
            return masterRB2D.angularVelocity;
        }

        //check gears for null
        public bool CheckGears()
        {
            return masterGear != null && slaveGear != null;
        }

        void OnDrawGizmos()
        {
            if (CheckGears())
            {
                GearHelper.DrawArrowGizmos(masterGear.transform, slaveGear.transform, Color.blue);
            }
        }
    }
}
