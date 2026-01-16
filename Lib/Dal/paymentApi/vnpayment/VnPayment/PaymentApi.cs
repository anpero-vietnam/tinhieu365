namespace VNPAYMENT_NET_CS.VnPayment
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;
    using VNPAYMENT_NET_CS.Properties;

    [WebServiceBinding(Name="PaymentApiSoap", Namespace="https://www.vnpayment.vn/"), DesignerCategory("code"), GeneratedCode("System.Web.Services", "4.0.30319.34209"), DebuggerStepThrough]
    public class PaymentApi : SoapHttpClientProtocol
    {
        private SendOrPostCallback ExecuteOperationCompleted;
        private SendOrPostCallback HelloOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        public event ExecuteCompletedEventHandler ExecuteCompleted;

        public event HelloCompletedEventHandler HelloCompleted;

        public PaymentApi()
        {
            this.Url = Settings.Default.VNPAYMENT_NET_CS_VnPayment_PaymentApi;
            if (this.IsLocalFileSystemWebService(this.Url))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public void CancelAsync2(object userState)
        {
            base.CancelAsync(userState);
        }

        [SoapDocumentMethod("https://www.vnpayment.vn/Execute", RequestNamespace="https://www.vnpayment.vn/", ResponseNamespace="https://www.vnpayment.vn/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string Execute(string data)
        {
            return (string) base.Invoke("Execute", new object[] { data })[0];
        }

        public void ExecuteAsync(string data)
        {
            this.ExecuteAsync(data, null);
        }

        public void ExecuteAsync(string data, object userState)
        {
            if (this.ExecuteOperationCompleted == null)
            {
                this.ExecuteOperationCompleted = new SendOrPostCallback(this.OnExecuteOperationCompleted);
            }
            base.InvokeAsync("Execute", new object[] { data }, this.ExecuteOperationCompleted, userState);
        }

        [SoapDocumentMethod("https://www.vnpayment.vn/Hello", RequestNamespace="https://www.vnpayment.vn/", ResponseNamespace="https://www.vnpayment.vn/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string Hello()
        {
            return (string) base.Invoke("Hello", new object[0])[0];
        }

        public void HelloAsync()
        {
            this.HelloAsync(null);
        }

        public void HelloAsync(object userState)
        {
            if (this.HelloOperationCompleted == null)
            {
                this.HelloOperationCompleted = new SendOrPostCallback(this.OnHelloOperationCompleted);
            }
            base.InvokeAsync("Hello", new object[0], this.HelloOperationCompleted, userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        private void OnExecuteOperationCompleted(object arg)
        {
            if (this.ExecuteCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.ExecuteCompleted(this, new ExecuteCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnHelloOperationCompleted(object arg)
        {
            if (this.HelloCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.HelloCompleted(this, new HelloCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        public string Url2
        {
            get
            {
                return base.Url;
            }
            set
            {
                if (!((!this.IsLocalFileSystemWebService(base.Url) || this.useDefaultCredentialsSetExplicitly) || this.IsLocalFileSystemWebService(value)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials2
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

