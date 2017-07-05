using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommerceServer.Core.Catalog;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Pricing;
using Sitecore.Framework.Pipelines;

namespace Plugin.Xcentium.Promotions.PercentOff.Pipelines.Blocks
{
    /// <summary>
    /// 
    /// </summary>
    public class TakePercentOff : PipelineBlock<SellableItem, SellableItem, CommercePipelineExecutionContext>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<SellableItem> Run(SellableItem arg, CommercePipelineExecutionContext context)
        {
            var percentOffClientPolicy = context.GetPolicy<PercentOffClientPolicy>();

            if (arg == null)
                return Task.FromResult<SellableItem>((SellableItem)null);

            var product = context.CommerceContext.Objects.OfType<Product>().FirstOrDefault<Product>((Func<Product, bool>)(p => p.ProductId.Equals(arg.FriendlyId, StringComparison.OrdinalIgnoreCase)));

            if (product == null)
                return Task.FromResult<SellableItem>(arg);

            if (arg.HasComponent<PriceSnapshotComponent>())
                arg.Components.Remove((Component)arg.GetComponent<PriceSnapshotComponent>());

            if (!product.HasProperty(percentOffClientPolicy.PercentOffFieldNameInSitecore) ||
                product[percentOffClientPolicy.PercentOffFieldNameInSitecore] == null)
            {
                return Task.FromResult<SellableItem>(arg);
            }

            var percentOff = GetFirstDecimalFromString(product[percentOffClientPolicy.PercentOffFieldNameInSitecore].ToString());


            if (percentOff <= 0 || percentOff >= 100)
            {
                return Task.FromResult<SellableItem>(arg);
            }

            var optionMoneyPolicy = new PurchaseOptionMoneyPolicy();

            var listPrice = product.ListPrice;

            optionMoneyPolicy.SellPrice = arg.GetPolicy<PurchaseOptionMoneyPolicy>().SellPrice;

            var sellPrice = optionMoneyPolicy.SellPrice.Amount.ToString(CultureInfo.InvariantCulture);

            if (!percentOffClientPolicy.CombineWithOtherPromos)
            {
                if (listPrice != GetFirstDecimalFromString(sellPrice))
                {
                    return Task.FromResult<SellableItem>(arg);
                }
            }


            var newsalePrice = GetFirstDecimalFromString(sellPrice) * (100 - percentOff) / 100;

            arg.Policies.Remove((Policy)arg.GetPolicy<PurchaseOptionMoneyPolicy>());

            var newOptionMoneyPolicy = new PurchaseOptionMoneyPolicy();
            var currentCurrency = context.CommerceContext.CurrentCurrency();

            newOptionMoneyPolicy.SellPrice = new Money(currentCurrency, newsalePrice);
            arg.SetPolicy((Policy)newOptionMoneyPolicy);         

            return Task.FromResult<SellableItem>(arg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private decimal GetFirstDecimalFromString(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0.00M;
            var decList = Regex.Split(str, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "").ToList();
            var decimalVal = decList.Any() ? decList.FirstOrDefault() : string.Empty;

            if (string.IsNullOrEmpty(decimalVal)) return 0.00M;
            decimal decimalResult = 0;
            decimal.TryParse(decimalVal, out decimalResult);
            return decimalResult;
        }

    }
}
