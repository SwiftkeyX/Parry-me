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
        [SerializeField] private float _deAggroRange = 8f; // Slightly larger than detection
        private bool _hasDetectedTarget = false;
        private bool _canDetect;

        // =============================== gizmo ===============================
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
        }

        private void IsTargetDetect()
        {
            float distanceToTarget = Vector3.Distance(this.transform.position, _target.transform.position);

            // PHASE 1: SEARCHING TARGET (Vision Cone)
            if (!_hasDetectedTarget)
            {
                if (distanceToTarget <= _detectionRange)
                {
                    Vector3 directionToTarget = (_target.transform.position - this.transform.position).normalized;
                    float angleToTarget = Vector3.Angle(this.transform.forward, directionToTarget);

                    if (angleToTarget <= _detectionAngleRange / 2f)
                    {
                        _hasDetectedTarget = true;
                    }
                }
            }

            // PHASE 2: PURSUIT TARGET (Ignore Vision Cone. Check De-aggro Distance)
            else
            {
                if (distanceToTarget > _deAggroRange)
                {
                    _hasDetectedTarget = false; // Lost the target (Player ran away)
                }
            }

            // Export the result to the property used by other scripts
            _canDetect = _hasDetectedTarget;
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