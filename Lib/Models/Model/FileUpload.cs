//*******************************************************************************
//
//          Anpero Confidential 
//          © Copyright Anpero - 2020.
//-------------------------------------------------------------------------------
//  Initiator: Tran Duy Thang.
//*******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class FileUpload
    {        
        public string TokenKey { get; set; }
        public string UserToken { get; set; }
        public string AgencyID { get; set; }
        public string Size { get; set; }
        public string ExternalImagesLinkToUpload { get; set; }
        
        public FileUpload()
        {           
            TokenKey = string.Empty;
            Size = string.Empty;
            AgencyID = string.Empty;
        }
    }
    public class FileDelete
    {
        public string TokenKey { get; set; }
        public string UserToken { get; set; }
        public string AgencyID { get; set; }
        
        public string FilePath { get; set; }
        public FileDelete()
        {
            UserToken = string.Empty;
            FilePath = string.Empty;
            TokenKey = string.Empty;            
            AgencyID = string.Empty;
        }
    }
}
