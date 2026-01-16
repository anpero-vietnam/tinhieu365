using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultil.Static
{
    public class Oderstatus
    {
        public static int Unprocessor = 0;
        public static int Watched = 1;
        public static int PaidAndShipped = 2;
        public static int NganLuongPaiedAndUnprocessor = 3;
        public static int NganLuongPaiedAndBeginShip = 4;
        public static int PaidOnlineButNotDelivered = 5;
        public static int DoneAll = 6;

        public static int TransactionCanceled = -1;
        public static String GetOrderStatusHtmlOption()
        {
            String optionList = "<option value=''> Tất cả các trạng thái</option>";
            optionList += "<option value='0'> Chưa xử lý</option>";
            optionList += "<option value='-1'> Hủy giao dịch</option>";
            optionList += "<option value='1'> Đã xem</option>";
            optionList += "<option value='2'> Đã thanh toán đủ và giao hàng</option>";
            optionList += "<option value='3'> Đã thanh toán Online, Chưa xử lý</option>";
            optionList += "<option value='4'> Đã thanh toán Online, Đang giao hàng</option>";
            optionList += "<option value='5'> Đã thanh toán Online, Chưa giao hàng</option>";
            optionList += "<option value='7'> Đang chuyển hàng</option>";
            optionList += "<option value='6'> Hoàn tất giao dịch</option>";
            optionList += "<option value='-2'> Có giao dịch và đã xóa</option>";
            return optionList;
        }
        public static string GetOrderStatusText(int status)
        {

            switch (status)
            {
                case -2:
                    return "Đơn hàng có giao dịch và đã xóa";
                case -1:
                    return "Hủy giao dịch";
                case 0:
                    return "Chưa xử lý";
                case 1:
                    return "Đã xem";
                case 2:
                    return "Đã thanh toán đủ và giao hàng";
                case 3:
                    return "Đã thanh toán Online, Chưa xử lý";
                case 4:
                    return "Đã thanh toán Online, đang ship hàng";
                case 5:
                    return "Đã thanh toán Online nhưng chưa giao hàng";
                case 6:
                    return "Hoàn tất giao dịch";
                case 7:
                    return "Đang chuyển hàng";
                case 90:
                    return "Hủy đang chuyển hàng";
                case 89:
                    return "Hủy chuyển hàng COD";
                case 81:
                    return "Phiếu nợ đã xóa";

                case 79:
                    return "Đã xóa Đã thanh toán đủ và giao hàng";
                case 83:
                    return "Đã xóa 'Hoàn tất giao dịch'";
                case 78:
                    return "Đã xóa 'Đã xem' ";
                case 77:
                    return "Đã xóa 'Chưa xử lý' ";
                default:
                    return "";
            }
        }

    }
    public class PayMenMethod
    {
        public static int AtStore = 0;
        public static int COD = 1;
        public static int PaymentOnline = 2;
        public static int PaymentNganLuong = 3;
        public static int BankTranfer = 4;
        public static string GetPaymentMethodText(int i)
        {
            switch (i)
            {
                case 0:
                    return "Thanh toán tại cửa hàng";
                case 1:
                    return "Thu tiền khi nhận hàng";
                case 2:
                    return "Thanh toán online";
                case 3:
                    return "Thanh toán Ngân Lượng";
                case 4:
                    return "Chuyển khoản ngân hàng";
                default:
                    return "Khác";
            }
        }
    }
    public class ShippingMethod
    {
        public static int fastDelivery= 1;
        public static int normalDelivery = 2;
        public static int noShipGetProductAtStore = 3;
        public static string GetShippingMethodText(int i)
        {
            switch (i)
            {
                case 1:
                    return "Chuyển phát nhanh";
                case 2:
                    return "Chuyển phát thường";
                case 3:
                    return "Đến cửa hàng lấy hàng";
                default:
                    return "Giao tại cửa hàng";
            }
        }
    }
}
