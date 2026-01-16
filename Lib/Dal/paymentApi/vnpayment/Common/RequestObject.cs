namespace VNPAYMENT_NET_CS.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class RequestObject
    {
        public bool IsValidSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return Utils.Md5(this.Action + "|" + this.TerminalId + "|" + this.OrderId + "|" + this.LocalDate + "|" + this.RequestDesc + "|" + secretKey).Equals(this.Signature);
        }

        public string MakeSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return string.Empty;
            }
            return Utils.Md5(this.Action + "|" + this.TerminalId + "|" + this.OrderId + "|" + this.LocalDate + "|" + this.RequestDesc + "|" + secretKey);
        }

        public string Action { get; set; }

        public string LocalDate { get; set; }

        public string Locale { get; set; }

        public string OrderId { get; set; }

        public string RequestDesc { get; set; }

        public string Signature { get; set; }

        public string TerminalId { get; set; }

        public string Version { get; set; }

        public string VnPayTranId { get; set; }
    }
}

