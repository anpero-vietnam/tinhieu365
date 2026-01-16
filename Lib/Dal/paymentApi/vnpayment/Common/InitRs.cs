namespace VNPAYMENT_NET_CS.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class InitRs
    {
        public InitRs(string code, string msg)
        {
            this.RspCode = code;
            this.Message = msg;
        }

        public bool IsValidSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return Utils.Md5(this.RspCode + "|" + this.Message + "|" + this.UrlRedirect + "|" + secretKey).Equals(this.Signature);
        }

        public string MakeSignature(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return string.Empty;
            }
            return Utils.Md5(this.RspCode + "|" + this.Message + "|" + this.UrlRedirect + "|" + secretKey);
        }

        public string AdditionalInfo { get; set; }

        public string Amount { get; set; }

        public string CreatedDate { get; set; }

        public string CurrCode { get; set; }

        public string LocalDate { get; set; }

        public string Message { get; set; }

        public string OrderId { get; set; }

        public string PaymentMethod { get; set; }

        public string RspCode { get; set; }

        public string Signature { get; set; }

        public string Status { get; set; }

        public string TerminalId { get; set; }

        public string UrlRedirect { get; set; }

        public string VnpTranid { get; set; }
    }
}

