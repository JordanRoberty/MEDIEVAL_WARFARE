using System;
using UnityEngine.GameFoundation.Components;

namespace UnityEngine.GameFoundation.Sample
{
    public class RewardPopupSample : PrefabSample
    {
        /// <summary>
        ///     The reference to Reward Popup View to open it when Game Foundation is Initialized
        /// </summary>
        public RewardPopupView rewardPopupView;

        /// <summary>
        /// Standard starting point for Unity scripts.
        /// </summary>
        void Start()
        {
            // Check if the Sample Project database has been properly setup
            if (!VerifyDatabase())
            {
                wrongDatabasePanel.SetActive(true);
            }
        }
        
        /// <summary>
        ///     Disable Reward Popup if Game Foundation is not initialized.
        ///     Adds a listener to open Reward Popup when Game Foundation is initialized.
        /// </summary>
        private void OnEnable()
        {
            // If Game Foundation is not initialized yet, Reward Popup will be invisible until it's initialized.
            if (rewardPopupView != null && !GameFoundationSdk.IsInitialized)
            {
                rewardPopupView.gameObject.SetActive(false);
            }
            
            // We always listen to "GameFoundationSdk.initialized" in case this event may be triggered after this view is rendered
            // and to make Reward Popup visible when Game Foundation is initialized.
            GameFoundationSdk.initialized += OnGameFoundationInitialized;
        }

        /// <summary>
        ///     Removes the listener from Game Foundation.
        /// </summary>
        private void OnDisable()
        {
            GameFoundationSdk.initialized -= OnGameFoundationInitialized;
        }

        /// <summary>
        ///     Opens Reward Popup when Game Foundation is initialized.
        /// </summary>
        void OnGameFoundationInitialized()
        {
            OpenRewardPopup();
        }

        /// <summary>
        ///     Opens Reward Popup if Game Foundation is initialized and Reward Popup reference is defined.
        ///     This method is also triggered by the Button on the scene. 
        /// </summary>
        public void OpenRewardPopup()
        {
            if (GameFoundationSdk.IsInitialized)
            {
                if (rewardPopupView != null)
                {
                    rewardPopupView.Open();
                }
                else
                {
                    Debug.LogWarning("Reward Popup View reference is missing.");    
                }
            }
            else
            {
                Debug.LogWarning("Game Foundation should be initialized to open Reward Popup.");
            }
        }

        /// <summary>
        ///     Callback that gets triggered when a reward claim has been initiated.
        /// </summary>
        /// <param name="rewardKey">
        ///     The key of the Reward that has a claim being initiated on it.
        /// </param>
        /// <param name="rewardItemKey">
        ///     The key of the RewardItem attempting to be claimed.
        /// </param>
        public void OnRewardClaimInitiated(string rewardKey, string rewardItemKey)
        {
           Debug.Log($"Reward Claim Initiated - Reward Key: {rewardKey} - Reward Item Key {rewardItemKey}");
        }

        /// <summary>
        ///     Callback that gets triggered when a reward item is successfully claimed.
        /// </summary>
        /// <param name="reward">
        ///     The key of the Reward that had a successful claim.
        /// </param>
        /// <param name="rewardItemKey">
        ///     The key of the RewardItem that was successfully claimed.
        /// </param>
        /// <param name="payout">
        ///     The payout received from the RewardItem.
        /// </param>
        public void OnRewardClaimSucceeded(Reward reward, string rewardItemKey, Payout payout)
        {
            Debug.Log($"Reward Claim Succeeded - Reward: {reward.rewardDefinition.displayName} - Reward Item Key: {rewardItemKey}");
        }

        /// <summary>
        ///     Callback that gets triggered when a reward item is attempted to be claimed, but fails.
        /// </summary>
        /// <param name="rewardKey">
        ///     The key of the Reward that had a claim fail.
        /// </param>
        /// <param name="rewardItemKey">
        ///     The key of the RewardItem that failed to be claimed.
        /// </param>
        /// <param name="exception">
        ///     The reason the claim was unsuccessful.
        /// </param>
        public void OnRewardClaimFailed(string rewardKey, string rewardItemKey, Exception exception)
        {
            Debug.Log($"Reward Claim Failed - RewardKey: {rewardKey} - Reward Item Key: {rewardItemKey} - Exception: {exception.Message}");
        }
    }
}
