namespace VNPAYMENT_NET_CS.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConfirmRs
    {
        public bool IsValidSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return Utils.Md5(this.RspCode + "|" + this.Message + "|" + this.OrderId + "|" + secretKey).Equals(this.Signature);
        }

        public string MakeSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return string.Empty;
            }
            return Utils.Md5(this.RspCode + "|" + this.Message + "|" + this.OrderId + "|" + secretKey);
        }

        public string Localdate { get; set; }

        public string Message { get; set; }

        public string OrderId { get; set; }

        public string RspCode { get; set; }

        public string Signature { get; set; }

        public string TerminalId { get; set; }
    }
}

