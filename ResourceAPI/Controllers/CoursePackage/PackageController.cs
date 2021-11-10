using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.CoursePackage.Package;
using Resource.Application.Command.CoursePackage.Promo;
using Resource.Application.Command.Customer;
using Resource.Application.Query.CoursePackage.Package;
using Resource.Application.Query.CoursePackage.Promo;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.CoursePackage
{

    public class PackageController : BaseController
    {
       
        #region begin package
        [HttpGet("Get/GetAll")]
        [Permission(PackageCourseClaimConstant.ViewPackage)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllPackageDetail.GetAllPackageQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get/GetById/{id}")]
        [Permission(PackageCourseClaimConstant.ViewPackage)]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSinglePackageDetail.GetSinglePackageQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        [Permission(PackageCourseClaimConstant.WritePackage)]
        public async Task<IActionResult> Create(AddPackageDetail.AddPackageDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [Permission(PackageCourseClaimConstant.WritePackage)]
        public async Task<IActionResult> Update(string id, UpdatePackageDetail.UpdatePackageDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        [Permission(PackageCourseClaimConstant.WritePackage)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeletePackageDetail.DeletePackageDetailCommand { Id = id }));
        }
        #endregion end course type

        #region begin promo type
        [HttpGet("Promo/Get/GetAll")]
        [Permission(PackageCourseClaimConstant.ViewPromo)]
        public async Task<IActionResult> GetPromoAll()
        {
            return Ok(await Mediator.Send(request: new GetAllPromoDetail.GetAllPromoQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Promo/Get/GetById/{id}")]
        public async Task<IActionResult> GetSinglePromoById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSinglePromoDetail.GetSinglePromoQuery { Id = id }));
        }
        /// <summary>
        /// <param name="promoCode"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Promo/Get/GetByPromoCode/{promoCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSinglePromoByPromoCode(string promoCode)
        {
            return Ok(await Mediator.Send(request: new GetSinglePromoDetailByPromoCode.GetSinglePromoQuery { PromoCode = promoCode }));
        }
        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPost("Promo/Create")]
        public async Task<IActionResult> CreatePromo(AddPromoDetail.AddPromoDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name=    "title"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpPut("Promo/Update/{id}")]
        public async Task<IActionResult> UpdatePromo(string id, UpdatePromoDetail.UpdatePromoDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpDelete("promo/Delete/{id}")]
        public async Task<IActionResult> DeletePromo(string id)
        {
            return Ok(await Mediator.Send(request: new DeletePromoDetail.DeletePromoDetailCommand { Id = id }));
        }
        #endregion end course type


        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost("RegisterCustomerPackage")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomerPackage(AddCustomerPackageDetail.AddCustomerPackageDetailCommand command)
        {
            return Ok(await Mediator.Send(command));

        }
    }
}
