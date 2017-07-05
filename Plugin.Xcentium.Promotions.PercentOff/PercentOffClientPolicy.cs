using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Plugin.Xcentium.Promotions.PercentOff
{
    public class PercentOffClientPolicy: Policy 
    {
        public PercentOffClientPolicy()
        {
            this.PercentOffFieldNameInSitecore = "Percentage Off";
            this.CombineWithOtherPromos = false;
        }

        /// <summary>
        /// The name of the percentage off field in Sitecore where value to take off will be extracted from
        /// </summary>
        public string PercentOffFieldNameInSitecore { get; set; }

        /// <summary>
        /// Weather to combine this with other promos or discount applied to this product or not.
        /// </summary>
        public bool CombineWithOtherPromos { get; set; }

    }
}
