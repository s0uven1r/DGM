using Dgm.Common.Models;
using Resource.Application.Command.Customer;
using Resource.Application.Common.Interfaces;
using Resource.Domain.Entities.Account;
using Resource.Domain.Entities.PackageCourse;
using Resource.Domain.Entities.Shift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NepaliDate;

namespace Resource.Infrastructure.Service
{
    public class CustomerPackageService : ICustomerPackageService
    {
        public readonly IAppDbContext _appDbContext;
        public CustomerPackageService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task RegisterCustomerPackage(CustomerPackageViewModel model, string accNo)
        {
            var individualShiftList = new Lazy<List<IndividualShift>>();

            var vehicleId = (from vehicle in _appDbContext.VehicleDetails
                             join individualShift in _appDbContext.IndividualShifts
                             on vehicle.Id equals individualShift.VehicleId into tempIndividual
                             from individual in tempIndividual.DefaultIfEmpty()
                             select new { vehicle.Id, individual.VehicleId, individual.ShiftId })
                             .FirstOrDefault(x => x.Id != x.VehicleId && x.ShiftId != model.ShiftId)?.Id;

            var packageDetails = _appDbContext.Packages.FirstOrDefault(x => x.Id == model.PackageId);
            var promoDetails = _appDbContext.PackagePromoOffers.FirstOrDefault(x => x.PackageId == model.PackageId && x.PromoCode == model.PromoCode);

            var data = new CustomerPackage
            {
                CustomerAccountNumber = accNo,
                PackageStartDate = DateTime.Parse(model.StartDate),
                PackageEndDate = DateTime.Parse(model.EndDate),
                PackageId = model.PackageId,
                PackageEndDateNp = model.EndDateNP,
                PackageStartDateNp = model.StartDateNP,
                PromoCode = model.PromoCode
            };
            _appDbContext.CustomerPackages.Add(data);
            // calculation
            decimal dueAmount = 0M;
            decimal netAmount = 0M;
            decimal discountAmount = 0M;
            if (promoDetails != null && !promoDetails.HasDiscountPercent)
                discountAmount = promoDetails.Discount;
            else if (promoDetails != null && promoDetails.HasDiscountPercent)
                discountAmount = packageDetails.Price - (packageDetails.Price * promoDetails.Discount / 100);
            netAmount = packageDetails.Price - discountAmount;
            dueAmount = netAmount - model.PaidAmount;

            _appDbContext.CustomerPayments.Add(new CustomerPayment
            {
                AccountNumber = accNo,
                CustomerPackageId = data.Id,
                PaidAmount = model.PaidAmount,
                DiscountAmount = promoDetails != null && !promoDetails.HasDiscountPercent ? promoDetails.Discount : 0,
                IsDiscountAvail = promoDetails != null && !promoDetails.HasDiscountPercent,
                IsPercentDiscount = promoDetails != null && promoDetails.HasDiscountPercent,
                DueAmount = dueAmount,
                DiscountPercent = promoDetails != null && promoDetails.HasDiscountPercent ? promoDetails.Discount : 0,
                NetAmount = netAmount,
                PaymentGateway = model.PaymentGateway
            });

            for (DateTime trainDate = data.PackageStartDate; trainDate <= data.PackageEndDate; trainDate = trainDate.AddDays(1))
            {
                var individualShift = new IndividualShift
                {
                    PackageId = data.PackageId,
                    ShiftId = model.ShiftId,
                    UserAccountNumber = accNo,
                    VehicleId = vehicleId,
                    TrainingDate = trainDate,
                    TrainingDateNp = NepaliDate trainDate
                };
                individualShiftList.Value.Add(individualShift);
            }
            _appDbContext.IndividualShifts.AddRange(individualShiftList.Value);
            CancellationToken cancellationToken = new();
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
