using Dgm.Common.Enums;
using Dgm.Common.Error;
using FluentValidation;
using MediatR;
using Resource.Application.Common.Interfaces;
using Resource.Application.Models.CoursePackage.CourseType.Request;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.CourseType
{
    public class AddCourseTypeDetail
    {
        public class AddCourseTypeDetailCommand : CourseTypeCreateViewModel, IRequest
        {

        }

        public class AddCourseTypeDetailCommandValidator : AbstractValidator<AddCourseTypeDetailCommand>
        {
            public AddCourseTypeDetailCommandValidator()
            {
                RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<AddCourseTypeDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(IAppDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddCourseTypeDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    string userId = _userAccessor.UserId;
                    var checkExisting = _context.CourseTypes.Where(q => q.Title.ToLower() == request.Title.ToLower() && !q.IsDeleted).FirstOrDefault();
                    if (checkExisting != null) throw new AppException("CoursePackage Type with same name already exists!");

                    Domain.Entities.PackageCourse.CourseType courseTypes = new()
                    {
                        Title = request.Title,
                        CreatedBy = userId
                    };

                    await _context.CourseTypes.AddAsync(courseTypes, cancellationToken);
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
