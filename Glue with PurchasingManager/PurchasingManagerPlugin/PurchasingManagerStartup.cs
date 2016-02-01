using FlatRedBall.Glue.Plugins;
using FlatRedBall.Glue.Plugins.ExportedInterfaces;
using FlatRedBall.Glue.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingManagerPlugin
{
    [Export(typeof(PluginBase))]
    public partial class PurchasingManagerStartup :PluginBase
    {
        [Import("GlueCommands")]
        public IGlueCommands GlueCommands
        {
            get;
            set;
        }

        [Import("GlueState")]
        public IGlueState GlueState { get; set; }

        public override string FriendlyName
        {
            get { return "Purchasing Manager"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }


        public override void StartUp()
        {

        }

        public override bool ShutDown(PluginShutDownReason shutDownReason)
        {


            return true;
        }
    }
}
