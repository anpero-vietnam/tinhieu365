namespace VNPAYMENT_NET_CS.VnPayment
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [GeneratedCode("System.Web.Services", "4.0.30319.34209"), DebuggerStepThrough, DesignerCategory("code")]
    public class HelloCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal HelloCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public string Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (string) this.results[0];
            }
        }
    }
}

