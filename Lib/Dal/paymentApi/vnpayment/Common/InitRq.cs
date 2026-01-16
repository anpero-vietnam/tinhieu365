namespace VNPAYMENT_NET_CS.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class InitRq
    {
        public bool IsValidSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return Utils.Md5(this.TerminalId + "|" + this.OrderId + "|" + this.Amount + "|" + this.CurrCode + "|" + this.PaymentMethod + "|" + this.LocalDate + "|" + this.OrderDesc + "|" + this.ClientIp + "|" + secretKey).Equals(this.Signature);
        }

        public string MakeSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return string.Empty;
            }
            return Utils.Md5(this.TerminalId + "|" + this.OrderId + "|" + this.Amount + "|" + this.CurrCode + "|" + this.PaymentMethod + "|" + this.LocalDate + "|" + this.OrderDesc + "|" + this.ClientIp + "|" + secretKey);
        }

        public string Action { get; set; }

        public string Amount { get; set; }

        public string ClientIp { get; set; }

        public string CurrCode { get; set; }

        public string LocalDate { get; set; }

        public string Locale { get; set; }

        public string LogData
        {
            get
            {
                return ("TerminalCode=" + this.TerminalId + ",OrderId=" + this.OrderId + ",Amount=" + this.Amount + ",CurrCode=" + this.CurrCode + ",PaymentMethod=" + this.PaymentMethod + ",LocalDate=" + this.LocalDate + ",OrderDesc=" + this.OrderDesc + ",ClientIp=" + this.ClientIp);
            }
        }

        public string OrderDesc { get; set; }

        public string OrderId { get; set; }

        public string PaymentMethod { get; set; }

        public string Signature { get; set; }

        public string TerminalId { get; set; }

        public string Version { get; set; }
    }
}

