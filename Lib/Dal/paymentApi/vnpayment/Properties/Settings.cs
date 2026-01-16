namespace VNPAYMENT_NET_CS.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [DefaultSettingValue("https://www.vnpayment.vn/merchantapi/PaymentApi.asmx"), ApplicationScopedSetting, DebuggerNonUserCode, SpecialSetting(SpecialSetting.WebServiceUrl)]
        public string VNPAYMENT_NET_CS_VnPayment_PaymentApi
        {
            get
            {
                return (string) this["VNPAYMENT_NET_CS_VnPayment_PaymentApi"];
            }
        }
    }
}

