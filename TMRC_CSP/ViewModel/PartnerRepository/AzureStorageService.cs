using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TMRC_CSP.ViewModel.PartnerRepository
{
    public class AzureStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        
        /// <summary>
        /// The BLOB container which contains the public portal's assets.
        /// </summary>
        private CloudBlobContainer publicBlobContainer;

        /// <summary>
        /// The name of the portal asserts blob container which contains publicly available blobs.
        /// This is useful for storing images which the browser can access.
        /// </summary>
        private const string PublicPortalAssetsBlobContainerName = "publiccustomerportalassets";

        /// <summary>
        /// The portal branding key in the cache.
        /// </summary>
        private const string PortalBrandingCacheKey = "PortalBranding";

      

        /// <summary>
        /// The name of the portal assets blob container.
        /// </summary>
        private const string PrivatePortalAssetsBlobContainerName = "customerportalassets";


        /// <summary>
        /// The BLOB container which contains the portal's configuration assets.
        /// </summary>
        private CloudBlobContainer privateBlobContainer;
        

        public async Task<CloudBlobContainer> GetPublicCustomerPortalAssetsBlobContainerAsync()
        {
            if (publicBlobContainer == null)
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                publicBlobContainer = blobClient.GetContainerReference(PublicPortalAssetsBlobContainerName);
            }

            if (!await publicBlobContainer.ExistsAsync().ConfigureAwait(false))
            {
                await publicBlobContainer.CreateAsync().ConfigureAwait(false);

                BlobContainerPermissions permissions = await publicBlobContainer.GetPermissionsAsync().ConfigureAwait(false);
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;

                await publicBlobContainer.SetPermissionsAsync(permissions).ConfigureAwait(false);
            }

            return publicBlobContainer;
        }


        /// <summary>
        /// Generates a new BLOB reference to store a new asset.
        /// </summary>
        /// <param name="blobContainer">The Blob container in which to create the BLOB.</param>
        /// <param name="blobPrefix">The BLOB name prefix to use.</param>
        /// <returns>The new BLOB reference.</returns>
        public async Task<CloudBlockBlob> GenerateNewBlobReferenceAsync(CloudBlobContainer blobContainer, string blobPrefix)
        {
            blobContainer.AssertNotNull(nameof(blobContainer));

            blobPrefix = blobPrefix ?? "asset";
            const string BlobNameFormat = "{0}{1}";
            CloudBlockBlob newBlob = null;

            do
            {
                newBlob = blobContainer.GetBlockBlobReference(string.Format(
                    CultureInfo.InvariantCulture,
                    BlobNameFormat,
                    blobPrefix,
                    new Random().Next().ToString(CultureInfo.InvariantCulture)));
            }
            while (await newBlob.ExistsAsync().ConfigureAwait(false));

            return newBlob;
        }


      

        /// <summary>
        /// Returns a cloud BLOB container reference which can be used to manage the customer portal assets.
        /// </summary>
        /// <returns>The customer portal assets BLOB container.</returns>
        public async Task<CloudBlobContainer> GetPrivateCustomerPortalAssetsBlobContainerAsync()
        {
            if (privateBlobContainer == null)
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                privateBlobContainer = blobClient.GetContainerReference(PrivatePortalAssetsBlobContainerName);
            }

            await privateBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            return privateBlobContainer;
        }
    }
}