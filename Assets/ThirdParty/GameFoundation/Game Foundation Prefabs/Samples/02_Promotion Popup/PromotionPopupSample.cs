using System;
using UnityEngine.GameFoundation.Components;

namespace UnityEngine.GameFoundation.Sample
{
    public class PromotionPopupSample : PrefabSample
    {
        /// <summary>
        ///     The reference to Promotion Popup View to open it when Game Foundation is Initialized
        /// </summary>
        public PromotionPopupView promotionPopupView;
        
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
            // If Game Foundation is not initialized yet, Promotion Popup will be invisible until it's initialized.
            if (promotionPopupView != null && !GameFoundationSdk.IsInitialized)
            {
                promotionPopupView.gameObject.SetActive(false);
            }   
            
            // We always listen to "GameFoundationSdk.initialized" in case this event may be triggered after this view is rendered
            // and to make Promotion Popup visible when Game Foundation is initialized.
            GameFoundationSdk.initialized += OnGameFoundationInitialized;
            GameFoundationSdk.uninitialized += UnregisterEvents;
            
            if (GameFoundationSdk.IsInitialized)
            {
                RegisterEvents();   
            }
        }
        
        private void OnDisable()
        {
            GameFoundationSdk.initialized -= OnGameFoundationInitialized;
            GameFoundationSdk.uninitialized -= UnregisterEvents;
            
            if (GameFoundationSdk.IsInitialized)
            {
                UnregisterEvents();   
            }
        }
        
        /// <summary>
        ///    Register Events and Opens Promotion Popup when Game Foundation is initialized.
        /// </summary>
        void OnGameFoundationInitialized()
        {
            RegisterEvents();
            
            OpenPromotionPopup();
        }
        
        //
        
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
            
            GameFoundationSdk.initialized -= OnGameFoundationInitialized;
        }

        /// <summary>
        ///     Opens Promotion Popup if Game Foundation is initialized and Promotion Popup reference is defined.
        ///     This method is also triggered by the Button on the scene. 
        /// </summary>
        public void OpenPromotionPopup()
        {
            if (GameFoundationSdk.IsInitialized)
            {
                if (promotionPopupView != null)
                {
                    promotionPopupView.Open();
                }
                else
                {
                    Debug.LogWarning("Promotion Popup View reference is missing.");    
                }
            }
            else
            {
                Debug.LogWarning("Game Foundation should be initialized to open Promotion Popup.");
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
