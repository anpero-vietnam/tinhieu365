namespace AEnum
{
    public class Cache
    {
        public enum Cachingkey
        {
            MainAnalytic =1,
            StoreListOfUser=2,
            RequestAnalytic = 3,
            AllStoreInfo=4,
            AllCategoryCache = 5,
            AllClientCache=6,            
            CommonInfo_FontEnd_ = 6

        }
        public static string GetCommonInfoKey()
        {
            return "CommonInfo_FontEnd_";
        }
        public static string CurrentSiteKey()
        {
            return "CurrentSiteKey_BackEnd_" ;
        }
    }
}
