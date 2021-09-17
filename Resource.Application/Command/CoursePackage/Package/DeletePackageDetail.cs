using Dgm.Common.Error;
using MediatR;
using Resource.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Command.CoursePackage.Package
{
    public class DeletePackageDetail
    {
        public class DeletePackageDetailCommand : IRequest
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<DeletePackageDetailCommand, Unit>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeletePackageDetailCommand request, CancellationToken cancellationToken)
            {
                var transaction = await _context.Instance.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var existing = _context.Packages.Where(q => q.Id == request.Id && !q.IsDeleted).SingleOrDefault();
                    if (existing == null) throw new AppException("Invalid! Package Detail not found!");

                    existing.IsDeleted = true;

                    _context.Packages.Update(existing);
                    
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
