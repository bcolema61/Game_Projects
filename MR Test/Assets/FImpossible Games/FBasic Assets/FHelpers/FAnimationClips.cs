using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Basics
{
    /// <summary>
    /// FM: Basic class for holding animation clips as hashes inside dictionary and with some other handy methods
    /// Add your animation clips with fAnimationClip.AddClip(animator, Idle) then use it by fAnimationClip["Idle"]
    /// </summary>
    public class FAnimationClips : Dictionary<string, int>
    {
        public readonly Animator Animator;

        public FAnimationClips(Animator animator)
        {
            this.Animator = animator;
        }

        /// <summary>
        /// Checking if inside animator clipName state exist and with 'exactClipName' false it will check all variants of clipName, uppercase / lowercase etc.
        /// </summary>
        public void AddClip(string clipName, bool exactClipName = false)
        {
            AddClip(Animator, clipName, exactClipName);
        }

        /// <summary>
        /// Checking if inside animator clipName state exist and with 'exactClipName' false it will check all variants of clipName, uppercase / lowercase etc.
        /// </summary>
        public void AddClip(Animator animator, string clipName, bool exactClipName = false)
        {
            if (!animator)
            {
                Debug.LogError("No animator!");
                return;
            }

            string existing = "";

            if (!exactClipName) // Checking if animation state exists with different variants for clipName word
            {
                if (FAnimatorMethods.StateExists(animator, clipName)) existing = clipName;
                else
                    if (FAnimatorMethods.StateExists(animator, FStringMethods.CapitalizeFirstLetter(clipName))) existing = FStringMethods.CapitalizeFirstLetter(clipName);
                else
                    if (FAnimatorMethods.StateExists(animator, clipName.ToLower())) existing = clipName.ToLower();
                else
                    if (FAnimatorMethods.StateExists(animator, clipName.ToUpper())) existing = clipName.ToUpper();
            }
            else // Checking if state with provided exact name exists inside animator
            {
                if (FAnimatorMethods.StateExists(animator, clipName)) existing = clipName;
            }

            if (existing == "")
            {
                Debug.LogWarning("Clip with name " + clipName + " not exists in animator from game object " + animator.gameObject.name);
            }
            else // Adding clip hash to dictionary if it exists inside animator
            {
                if (!ContainsKey(clipName))
                    Add(clipName, Animator.StringToHash(existing));
            }
        }

        /// <summary>
        /// Transitioning to choosed animation by dictionary
        /// </summary>
        public void CrossFadeInFixedTime(string clip, float transitionTime = 0.25f)
        {
            if (this.ContainsKey(clip))
            {
                if (!IsPlaying(clip)) Animator.CrossFadeInFixedTime(this[clip], transitionTime);
            }
        }

        /// <summary>
        /// Changing float parameter value smoothly (when speed value about 10, 60 is instant)
        /// </summary>
        public void SetFloat(string parameter, float value = 0f, float deltaSpeed = 60f)
        {
            float newValue = Animator.GetFloat(parameter);
            newValue = FLogicMethods.FLerp(newValue, value, Time.deltaTime * deltaSpeed);
            Animator.SetFloat(parameter, newValue);
        }

        internal bool IsPlaying(string clip)
        {
            AnimatorStateInfo info;
            if (Animator.IsInTransition(0))
            {
                info = Animator.GetNextAnimatorStateInfo(0);
                if (info.shortNameHash == this[clip]) return true;
            }
            else
            {
                info = Animator.GetCurrentAnimatorStateInfo(0);
                if (info.shortNameHash == this[clip]) return true;
            }

            return false;
        }
    }


    /// <summary>
    /// Machine behaviour for animator controll
    /// </summary>
    public sealed class FBasic_StateEventsBehaviour : StateMachineBehaviour
    {
        public event System.Action<int> OnEnterState;
        public event System.Action<int> OnExitState;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if ( OnEnterState != null ) OnEnterState.Invoke(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if ( OnExitState != null ) OnExitState.Invoke(stateInfo.shortNameHash);
        }
    }
}
