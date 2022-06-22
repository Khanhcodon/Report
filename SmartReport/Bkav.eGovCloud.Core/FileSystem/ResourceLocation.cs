using System;
using Bkav.eGovCloud.Core.Attributes;

namespace Bkav.eGovCloud.Core.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceLocation
    {
        #region Singleton

        private static readonly Lazy<ResourceLocation> DefaultConfigInitializer = new Lazy<ResourceLocation>(true);

        /// <summary>
        /// The default binding config.
        /// </summary>
        /// <value>The default.</value>
        public static ResourceLocation Default
        {
            get { return DefaultConfigInitializer.Value; }
        }

        #endregion

        #region Configs

        /// <summary>
        /// Vị trí lưu file đính kèm được upload tạm lên server
        /// </summary>
        [ConfigField("egovcloud.fileupload.temp", IsRequired = true, DefaultValue = @"TempFile")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string FileUploadTemp { get; set; }

        /// <summary>
        /// Vị trí lưu file tạm
        /// </summary>
        [ConfigField("egovcloud.file.temp", IsRequired = true, DefaultValue = @"Temp")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string FileTemp { get; set; }

        /// <summary>
        /// Vị trí lưu file cho văn bản quy phạm
        /// </summary>
        [ConfigField("egovcloud.file.filestore", IsRequired = true, DefaultValue = @"FileStore")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string FileStore { get; set; }

        /// <summary>
        /// Vị trí lưu file crystalReport
        /// </summary>
        [ConfigField("egovcloud.fileupload.crystalreport", IsRequired = true, DefaultValue = @"CrystalReport")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string CrystalReport { get; set; }
        
        /// <summary>
        /// Vị trí lưu file crystalReport
        /// </summary>
        [ConfigField("egovcloud.fileupload.embryonicform", IsRequired = true, DefaultValue = @"EmbryonicForm")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string EmbryonicForm { get; set; }

        /// <summary>
        /// Vi tri luu file xml
        /// </summary>
        [ConfigField("egovcloud.fileupload.xml", IsRequired = true, DefaultValue = @"FileXML")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string FileXML { get; set; }
        
        /// <summary>
        /// Vị trí lưu file chat
        /// </summary>
        [ConfigField("egovcloud.fileupload.chat", IsRequired = true, DefaultValue = @"Chat")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string Chat { get; set; }

        /// <summary>
        /// Vị trí lưu file chat
        /// </summary>
        [ConfigField("egovcloud.fileupload.chat", IsRequired = true, DefaultValue = @"ChartImg")]
        [ResolveAbsolutePath]
        [CreateDirectory]
        public string ChartImg { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLocation"/> class.
        /// </summary>
        public ResourceLocation() : this(false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLocation"/> class.
        /// </summary>
        /// <param name="useOverride">if set to <c>true</c> uses user-settings, do not read from app.config.</param>
        public ResourceLocation(bool useOverride)
        {
            if (!useOverride)
            {
                this.Initialize();
            }
        }

        #endregion
    }
}
