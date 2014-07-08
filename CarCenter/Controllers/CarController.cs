using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarCenter.Models;
using NHibernate;

namespace CarCenter.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/

        public ActionResult Index(string searchtext = null, int searchform = -1)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var car = session.QueryOver<Car>().OrderBy(x => x.Brand).Asc.ThenBy(x => x.Type).Asc.List();
                
                if(searchtext != null && searchform != -1)
                {
                    int ev = 0;
                    if (searchform == 3)
                    {
                        ev = Convert.ToInt16(searchtext);
                    }
                    switch (searchform)
                    {
                        case 1:
                            {
                                car.Clear();
                                foreach (Car item in session.QueryOver<Car>().OrderBy(x => x.Type).Asc.List())
                                {
                                    if (item.Brand.ToLower().StartsWith(searchtext.ToLower()))
                                        car.Add(item);
                                }
                                break;
                            }
                        case 2:
                            {
                                car.Clear();
                                foreach (Car item in session.QueryOver<Car>().OrderBy(x => x.Vintage).Asc.List())
                                {
                                    if (item.Type.ToLower().StartsWith(searchtext.ToLower()))
                                        car.Add(item);
                                }
                                break;
                            }
                        case 3:
                            {
                                car = session.QueryOver<Car>().Where(x => x.Vintage == ev).OrderBy(x => x.Brand).Asc.List();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                return View(car);
            }

        }

        //
        // GET: /Auto/Details/5

        public ActionResult Details(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Car auto = session.Get<Car>(id);
                if (auto == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    Site telep = session.Get<Site>(auto.SiteId);
                    CarView av = new CarView(auto, telep);
                    return View(av);
                }
            }
        }

        ////
        //// GET: /Auto/Create
        //[Authorize]
        public ActionResult Create()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                ViewBag.telep = new SelectList(session.QueryOver<Site>().List(), "Id", "Address");
                return View();
            }
        }

        ////
        //// POST: /Auto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Create(Car auto, int telep)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    if (ModelState.IsValid && (session.Get<Site>(telep).ReservedCount < session.Get<Site>(telep).ParkSlotCount))
                    {
                        try
                        {
                            session.Get<Site>(telep).ReservedCount += 1;
                            auto.SiteId = telep;
                            session.Save(auto);
                            trans.Commit();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return View(auto);
                        }

                    }
                }
            }

            return View(auto);
        }

        ////
        //// GET: /Auto/Edit/5
        //[Authorize]
        public ActionResult Edit(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Car auto = session.Get<Car>(id);

                if (auto == null)
                {
                    return HttpNotFound();
                }

                ViewBag.telephely = new SelectList(session.QueryOver<Site>().List(), "Id", "Address", auto.SiteId);

                return View(auto);
            }

        }

        ////
        //// POST: /Auto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Edit(Car auto, int telephely)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    if (ModelState.IsValid && (session.Get<Site>(telephely).ReservedCount < session.Get<Site>(telephely).ParkSlotCount))
                    {
                        try
                        {
                            Car car = session.Get<Car>(auto.Id);
                            session.Get<Site>(car.SiteId).ReservedCount -= 1;
                            session.Get<Site>(telephely).ReservedCount += 1;
                            car.Brand = auto.Brand;
                            car.Condition = auto.Condition;
                            car.PreviousOwners = auto.PreviousOwners;
                            car.ProductionTime = auto.ProductionTime;
                            car.SiteId = telephely;
                            car.Type = auto.Type;
                            car.Vintage = auto.Vintage;
                            session.Save(car);
                            trans.Commit();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return View(auto);
                        }

                    }
                }
            }
            return View(auto);
        }

        ////
        //// GET: /Auto/Delete/5
        //[Authorize]
        public ActionResult Delete(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Car auto = session.Get<Car>(id);
                if (auto == null)
                {
                    return HttpNotFound();
                }
                return View(auto);
            }
        }

        ////
        //// POST: /Auto/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    using (ISession session = NHibernateSession.OpenSession())
        //    {
        //        using (ITransaction trans = session.BeginTransaction())
        //        {
        //            Car auto = session.Get<Car>(id);
        //            session.Get<Site>(auto.SiteId).ReservedCount -= 1;

        //            session.Delete(auto);
        //            trans.Commit();
        //            return RedirectToAction("Index");
        //        }

        //    }
        //}
        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    Car auto = session.Get<Car>(id);
                    session.Delete(auto);
                    trans.Commit();
                    return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
        }


    }
}
