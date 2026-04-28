// using UnityEngine;

// /// <summary>
// /// This SO is for keep animation for a entity (the player and enemy).
// /// 
// /// Why?
// /// Each entity in this game use FSM to control their behavior.
// /// Also, this FSM is hard wired to the animator's statemachine.
// /// That mean for the most part, each state only have 1 animation (ex. idle state = 1animation, walk state = 1animation, etc...).
// /// But for my attack implementation, instead of having 1 animator's state per 1 attack animation 
// /// (which lead to several attack state in animator). 
// /// I will have only 1 animator's state for all the attack animation (by override the animator at runtime, we can achieve this).
// /// 
// /// </summary>
// [CreateAssetMenu(menuName = "MyAnimation/AnimationSO")]
// public class AnimationSO : ScriptableObject
// {
//     public AnimatorOverrideController animOV;       // over write previous attack animation
// }