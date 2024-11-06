using IoTWebPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTWebPanel.Controllers
{
    public class EquipmentListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Select()
        {
            using (ALNDBContext db = new ALNDBContext())
            {
                var row_data = db.EquipmentLists.Where(x=>x==x).ToList();
                return View(row_data[0]);
            }
        }

        public ActionResult Create()
        {
            using (ALNDBContext db = new ALNDBContext())
            {
                EquipmentList equip = new EquipmentList() 
                {
                    Guid = Guid.NewGuid().ToString(),
                    EquipName = "Test",
                    EquipIp = "127.0.0.1",
                    IsEnable = false,
                };
                db.EquipmentLists.Add(equip);
                db.SaveChanges();
                return View(equip);
            }
        }

        public ActionResult Edit() 
        {
            using (ALNDBContext db = new ALNDBContext())
            {
                var row_data = db.EquipmentLists.Where(x => x == x).ToList();
                var temp = db.EquipmentLists.Where(x=>x.Guid == row_data[0].Guid).SingleOrDefault();
                if (temp != null)
                {
                    temp.EquipName = "Test_Modify";
                    db.EquipmentLists.Update(temp);
                    db.SaveChanges();
                    return View(temp);
                }
                else
                {
                    return View(new EquipmentList() {
                        EquipName= "NA",
                    });
                }
            }
        }

        public ActionResult Delete(string? guid)
        {
            using (ALNDBContext db = new ALNDBContext())
            {
                var temp = db.EquipmentLists.Where(x => x.Guid == guid).SingleOrDefault();
                if (temp != null)
                {
                    db.EquipmentLists.Remove(temp);
                    db.SaveChanges();
                    ViewBag.Result = "刪除成功";
                    return View();
                }
                else
                {
                    ViewBag.Result = "刪除失敗";
                    return View();
                }
            }
        }
    }
}
