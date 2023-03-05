using UnityEngine.GameFoundation.DefaultCatalog;

namespace UnityEngine.GameFoundation.Sample
{
    /// <summary>
    ///     This class is not an actual sample, but provides functionality for all other samples as needed.
    ///     It is included in this sample because it needs to be brought in for any other samples to work for the database.
    /// </summary>
    public class PrefabSample : MonoBehaviour
    {
        /// <summary>
        ///     The reference to the panel to display when the wrong database is in use.
        /// </summary>
        public GameObject wrongDatabasePanel;

        /// <summary>
        ///     This method verifies that the database in use is correct.
        ///     It also handles any situations where the settings were not setup correctly and may result in errors.
        /// </summary>
        /// <returns>
        ///     If the database in use is the sample database.
        /// </returns>
        protected bool VerifyDatabase()
        {
            try
            {
                if (CatalogSettings.catalogAsset is null)
                {
                    return false;
                }

                var catalogAssetName = CatalogSettings.catalogAsset.name;
                if (catalogAssetName == "PrefabSampleCatalog")
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            if (GameFoundationSdk.IsInitialized)
            {
                var isSampleCatalog = GameFoundationSdk.catalog.Find<GameParameter>("Is_Sample_Catalog");

                return !(isSampleCatalog is null);
            }

            return false;
        }
    }
}
