using CarCenter.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCenter.Controllers
{
    public class SiteController : Controller
    {
        //
        // GET: /Site/

        public ActionResult Index(string searchtext = null)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                if (searchtext == null)
                {
                    var query = session.QueryOver<Site>().OrderBy(x => x.Address).Asc.List();

                    return View(query);
                }

                else
                {
                    var query = session.QueryOver<Site>().Where(x => x.Address == searchtext).OrderBy(x => x.Address).Asc.List();

                    return View(query);
                }
            }

        }

        //
        // GET: /Telephely/Details/5


        public ActionResult Details(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {

                Site telephely = session.Get<Site>(id);

                if (telephely == null)
                {
                    return HttpNotFound();
                }
                ViewBag.telep = telephely.ZipCode + ", " + telephely.Address;
                ViewBag.foglalt = telephely.ReservedCount;
                return View(session.QueryOver<Car>().Where(x => x.SiteId == telephely.Id).List());

            }

        }
        //
        // GET: /Telephely/Create
        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Telephely/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Create(Site telephely)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            session.Save(telephely);

                            trans.Commit();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return View(telephely);
                        }

                    }
                    else
                    {
                        return View(telephely);
                    }


                }
            }

        }

        //
        // GET: /Telephely/Edit/5
        //[Authorize]
        public ActionResult Edit(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Site telephely = session.Get<Site>(id);
                if (telephely == null)
                {
                    return HttpNotFound();
                }
                return View(telephely);
            }

        }

        //
        // POST: /Telephely/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Edit(Site telephely)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            Site site = session.Get<Site>(telephely.Id);
                            site.Address = telephely.Address;
                            site.ZipCode = telephely.ZipCode;
                            site.ParkSlotCount = telephely.ParkSlotCount;
                            session.Save(site);
                            trans.Commit();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return View(telephely);
                        }

                    }
                    else
                    {
                        return View(telephely);
                    }
                }

            }

        }

        //
        // GET: /Telephely/Delete/5
        //[Authorize]
        public ActionResult Delete(int id = 0)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Site telephely = session.Get<Site>(id);
                if (telephely == null)
                {
                    return HttpNotFound();
                }
                return View(telephely);
            }

        }

        //
        // POST: /Telephely/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    Site telephely = session.Get<Site>(id);
                    session.Delete(telephely);
                    trans.Commit();
                    return RedirectToAction("Index");
                }
            }

        }

    }
}
