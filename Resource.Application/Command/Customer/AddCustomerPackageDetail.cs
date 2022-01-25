using Dgm.Common.Models;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Domain.Entities.Account;
using Resource.Domain.Entities.PackageCourse;
using Resource.Domain.Entities.Shift;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NepaliCalendarBS;

namespace Resource.Application.Command.Customer
{
    public class AddCustomerPackageDetail
    {
        public class AddCustomerPackageDetailCommand : CustomerPackageViewModel, IRequest<string>
        {
        }

        public class AddCustomerPackageDetailCommandValidator : AbstractValidator<AddCustomerPackageDetailCommand>
        {
            public AddCustomerPackageDetailCommandValidator()
            {
                RuleFor(x => x.PackageId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.ShiftId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.StartDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
                RuleFor(x => x.EndDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddCustomerPackageDetailCommand, string>
        {
            private readonly IAppDbContext _appDbContext;
            private readonly IAccountHeadCountService _accountHeadCountService;
            
            public Handler(IAppDbContext context, IAccountHeadCountService accountHeadCountService)
            {
                _appDbContext = context;
                _accountHeadCountService = accountHeadCountService;
            }


            public async Task<string> Handle(AddCustomerPackageDetailCommand model, CancellationToken cancellationToken)
            {
                var transaction = await _appDbContext.Instance.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    string accountNo = string.Empty;
                    if (string.IsNullOrEmpty(model.AccountNo))
                    {
                        accountNo = await _accountHeadCountService.GenerateAccountNumber(model.RoleType, model.RoleAlias);

                    }
                    else
                    {
                        accountNo = model.AccountNo;
                    }
                    
                    var individualShiftList = new Lazy<List<IndividualShift>>();

                    var vehicleId = (from vehicle in _appDbContext.VehicleDetails
                                     join individualShift in _appDbContext.IndividualShifts
                                     on vehicle.Id equals individualShift.VehicleId into tempIndividual
                                     from individual in tempIndividual.DefaultIfEmpty()
                                     select new { vehicle.Id, individual.VehicleId, individual.ShiftId })
                                     .FirstOrDefault(x => x.Id != x.VehicleId && x.ShiftId != model.ShiftId)?.Id;

                    var packageDetails = _appDbContext.Packages.FirstOrDefault(x => x.Id == model.PackageId);
                    var promoDetails = _appDbContext.PackagePromoOffers.FirstOrDefault(x => x.PromoCode == model.PromoCode);
                    if (promoDetails == null && !string.IsNullOrEmpty(model.PromoCode))
                        throw new Exception($"{model.PromoCode} promocode does not exist.");
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    var format = "dd/MM/yyyy";
                    var data = new CustomerPackage
                    {
                        CustomerAccountNumber = accountNo,
                        PackageStartDate = DateTime.ParseExact(model.StartDate, format, provider),
                        PackageEndDate = DateTime.ParseExact(model.EndDate, format, provider),
                        PackageEndDateNp = model.EndDateNP,
                        PackageStartDateNp = model.StartDateNP,
                        PromoCode = model.PromoCode,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address
                    };
                    _appDbContext.CustomerPackages.Add(data);
                    // calculation
                    decimal dueAmount = 0M;
                    decimal netAmount = 0M;
                    decimal discountAmount = 0M;
                    if (promoDetails != null && !promoDetails.HasDiscountPercent)
                        discountAmount = promoDetails.Discount;
                    else if (promoDetails != null && promoDetails.HasDiscountPercent)
                        discountAmount = (packageDetails.Price * promoDetails.Discount / 100);
                    netAmount = packageDetails.Price - discountAmount;
                    dueAmount = netAmount - model.PaidAmount;

                    _appDbContext.CustomerPayments.Add(new CustomerPayment
                    {
                        AccountNumber = accountNo,
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
                        var nepaliTrainingDate = NepaliCalendar.Convert_AD2BS(trainDate);
                        var individualShift = new IndividualShift
                        {
                            PackageId = data.PackageId,
                            ShiftId = model.ShiftId,
                            UserAccountNumber = accountNo,
                            VehicleId = vehicleId,
                            TrainingDate = trainDate,
                            TrainingDateNp = $"{nepaliTrainingDate.Day}/{nepaliTrainingDate.Month}/{nepaliTrainingDate.Year}"
                        };
                        individualShiftList.Value.Add(individualShift);
                    }
                    _appDbContext.IndividualShifts.AddRange(individualShiftList.Value);
                    await _appDbContext.SaveChangesAsync(cancellationToken);


                    await transaction.CommitAsync(cancellationToken);
                    return accountNo;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
