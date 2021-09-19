﻿using System;

namespace AuthServer.Models.Users
{
    public class UserKYCRequestModel
    {
        public string FullName { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string AlternativeContactNumber { get; set; }
        public string CitizenshipNumber { get; set; }
        public string PanNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string SpouseName { get; set; }
        public string Child1Name { get; set; }
        public string Child2Name { get; set; }
        public string BloodGroup { get; set; }
        public bool AnyMedication { get; set; }
        public bool AnyMedicalCondition { get; set; }
    }

    public class UserKYCResponseModel : UserKYCRequestModel
    {
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}