using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource.Application.Command.CoursePackage.Course;
using Resource.Application.Command.CoursePackage.CourseType;
using Resource.Application.Query.CoursePackage.Course;
using Resource.Application.Query.CoursePackage.CourseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceAPI.Controllers.CoursePackage
{
    public class CourseController : BaseController
    {
        #region begin course type
        [HttpGet("Type/Get/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(request: new GetAllCourseTypeDetail.GetAllCourseTypeQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
        [HttpGet("Type/Get/GetById/{id}")]
        public async Task<IActionResult> GetSingleById(string id)
        {
            return Ok(await Mediator.Send(request: new GetSingleCourseTypeDetail.GetSingleCourseTypeQuery { Id = id }));
        }

        /// <summary> 
        /// <param name="title"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
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
        //[Permission(Permission.)]
        [HttpPut("Type/Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateCourseTypeDetail.UpdateCourseTypeDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
        #endregion end course type

        #region begin course
        [HttpGet("Get/GetAll")]
        public async Task<IActionResult> GetCourseAll()
        {
            return Ok(await Mediator.Send(request: new GetAllCourseDetail.GetAllCourseQuery()));
        }

        /// <summary>
        /// <param name="Id"></param>
        /// </summary>
        /// <returns></returns>
        //[Permission(Permission.)]
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
        //[Permission(Permission.)]
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
        //[Permission(Permission.)]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCourse(string id, UpdateCourseDetail.UpdateCourseDetailCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }


        #endregion end course
    }
}
