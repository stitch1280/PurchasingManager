using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_8
using Windows.ApplicationModel.Store;

namespace MTXManager
{

    public partial class MTXManager
    {
        LicenseInformation mLicenseInfo;
        
        public async void InitializeManager()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Purchasing\\license.xml");

                await CurrentAppSimulator.ReloadSimulatorAsync(file);

                mLicenseInfo = CurrentAppSimulator.LicenseInformation;
            }
            else
#endif
            {
                mLicenseInfo = CurrentApp.LicenseInformation;
            }

        }


        public async void PurchaseNonConsumable(string productKey)
        {

            try
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    await CurrentAppSimulator.RequestProductPurchaseAsync(productKey);
                }
                else
#endif
                {
                    await CurrentApp.RequestProductPurchaseAsync(productKey);
                }
            }
            catch (Exception e)
            {
#if DEBUG
                FlatRedBall.Debugging.Debugger.Write("Purchased Failed: " + e.ToString());
#endif
            }
        }

        public async void PurchaseConsumable(string productKey)
        {

            PurchaseResults result = null;
            try
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    result = await CurrentAppSimulator.RequestProductPurchaseAsync(productKey);
                }
                else
#endif
                {
                    result = await CurrentApp.RequestProductPurchaseAsync(productKey);
                }
            }
            catch (Exception e)
            {
#if DEBUG
                FlatRedBall.Debugging.Debugger.Write("Purchased Failed: " + e.ToString());
#endif
            }

            ReportConsumableFulfilled(result);

        }

        private async void ReportConsumableFulfilled(PurchaseResults purchaseResult)
        {
            if(purchaseResult != null && purchaseResult.Status == ProductPurchaseStatus.Succeeded)
            {
                //We will assume the result is purchase pending. 
                //Then put the fulfillment in a loop until it is one of the success values.
                FulfillmentResult result = FulfillmentResult.PurchasePending;
                try
                {
                    while (result == FulfillmentResult.PurchasePending)
                    {
#if DEBUG
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            result = await CurrentAppSimulator.ReportConsumableFulfillmentAsync(purchaseResult.OfferId, purchaseResult.TransactionId);
                        }
                        else
#endif
                        {
                            result = await CurrentApp.ReportConsumableFulfillmentAsync(purchaseResult.OfferId, purchaseResult.TransactionId);
                        }

                        //ToDo: Save the purchase result info to disk if we have a server error.
                        if(result == FulfillmentResult.ServerError)
                        {
                        }

                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    FlatRedBall.Debugging.Debugger.Write("Unable to Fulfill consumable: " + e.ToString());
#endif
                }
                
            }
        }
    }
}
#endif