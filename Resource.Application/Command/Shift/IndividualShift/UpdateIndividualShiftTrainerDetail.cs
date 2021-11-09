using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.Shift.IndividualShift.Request;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.Shift.IndividualShift
{
    public class UpdateIndividualShiftTrainerDetail
    {
        public class UpdateIndividualShiftTrainerDetailCommand : UpdateIndividualShiftTrainerViewModel, IRequest
        {

        }

        public class UpdateIndividualShiftTrainerDetailCommandValidator : AbstractValidator<UpdateIndividualShiftTrainerDetailCommand>
        {
            public UpdateIndividualShiftTrainerDetailCommandValidator()
            {
                RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty().NotNull().WithMessage("Id is required!");
            }
        }

        public class Handler : IRequestHandler<UpdateIndividualShiftTrainerDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateIndividualShiftTrainerDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var individualShift = await _context.IndividualShifts.Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
                    if (individualShift == null) throw new AppException("Individual Shift Record not found!");

                    individualShift.TrainerId = request.TrainerId;
                    individualShift.TrainingDate = string.IsNullOrEmpty(request.TrainingDate) ? null
                                                   : DateTime.ParseExact(request.TrainingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                    individualShift.TrainingDateNp = string.IsNullOrEmpty(request.TrainingDate) ? null : request.TrainingDateNp;

                    _context.IndividualShifts.Update(individualShift);
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return Unit.Value;
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
