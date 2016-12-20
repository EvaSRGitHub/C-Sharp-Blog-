using CSh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CSh.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment/Create
        [Authorize]
        public ActionResult CreateComment(int id)
        {
            //ViewBag.ArticleId = id;
           var model  = new CommentViewModels();
            model.ArticleId = id;
            return View(model);
        }

        //POST: Comment/Create
        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(CommentViewModels model)
            //FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //InsertComment in DB
                using (var database = new BlogDbContext())
                {
                    //Get comentator id
                    var comentatorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;
                    
                    //Set Comment
                    var comment = new Comment();
                    comment.CommentContent = model.CommentContent;
                    comment.UserId = comentatorId;
                    comment.ArticleId = model.ArticleId;

                
                    
                    //Save comment in DB
                    database.Comments.Add(comment);
                    database.SaveChanges();

                    return RedirectToAction("Details", "Article", new { id = comment.ArticleId });
                }
            }

            return View(model);
        }

        //GET: Comment/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                //Get comment from database
                var comment = database.Comments
                    .Where(cm => cm.Id == id)
                    .FirstOrDefault();


                if (!IsUserAuthorizedToEdit(comment))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                //Check if comment exists
                if (comment == null)
                {
                    return HttpNotFound();
                }

                //Pass comment to view
                return View(comment);
            }
        }

        //POST: Comment/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                //Get article from DB
                var comment = database.Comments
                    .Where(a => a.Id == id)
                    .FirstOrDefault();

                //Check if article exists
                if (comment == null)
                {
                    return HttpNotFound();
                }

                //Remove article from DB
                database.Comments.Remove(comment);
                database.SaveChanges();

                //Redirect to index page
                return RedirectToAction("Details", "Article", new { id = comment.ArticleId });
               
            }
        }

        //GET: Comment/Edit
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                //Get comment from database
                var comment = database.Comments
                    .Where(cm => cm.Id == id)
                    .FirstOrDefault();


                if (!IsUserAuthorizedToEdit(comment))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                //Check if comment exists
                if (comment == null)
                {
                    return HttpNotFound();
                }

                //Create the view model
                var model = new CommentViewModels();
                model.Id = comment.Id;
                model.ArticleId = comment.ArticleId;
                model.CommentContent = comment.CommentContent;

                //Pass view model to View
                return View(model);
            }
        }

        //POST: Comment/Edit
        [HttpPost]
        public ActionResult Edit(CommentViewModels model)
        {
            //Check if model state is valid
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    //Get comment from database
                    var comment = database.Comments
                        .FirstOrDefault(a => a.Id == model.Id);

                    //Set comment properties
                    comment.Id = model.Id;
                    comment.CommentContent = model.CommentContent;

                    //Save comment state in database
                    database.Entry(comment).State = EntityState.Modified;
                    database.SaveChanges();

                    //Redirect to the page
                    return RedirectToAction("Details", "Article", new { id = comment.ArticleId});
                }
            }
            //if model state is invalid, return the same view
            return View(model);
        }



        private bool IsUserAuthorizedToEdit(Comment comment)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isComentator = comment.IsComentator(this.User.Identity.Name);
           // this.User.IsInRole("User");

            return isAdmin || isComentator;
        }
    }
}