using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;
using Transfer8Pro.WebApp.Infrastructure.WebServices;
using Transfer8Pro.WebApp.Models;

namespace Transfer8Pro.WebApp.Controllers
{
    public class SystemUserController : BaseController
    {
        private SystemUserService userBll = null;
        private SystemRoleService roleBll = null;
        private SystemFunctionService funBll = null;

        public SystemUserController()
        {
            userBll = new SystemUserService();
            roleBll = new SystemRoleService();
            funBll = new SystemFunctionService();
        }
        // GET: SystemUser
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(SystemUserViewEntity prms)
        {
            var pageObj = userBll.GetList(prms);
            var res = JsonObj.ToJson(new { Status = 1, rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult SetRole(string pid, int rid = 0)
        {
            List<SystemRoleEntity> roleList = roleBll.GetList();
            ViewBag.roleList = roleList;
            ViewBag.userID = pid;
            ViewBag.roleID = rid;

            return View();
        }

        [HttpPost]
        public ActionResult SetRole(SystemUserRelRoleEntity prms)
        {
            if (prms == null || prms.RoleID == 0 || string.IsNullOrEmpty(prms.UserID))
            {
                return ErrorResult("系统参数丢失");
            }

            if (new PassportServiceProxy().UpdateUserStateBySysID(prms.UserID, ConfigHelper.GetConfig("SystemID", "17").ToInt()))
            {
                bool isSuccess = userBll.InsertOrUpdateRole(prms.UserID, prms.RoleID);
                if (isSuccess)
                {
                    return OkResult("保存成功");
                }
                else
                {
                    return ErrorResult("保存失败");
                }
            }
            else
            {
                return ErrorResult("OBWeb服务接口异常");
            }           
        }

        [HttpPost]
        public ActionResult GetNewUsers()
        {
            //DataSet ds = new PassportServiceProxy().GetAllEmpInfo();
            DataSet ds = new PassportServiceProxy().GetAllUserBySysID(ConfigHelper.GetConfig("SystemID", "17").ToInt());
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return ErrorResult("未获取到最新员工数据");
            }

            bool isSuccess = userBll.InsertOrUpdate(ds.Tables[0]);
            if (isSuccess)
            {
                return OkResult("获取成功");
            }
            else
            {
                return ErrorResult("获取失败");
            }
        }

        public ActionResult RoleList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RoleList(SystemRoleEntity prms)
        {
            var pageObj = roleBll.GetList(prms);
            var res = JsonObj.ToJson(new { Status = 1, rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult RoleTree(int roleID)
        {
            if (roleID == 0)
            {
                return  RedirectToAction("HttpError404","Error",new { error = "未找到指定数据" });
            }
            ViewBag.RoleID = roleID;
            return View();
        }

        [HttpPost]
        public ActionResult MenuList(int roleID)
        {
            if (roleID == 0)
            {
                return ErrorResult("参数错误");
            }
            List<SystemRoleRelFunEntity> roleRelFunList = roleBll.GetRoleRelFunList(roleID);
            List<SystemFunctionEntity> funList = funBll.GetList();
            var selectedFuns = roleRelFunList.Select(item => item.FunctionID);

            var jsTreeList = funList.Select(item => new JsTreeEntity {
                id = item.FunctionID.ToString(),
                text = item.FunctionName,
                pid = item.FunctionParentID.ToString(),
                state = new State { disabled = item.FunctionIsValid != 1 ? true :false , selected = selectedFuns.Contains(item.FunctionID)}
            }).ToList();

            JsTreeEntity rootTree = new JsTreeEntity() { id="0",text="传8后台权限" };
            SetTreeChildNode(rootTree, jsTreeList);

            return OkResult(rootTree);
        }

        [HttpPost]
        public ActionResult SaveRoleMenu(List<SystemRoleRelFunEntity> menus)
        {           
            if (menus == null || menus.Count == 0)
            {
                return ErrorResult("请至少选择一条数据");
            }

            bool isSuccess = roleBll.ChangeRole_Fun_Rel(menus);
            if (isSuccess)
            {
                return OkResult("权限菜单设置成功");
            }
            else
            {
                return ErrorResult("权限菜单设置失败");
            }
        }

        private void SetTreeChildNode(JsTreeEntity parentNode,List<JsTreeEntity> source)
        {
            foreach (JsTreeEntity model in source)
            {
                if (model.pid == parentNode.id)
                {
                    SetTreeChildNode(model, source);

                    if (!model.state.selected)
                    {
                        parentNode.state.selected = false;
                    }

                    parentNode.children.Add(model);
                }
            }
        }
    }
}