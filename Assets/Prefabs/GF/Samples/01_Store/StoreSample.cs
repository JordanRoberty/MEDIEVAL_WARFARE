using System;
using UnityEngine.GameFoundation.Components;

namespace UnityEngine.GameFoundation.Sample
{
    public class StoreSample : PrefabSample
    {
        /// <summary>
        ///     The prefab to show wait animation when Transaction is in progress
        /// </summary>
        public GameObject waitAnimationPrefab;
        
        /// <summary>
        ///     The Transform to keep the reference of wait animation
        /// </summary>
        private Transform m_WaitAnimation;

        /// <summary>
        ///     Standard starting point for Unity scripts.
        /// </summary>
        void Start()
        {
            // Check if the Sample Project database has been properly setup
            if (!VerifyDatabase())
            {
                wrongDatabasePanel.SetActive(true);
            }
        }
        
        private void OnEnable()
        {
            GameFoundationSdk.initialized += RegisterEvents;
            GameFoundationSdk.uninitialized += UnregisterEvents;
            
            if (GameFoundationSdk.IsInitialized)
            {
                RegisterEvents();   
            }
        }
        
        private void OnDisable()
        {
            GameFoundationSdk.initialized -= RegisterEvents;
            GameFoundationSdk.uninitialized -= UnregisterEvents;

            if (GameFoundationSdk.IsInitialized)
            {
                UnregisterEvents();   
            }
        }
        
        /// <summary>
        ///     Add necessary events for this sample to Game Foundation.
        /// </summary>
        void RegisterEvents()
        {
            if (GameFoundationSdk.transactions == null)
                return;

            GameFoundationSdk.transactions.transactionInitiated += OnTransactionInitiated;
            GameFoundationSdk.transactions.transactionSucceeded += OnTransactionSucceeded;
            GameFoundationSdk.transactions.transactionFailed += OnTransactionFailed;
        }

        /// <summary>
        ///     Removes the events for this sample from Game Foundation.
        /// </summary>
        void UnregisterEvents()
        {
            if (GameFoundationSdk.transactions == null)
                return;

            GameFoundationSdk.transactions.transactionInitiated -= OnTransactionInitiated;
            GameFoundationSdk.transactions.transactionSucceeded -= OnTransactionSucceeded;
            GameFoundationSdk.transactions.transactionFailed -= OnTransactionFailed;
        }
        
        private void OnTransactionInitiated(BaseTransaction transaction)
        {
            if (m_WaitAnimation == null && waitAnimationPrefab != null)
            {
                m_WaitAnimation = Instantiate(waitAnimationPrefab).transform;
            }
        }

        /// <summary>
        ///     Callback that gets triggered when any item in the store is successfully purchased. Triggers the
        ///     user-specified onPurchaseSuccess callback.
        /// </summary>
        private void OnTransactionSucceeded(BaseTransaction transaction, TransactionResult result)
        {
            RemoveLoadingAnimation();
        }

        /// <summary>
        ///     Callback that gets triggered when any item in the store is attempted and fails to be purchased. Triggers the
        ///     user-specified onPurchaseFailure callback.
        /// </summary>
        private void OnTransactionFailed(BaseTransaction transaction, Exception exception)
        {
            RemoveLoadingAnimation();
        }
        
        /// <summary>
        ///     Callback that gets triggered when any purchase button status is changed.
        /// </summary>
        /// <param name="purchaseButton">
        ///     A purchase button
        /// </param>
        /// <param name="oldStatus">
        ///     The old status of the purchase button
        /// </param>
        /// <param name="newStatus">
        ///     The new status of the purchase button
        /// </param>
        public void OnPurchasableStatusChanged(PurchaseButton purchaseButton, PurchasableStatus oldStatus,
            PurchasableStatus newStatus)
        {
            if (newStatus == PurchasableStatus.ItemUnaffordable)
            {
                purchaseButton.SetInteractable(false);
            }
            else if (oldStatus == PurchasableStatus.ItemUnaffordable)
            {
                purchaseButton.SetInteractable(true);
            }
        }

        /// <summary>
        ///     Removes wait animation from scene and the reference of its transform.
        /// </summary>
        private void RemoveLoadingAnimation()
        {
            if (m_WaitAnimation == null) return;
            Destroy(m_WaitAnimation.gameObject);
            m_WaitAnimation = null;
        }
    }
}
