using System.Collections;
using General.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace InExercise
{
    public class NotificationManager : MonoBehaviour
    {
        private Rule violatedRule;
        private string notificationText;
        
        public int showNotificationDurationInSeconds = 3;
        private bool notificationShown;

        public void SetViolatedRule(Rule rule)
        {
            if (!ShouldTriggerNotification(rule)) return;
            violatedRule = rule;
            notificationText = violatedRule.notificationText;
            NotifyUser();
        }

        public void SendNotification(string text)
        {
            notificationText = text;
            NotifyUser();
        }

        private void NotifyUser()
        {
            var animator = gameObject.GetComponent<Animator>();

            if (animator == null) return; // Exists
            if (animator.GetBool("show") || notificationShown) return; // Is not already shown
            gameObject.GetComponentInChildren<Text>().text = notificationText;
            animator.SetBool("show", true);
            StartCoroutine(HideNotification(animator));
            notificationShown = true;
        }

        private bool ShouldTriggerNotification(Rule rule)
        {
            var animator = gameObject.GetComponent<Animator>();
            if (animator == null) return false;
            if (animator.GetBool("show") || notificationShown) return false;
            if (rule.Equals(violatedRule)) return false;
            return true;
        }

        private IEnumerator HideNotification(Animator animator)
        {
            if (!animator.GetBool("show")) yield break;
            yield return new WaitForSeconds(showNotificationDurationInSeconds);
            animator.SetBool("show", false);
            yield return new WaitForSeconds(1); // Can be removed when more stable
            notificationShown = false;
        }
    }
}
