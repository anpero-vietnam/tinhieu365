namespace VNPAYMENT_NET_CS.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConfirmRq
    {
        public bool IsValidSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return Utils.Md5(this.RspCode + "|" + this.TerminalId + "|" + this.OrderId + "|" + this.Amount + "|" + this.CurrCode + "|" + this.VnpTranid + "|" + this.PaymentMethod + "|" + this.PayDate + "|" + secretKey).Equals(this.Signature);
        }

        public string MakeSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return string.Empty;
            }
            return Utils.Md5(this.RspCode + "|" + this.TerminalId + "|" + this.OrderId + "|" + this.Amount + "|" + this.CurrCode + "|" + this.VnpTranid + "|" + this.PaymentMethod + "|" + this.PayDate + "|" + secretKey);
        }

        public string AdditionalInfo { get; set; }

        public string Amount { get; set; }

        public string CurrCode { get; set; }

        public string OrderId { get; set; }

        public string PayDate { get; set; }

        public string PaymentMethod { get; set; }

        public string RspCode { get; set; }

        public string Signature { get; set; }

        public string TerminalId { get; set; }

        public string VnpTranid { get; set; }
    }
}

