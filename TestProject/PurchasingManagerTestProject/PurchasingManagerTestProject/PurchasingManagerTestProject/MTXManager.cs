using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace MTXManager
{
    public partial class MTXManager
    {
        private static MTXManager mSelf;
        public static MTXManager Self
        {
            get
            {
                if (mSelf == null)
                {
                    mSelf = new MTXManager();
                }
                return mSelf;
            }
        }

        public MTXManager()
        {
            InitializeManager();
        }

        private void UpdateConsumableCurrency(string consumableKey)
        {

        }
    }
}
