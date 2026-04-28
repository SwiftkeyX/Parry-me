using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Reusable
    /// 
    /// Role?
    /// Detection for the enemy using "Vision Cone".
    /// 
    /// How to use?
    /// Called "_enemyDetection.CanDetect" from other script.
    /// </summary>
    public class EnemyDetection : MonoBehaviour
    {
        // =============================== logic var ===============================
        private GameObject _target;
        [SerializeField] private float _detectionRange = 5f;
        [SerializeField] private float _detectionAngleRange = 90;
        private bool _canDetect;
        [SerializeField] private bool _gizmoOn = true;

        // =============================== setter and getter ===============================
        public bool CanDetect { get { return _canDetect; } }

        void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        void Start()
        {
            _canDetect = false;
        }

        void Update()
        {
            IsTargetDetect();

            // Debug.Log("[EnemyDetection] can detect: " + _canDetect);
        }

        void IsTargetDetect()
        {
            float distanceToTarget = Vector3.Distance(this.transform.position, _target.transform.position);
            // Debug.Log("[EnemyDetection] distanceToTarget: " + distanceToTarget);

            // distance check
            if (distanceToTarget <= _detectionRange)
            {
                Vector3 directionToTarget = (_target.transform.position - this.transform.position);
                float angleToTarget = Vector3.Angle(this.transform.forward, directionToTarget);
                // Debug.Log("[EnemyDetection] forward : " + this.transform.forward + " directionToTarget: " + directionToTarget);
                // Debug.Log("[EnemyDetection] angleToTarget: " + angleToTarget);

                // Is target in vision cone's angle
                if (angleToTarget <= _detectionAngleRange / 2f)
                {
                    _canDetect = true;
                    return;
                }
            }
            _canDetect = false;
        }

        /// <summary>
        /// Draw detection range gizmo
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (!_gizmoOn) return;
            
            // draw red sphere around enemy
            Gizmos.color = Color.red;  
            Gizmos.DrawWireSphere(transform.position, _detectionRange);

            Vector3 leftRayDirection = Quaternion.Euler(0, 0, -_detectionAngleRange / 2) * transform.forward;
            Vector3 rightRayDirection = Quaternion.Euler(0, 0, _detectionAngleRange / 2) * transform.forward;

            // draw yellow cone to the edge of the red sphere
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, leftRayDirection * _detectionRange);
            Gizmos.DrawRay(transform.position, rightRayDirection * _detectionRange);
        }
    }
}