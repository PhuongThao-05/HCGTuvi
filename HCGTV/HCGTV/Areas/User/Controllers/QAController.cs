using HCGTV.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace HCGTV.Areas.User.Controllers
{
    public class QAController : Controller
    {
        Models.TUVIEntities db = new Models.TUVIEntities();
        // GET: User/QA
        public ActionResult Index()
        {
            if (Session["HasVisitedIntro"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var questions = db.LOAIDD
               .Include(x => x.DACDIEM)
               .ToList()
               .Where(x => x.IDLDD < 11)
               .ToList();
            ViewBag.Questions = questions;
            return View();
        }
        [HttpPost]
        public ActionResult LuuLuaChon(string answers)
        {
            string chuoiDapAn = answers;
            int makl; // Biến để lưu mã mệnh
            string prediction; // Biến để lưu mô tả
            string moTa; // Biến để lưu mô tả

                // Tìm luật trùng khớp với chuỗi đáp án
                var luatTrung = db.LUAT
                                 .Where(l => l.NDLUAT == chuoiDapAn)
                                 .FirstOrDefault();

                if (luatTrung != null)
                {
                    // Nếu có luật trùng, lấy mã mệnh tương ứng
                    makl = luatTrung.KETLUAN.MAKL;
                    prediction = db.KETLUAN
                             .Where(m => m.MAKL == makl)
                             .Select(m => m.NDKL)
                             .FirstOrDefault() ?? "Không có dữ liệu"; // Mô tả mặc định nếu không tìm thấy
                    // Lấy mô tả của mệnh từ bảng KETLUAN
                    moTa = db.KETLUAN
                             .Where(m => m.MAKL == makl)
                             .Select(m => m.MOTA)
                             .FirstOrDefault() ?? "Mô tả không có"; // Mô tả mặc định nếu không tìm thấy
                }
                else
                {
                    // Nếu không tìm thấy luật trùng, thiết lập mã mệnh và mô tả mặc định
                    makl = 10;
                    prediction = db.KETLUAN
                             .Where(m => m.MAKL == makl)
                             .Select(m => m.NDKL)
                             .FirstOrDefault() ?? "Không có dữ liệu"; // Mô tả mặc định nếu không tìm thấy
                    moTa = db.KETLUAN
                             .Where(m => m.MAKL == makl)
                             .Select(m => m.MOTA)
                             .FirstOrDefault() ?? "Mô tả không có";// Cập nhật mô tả theo ý muốn
                }

                // Truyền thông tin mã mệnh và mô tả vào view
                ViewBag.Makl = makl;
                ViewBag.NDKL = prediction;
                ViewBag.MoTa = moTa;
                ViewBag.Result = !string.IsNullOrEmpty(chuoiDapAn) && db.LUAT.Any(l => l.NDLUAT == chuoiDapAn);

                // Trả về view KetQua
                return View("KetQua");

        }
        public ActionResult KetQua()
            {
                return View();
            }

        public ActionResult Continue(int? id)
        {
            if (Session["HasVisitedIntro"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(id != null)
            {
                var maKL = db.KETLUAN.Where(m => m.MAKL == id).Select(m => m.MAQUYUOC).FirstOrDefault();
                ViewBag.KL = maKL;
            }    
            var questions = db.LOAIDD
               .Include(x => x.DACDIEM)
               .ToList()
               .Where(x => x.IDLDD > 10)
               .ToList();
            ViewBag.Questions = questions;
            return View();
        }

        [HttpPost]
        public ActionResult YourSelected(string answers)
        {
            string chuoiDapAn = answers;
            int makl; // Biến để lưu mã mệnh
            string prediction; // Biến để lưu mô tả
            string moTa; // Biến để lưu mô tả
            // Tìm luật trùng khớp với chuỗi đáp án
            var luatTrung = db.LUAT
                             .Where(l => l.NDLUAT == chuoiDapAn)
                             .FirstOrDefault();

            if (luatTrung != null)
            {
                // Nếu có luật trùng, lấy mã mệnh tương ứng
                makl = luatTrung.KETLUAN.MAKL;
                prediction = db.KETLUAN
                         .Where(m => m.MAKL == makl)
                         .Select(m => m.NDKL)
                         .FirstOrDefault() ?? "Không có dữ liệu"; // Mô tả mặc định nếu không tìm thấy                                                // Lấy mô tả của mệnh từ bảng KETLUAN
                moTa = luatTrung.LOIKHUYEN ?? "Không có"; // Mô tả mặc định nếu không tìm thấy
            }
            else
            {
                // Nếu không tìm thấy luật trùng, thiết lập mã mệnh và mô tả mặc định
                makl = 13;
                prediction = db.KETLUAN
                         .Where(m => m.MAKL == makl)
                         .Select(m => m.NDKL)
                         .FirstOrDefault() ?? "Không có dữ liệu"; // Mô tả mặc định nếu không tìm thấy
                moTa = db.KETLUAN
                         .Where(m => m.MAKL == makl)
                         .Select(m => m.MOTA)
                         .FirstOrDefault() ?? "Mô tả không có";// Cập nhật mô tả theo ý muốn
            }

            // Truyền thông tin mã mệnh và mô tả vào view
            ViewBag.Makl = makl;
            ViewBag.NDKL = prediction;
            ViewBag.MoTa = moTa;
            ViewBag.Result = !string.IsNullOrEmpty(chuoiDapAn) && db.LUAT.Any(l => l.NDLUAT == chuoiDapAn);

            // Trả về view Ket Qua
            return View("Lastresult");

        }
        public ActionResult Lastresult()
        {
            return View();
        }

    }
}
