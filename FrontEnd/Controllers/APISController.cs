using AModul.Bill;
using Dal;
using Dal.paymentApi.NganLuong;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [IsAuthenlication]
    public class APISController : BaseController
    {
        // GET: API

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payment(int projectId, string priceKey)
        {

            KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
            var allKeyValue = keyValueConfigControl.GetAllKeyValueConfig();
            var _priceKey = allKeyValue.Where(k => k.Key == priceKey).FirstOrDefault();
            string vipDayConfig = (priceKey == "Vip1Price" ? "Vip1Day" : "Vip2Day");
            var priceDayConfig = allKeyValue.Where(k => k.Key == vipDayConfig).FirstOrDefault();
            ViewBag.Price = Ultil.StringHelper.ConVertToMoneyFormat(_priceKey.Value);
            ViewBag.Day = priceDayConfig.Value;
            return View();
        }
        [HttpPost]
        public string GetUrlCheckOut(string paymentMethod, string bankcode, string projectId, string price)
        {
            string rs = string.Empty;
            try
            {
                //https://www.nganluong.vn/checkout/version31/index/token_code/49459653-e725fe66e0665ae941d205fb0ff9b340
                // payment_method "NL"|"VISA|ATM_ONLINE|ATM_OFFLINE|IB_ONLINE"

                KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
                var allKeyValue = keyValueConfigControl.GetAllKeyValueConfig();
                //Vip1Price|Vip2Price
                var keyValue = allKeyValue.Where(k => k.Key == price).FirstOrDefault();
                //string vipDaykey = (rank == Vip1Price ? "Vip1Day" : "Vip2Day");
                var nlConfig = GetNLPaymentConfig();

                if (nlConfig != null && !string.IsNullOrEmpty(nlConfig.MerchantId))
                {
                    string _description = "Thanh toan cho project " + projectId + ", paymentMethod: " + paymentMethod + ", rank: " + price;
                    var insertBillId = CreateBill(keyValue.Value, projectId, _description);
                    //string DomainName = Request.Url.Scheme + @"://" + Request.Url.Host;
                    string DomainName = "https://tinhieu365.com";
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = nlConfig.MerchantId.Trim();
                    info.Merchant_password = nlConfig.MerchantPassword.Trim();
                    //info.Merchant_id = "24338";
                    //info.Merchant_password = "12345612";
                    // info.Payment_type="1";
                    info.Receiver_email = nlConfig.Email.Trim();

                    info.cur_code = "vnd";
                    if (!string.IsNullOrEmpty(bankcode))
                    {
                        info.bank_code = bankcode;
                    }

                    info.Order_code = insertBillId.ToString();
                    info.Total_amount = keyValue.Value;
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = _description ?? string.Empty;
                    info.return_url = DomainName + "/APIS/NLCallback?price=" + price;
                    info.cancel_url = DomainName + "/APIS/NLCancel";
                    info.Buyer_fullname = AppSession.CurentProfile.SureName;
                    info.Buyer_email = AppSession.CurentProfile.Email ?? string.Empty;
                    info.Buyer_mobile = AppSession.CurentProfile.Phone ?? string.Empty;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, paymentMethod);

                    if (result.Error_code == "00")
                    {
                        rs = result.Checkout_url;
                    }
                    else
                        rs = result.Description + ", DomainName: " + DomainName;
                }

            }
            catch (Exception ex)
            {

            }

            return rs;
        }
        [HttpPost]
        public string WhereVietCheckOut(int projectId, string price)
        {
            string rs = string.Empty;
            Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            try
            {
                KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
                var allKeyValue = keyValueConfigControl.GetAllKeyValueConfig();
                var priceKey = allKeyValue.Where(k => k.Key == price).FirstOrDefault();
                string vipDayConfig = (priceKey.Key == "Vip1Price" ? "Vip1Day" : "Vip2Day");
                int rank = (priceKey.Key == "Vip1Price" ? 1 : 2);
                var priceDayConfig = allKeyValue.Where(k => k.Key == vipDayConfig).FirstOrDefault();
                if (currentProfile.Credit >= Convert.ToDouble(priceKey.Value))
                {
                    BillControl billControl = new BillControl();
                    var insertId = billControl.AddBill(new Models.Modul.Bill.BillModel
                    {
                        Amount = Convert.ToDecimal(priceKey.Value),
                        CreateBy = AppSession.CurentProfile.UserId,
                        CreateFor = AppSession.CurentProfile.UserId,
                        Detail = "Thanh toán bằng tinhieu365 credit",
                        IsSuccess = true,
                        OrderId = projectId.ToString(),
                        IsCredit = false,
                        Merchant = "WV"
                    });
                    if (insertId > 0)
                    {
                        AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();


                        if (productControl.UpdateProductRank(projectId, rank, Convert.ToInt32(priceDayConfig.Value)) > 0)
                        {
                            rs = $"Cập nhật thành công, bạn đã nâng cấp project lên tin Vip, thời gian cộng thêm là {priceDayConfig.Value} ngày";
                            RefreshToken();
                        }


                    }
                }
                else
                {
                    rs = "Số dư tài khoản không đủ, cập nhật tin Vip thất bại";
                }
            }
            catch (Exception ex)
            {

            }

            return rs;
        }
        private int CreateBill(string Amount, string projectId, string detail, bool IsSuccess = false)
        {
            BillControl billControl = new BillControl();
            return billControl.AddBill(new Models.Modul.Bill.BillModel
            {
                Amount = Convert.ToDecimal(Amount),
                CreateBy = AppSession.CurentProfile.UserId,
                CreateFor = AppSession.CurentProfile.UserId,
                Detail = detail,
                IsSuccess = false,
                OrderId = projectId,
                IsCredit = true,
                Merchant = "NL"
            });
        }
        private int UpdateBillToSuccess(int billId)
        {
            BillControl billControl = new BillControl();
            var bill = billControl.GetBillById(billId, AppSession.CurentProfile.UserId);
            bill.IsSuccess = true;
            bill.IsCredit = true;
            return billControl.AddBill(bill);
        }

        private PaymentConfig GetNLPaymentConfig()
        {
            var paymentConfig = new PaymentConfig();
            StoreMng.Config.PaymentConfigControl ws = new StoreMng.Config.PaymentConfigControl();
            var pa = ws.GetPaymentConfigList();
            if (pa != null && pa.Count() > 0)
            {
                paymentConfig = pa.Where(x => x.PaymentCode.Equals("NL", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            return paymentConfig;
        }
        public ActionResult NLCallback(string Token, string price, string error_code)
        {
            string rs = string.Empty;

            if (error_code == "00")
            {
                KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
                var allKeyValue = keyValueConfigControl.GetAllKeyValueConfig();

                StoreMng.Inteface.IPaymentConfig pcx = new StoreMng.Config.PaymentConfigControl();
                var pc = pcx.GetPaymentConfigList().Where(x => x.Name.ToUpper().Equals("NL")).FirstOrDefault();

                string vipDayConfig = (price == "Vip1Price" ? "Vip1Day" : "Vip2Day");
                int rank = (price == "Vip1Price" ? 1 : 2);
                var priceConfig = allKeyValue.Where(k => k.Key == price).FirstOrDefault();
                var priceDayConfig = allKeyValue.Where(k => k.Key == vipDayConfig).FirstOrDefault();

                if (pc != null)
                {

                    RequestCheckOrder info = new RequestCheckOrder();
                    info.Merchant_id = pc.MerchantId.Trim();
                    info.Merchant_password = pc.MerchantPassword.Trim();
                    info.Token = Token;
                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    BillControl billControl = new BillControl();

                    ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
                    var bill = billControl.GetBillById(Convert.ToInt32(result.order_code), AppSession.CurentProfile.UserId);

                    // nếu đơn hàng đã tạo và cộng tiền rồi thì thôi ko tiến hành giao dịch gì nữa
                    if (result.errorCode == "00" && !string.IsNullOrEmpty(result.order_code) && !bill.IsSuccess && bill.Amount == Convert.ToDecimal(result.paymentAmount))
                    {

                        //update and add cash book
                        //ws.UpdateOrderStatus(StoreID, TokenKey, Convert.ToInt32(result.order_code), Convert.ToInt32(result.paymentAmount), "Ngân Lượng (Mã giao dịch Ngân Lượng " + result.transactionId + ")<br />" + rs);
                        AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();
                        if (Convert.ToDecimal(priceConfig.Value) == bill.Amount && UpdateBillToSuccess(Convert.ToInt32(result.order_code)) > 0 && productControl.UpdateProductRank(Convert.ToInt32(bill.OrderId), rank, Convert.ToInt32(priceDayConfig.Value)) > 0)
                        {
                            string vipName = vipDayConfig.Replace("Day",string.Empty);
                            rs = $"Cập nhật thành công, bạn đã nâng cấp project lên tin {vipName}, thời gian cộng thêm là {priceDayConfig.Value} ngày";
                            billControl.AddBill(new Models.Modul.Bill.BillModel
                            {
                                Amount = bill.Amount,
                                CreateBy = AppSession.CurentProfile.UserId,
                                CreateFor = AppSession.CurentProfile.UserId,
                                Detail = "Trừ tiền tự động khi thanh toán bằng ngân lượng",
                                IsSuccess = true,
                                OrderId = bill.OrderId,
                                IsCredit = false,
                                Merchant = "NL"
                            });
                            RefreshToken();
                        }
                        rs += "<br>";
                        rs += result.description;
                        rs += "<br>";
                        rs += "Số tiền thanh toán: <strong>" + result.paymentAmount + "</strong>";
                        rs += "<br>";
                        rs += "Mã giao dịch Ngân Lượng: <strong>" + result.transactionId + "</strong>";
                        rs += "<br>";
                        rs += "Mã đơn hàng: " + result.order_code + "</strong>";
                        rs += "<br>";
                        rs += "Tên người thanh toán: " + result.payerName + "</strong>";

                        ViewBag.Msg = rs;
                    }
                    else
                    {
                        rs += result.description;
                        rs += "<br>";
                        rs += "Đơn hàng này đã được thanh toán.";
                        rs += "<br>";
                        rs += "Số tiền thanh toán: <strong>" + result.paymentAmount + "</strong>";
                        rs += "<br>";
                        rs += "Mã giao dịch Ngân Lượng: <strong>" + result.transactionId + "</strong>";
                        rs += "<br>";
                        rs += "Mã đơn hàng: " + result.order_code + "</strong>";
                        rs += "<br>";
                        rs += "Tên người thanh toán: " + result.payerName + "</strong>";

                    }

                }

            }
            else
            {
                rs += "Lỗi " + error_code + "";
            }
            ViewBag.Msg = rs;
            return View("Index");
        }

        public ActionResult NLCancel(string Token)
        {
            ViewBag.Msg = "Giao dịch đã được hủy, cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi";
            return View();
        }
        private void RefreshToken()
        {
            Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            int cacheMinute = AEnum.SiteConfig.SaveLoginDay * 60 * 24;
            var newToken = Ultil.JwtManagers.GenerateToken(currentProfile.UserName, currentProfile.UserId.ToString(), currentProfile.SureName, currentProfile.Avata, currentProfile.Address, currentProfile.Credit.ToString(), cacheMinute);
            Ultil.CookieHelper.SetCookie("_tk", newToken, cacheMinute);
        }

    }

}