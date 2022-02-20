using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ThuForm.Models;


namespace ThuForm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        FormEntities db = new FormEntities();
        //int pageSize = 5;
        // GET: Home

        [AllowAnonymous]
        public ActionResult Index()
        {
          var registform = db.RegistForm.ToList();
          //int currentPage = page < 1 ? 1 : page;
          //var products = db.RegistForm.OrderBy(m => m.DataID).ToList();
          //var result = products.ToPagedList(currentPage, pageSize);
          return View(registform);
        }

        [AllowAnonymous]
        public ActionResult Theme()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult ManagePage()
        {
            var registform2 = db.RegistForm.ToList();
            //int currentPage = page < 1 ? 1 : page;
            //var products2 = db.RegistForm.OrderBy(m => m.DataID).ToList();
            //var result = products2.ToPagedList(currentPage, pageSize);
            return View(registform2);
        }

        [AllowAnonymous]
        public ActionResult Form()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Form(RegistForm registForm)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = false;
                var temp = db.RegistForm.Where(m => m.DataID == registForm.DataID)
                    .FirstOrDefault();
                if (temp != null)
                {
                    ViewBag.Error = true;
                    return View(registForm);
                }
                db.RegistForm.Add(registForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registForm);
        }

        [Authorize]
        public ActionResult Edit(int DataID)
        {
            var employee = db.RegistForm
                .Where(m => m.DataID == DataID).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(RegistForm registForm)
        {
            if (ModelState.Count > 0)
            {
                var temp = db.RegistForm
                    .Where(m => m.DataID == registForm.DataID)
                    .FirstOrDefault();
                temp.Id = registForm.Id;
                temp.Name = registForm.Name;
                temp.Total = registForm.Total;
                temp.Mail = registForm.Mail;
                temp.Note = registForm.Note;
                //temp.isActive = employee.isActive;
                db.SaveChanges();
                return RedirectToAction("ManagePage");
            }
            return View(registForm);
        }

        [Authorize]
        public ActionResult Delete(int DataId)
        {
            var employee = db.RegistForm
                .Where(m => m.DataID == DataId).FirstOrDefault();
            db.RegistForm.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("ManagePage");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();   // 登出
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string txtUid, string txtPwd)
        {
            string[] uidAry = new string[] { "thu1314" };
            string[] pwdAry = new string[] { "im1314" };

            // 循序搜尋法
            int index = -1;
            for (int i = 0; i < uidAry.Length; i++)
            {
                if (uidAry[i] == txtUid && pwdAry[i] == txtPwd)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ViewBag.Err = "帳密錯誤!";
            }
            else
            {
                // 表單驗證服務，授權指定的帳號
                FormsAuthentication.RedirectFromLoginPage(txtUid, true);
                return RedirectToAction("ManagePage");
            }
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}