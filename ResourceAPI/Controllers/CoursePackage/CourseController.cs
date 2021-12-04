using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Resource;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.CoursePackage.Course;
using Resource.Application.Command.CoursePackage.CourseType;
using Resource.Application.Query.CoursePackage.Course;
using Resource.Application.Query.CoursePackage.CourseType;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.CoursePackage
{
    public class CourseController : BaseController
    {
        #region begin course type
        [HttpGet("Type/Get/GetAll")]
        [Permission(PackageCourseClaimConstant.ViewCourseType)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllCourseTypeDetail.GetAllCourseTypeQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [Permission(PackageCourseClaimConstant.ViewCourseType)]
        [HttpGet("Type/Get/GetById/{id}")]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleCourseTypeDetail.GetSingleCourseTypeQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        [Permission(PackageCourseClaimConstant.WriteCourseType)]
        [HttpPost("Type/Create")]
        public async Task<IActionResult> Create(AddCourseTypeDetail.AddCourseTypeDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        [Permission(PackageCourseClaimConstant.WriteCourseType)]
        [HttpPut("Type/Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateCourseTypeDetail.UpdateCourseTypeDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Type/Delete/{id}")]
        [Permission(PackageCourseClaimConstant.WriteCourseType)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteCourseTypeDetail.DeleteCourseTypeDetailCommand { Id = id }));
        }
        #endregion end course type

        #region begin course
        [HttpGet("Get/GetAll")]
        [Permission(PackageCourseClaimConstant.ViewCourse)]
        public async Task<IActionResult> GetCourseAll()
        {
            return Ok(await Mediator.Send(request: new GetAllCourseDetail.GetAllCourseQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [Permission(PackageCourseClaimConstant.ViewCourse)]
        [HttpGet("Get/GetById/{id}")]
        public async Task<IActionResult> GetSingleCourseById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleCourseDetail.GetSingleCourseQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// <param name="accountTypeId"></param>
        /// </summary>
        /// <returns></returns>
        [Permission(PackageCourseClaimConstant.WriteCourse)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCourse(AddCourseDetail.AddCourseDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>s
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// <param name="accountTypeId"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [Permission(PackageCourseClaimConstant.WriteCourse)]
        public async Task<IActionResult> UpdateCourse(string id, UpdatePackageDetail.UpdateCourseDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        [Permission(PackageCourseClaimConstant.WriteCourse)]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            return Ok(await Mediator.Send(request: new DeleteCourseDetail.DeleteCourseDetailCommand { Id = id }));
        }

        #endregion end course
    }
}
