﻿using AuthServer.Models.Users.Employee.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Users.Customer
{
    public class RegisterCustomerPackageViewModel
    {
        public CreateEmployeeRequest CustomerDetail { get; set; }
        public string PackageId { get; set; }
        public string StartDate { get; set; }
        public string StartDateNP { get; set; }
        public string EndDate { get; set; }
        public string EndDateNP { get; set; }
        public int PaymentGateway { get; set; }
        public string ShiftId { get; set; }
        public string PromoCode { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsAdmin { get; set; }
    }
}