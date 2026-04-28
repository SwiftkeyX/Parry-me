using DebugHelper;
using Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class EnemyDebugList : BaseDebugList<EnemyStateMachine>
    {
        private DebugMenu _debugDetection;

        [Header("Enemy Specific Debug")]
        [SerializeField] private List<DebugEntry> DebugDetectionInfo;


    }
}
