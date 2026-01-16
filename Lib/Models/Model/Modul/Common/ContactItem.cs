//*******************************************************************************
//
//          Anpero Confidential 
//          © Copyright Anpero - 2020.
//-------------------------------------------------------------------------------
//  Initiator: Tran Duy Thang.
//*******************************************************************************
using System;

namespace Models.Modul.Common
{
    public class ContactItem
    {
        

        public string ClienceIp { get; set; }
        
        public string SesionId { get; set; }
        public string BankName { get; set; }
        public string BankNumBer { get; set; }
        public bool Reader { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }        
        public string Address { get; set; }       

        public string Contacts { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
