using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Transfer8Pro.CommunalWebAPI.Controllers.User
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [SwaggerControllerGroup("用户", "管理")]
    [RoutePrefix("api/user/user")]
    public class UserController : ApiController
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [Route("GetUser")]
        public string GetUser(int userId)
        {
            return "get user success";
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("PostUser")]
        public string PostUser(User user)
        {
            return "post user success";
        }

        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [Route("PostUsers")]
        public IHttpActionResult PostUsers(List<User> users)
        {
            // System.Collections.Generic.HashSet<string>
            List<User> userList = new List<User>();
            userList.Add(new User { Name = "张三", UserId = 1 });
            userList.Add(new User { Name = "张四", UserId = 2 });
            return Ok(userList);
        }
    }

    /// <summary>
    /// 用户实体
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>

        public string Name { get; set; }
    }
}