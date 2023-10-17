using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Entry(User user, int iduser)
        {
            if (iduser == 0)
            {
                User useR = user.Get(user.email, user.password);
                if (useR != null)
                {
                    using (ApplicationContext Db = new ApplicationContext())
                    {
                        if (Db.Users.FirstOrDefault(u => u.email == user.email && u.password == user.password) == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ForEntry entry = new ForEntry();
                            entry.user = useR;
                            entry.usersList = Db.Users.ToList();
                            entry.projects = Db.Projects.ToList();
                            return View(entry);
                        }
                    }
                }
            }
            else
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    ForEntry entry = new ForEntry();
                    User user1 = new User();
                    user1 = user1.GetById(iduser);
                    entry.user = user1;
                    entry.usersList = Db.Users.ToList();
                    entry.projects = Db.Projects.ToList();
                    return View(entry);
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Recovery(User user)
        {
            User useR = new User();
            useR = user.GetByEmail(user.email);
            string body = $"Hello, {useR.name}, your password: {useR.password}";
            var mail = CodeeloMail.CreateMail(user.email, "Recovery your password", body);
            CodeeloMail.SendMail(mail);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult OpenProject(int iduser, int idproject)
        {
            User user = new User();
            user = user.GetById(iduser);
            using (ApplicationContext Db = new ApplicationContext())
            {
                ForSprint proJect = new ForSprint();
                List<Sprint> sprints = Db.Sprints.Where(x => x.idproject == idproject).ToList();
                proJect.sprints = sprints;
                proJect.user = user;
                proJect.usersList = new List<User>();
                proJect.userTasks = new List<UserTask>();
                proJect.tasks = new List<Models.Task>();
                proJect.project = new Project();
                proJect.project.id = idproject;
                return View(proJect);
            }
        }
        public IActionResult OpenTask(int iduser, int idtask, int idproject)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                ForTask tasK = new ForTask();
                User user = new User();
                tasK.user = user.GetById(iduser);
                List<UserTask> userTasks = Db.UserTasks.Where(x => x.idtask == idtask).ToList();
                tasK.userTasks = userTasks;
                List<User> usersList = new List<User>();
                foreach (UserTask userTask in userTasks)
                {
                    usersList.Add(Db.Users.FirstOrDefault(x => x.id == userTask.iduser));
                }
                tasK.usersList = usersList;
                List<UserTask> AlluserTasks = Db.UserTasks.ToList();
                tasK.AlluserTasks = AlluserTasks;
                List<User> AllusersList = Db.Users.ToList();
                tasK.AllusersList = AllusersList;
                Models.Task task = new Models.Task();
                tasK.task = task.GetById(idtask);
                tasK.project = new Project();
                tasK.project.id = idproject;
                tasK.filesList = Db.Files.Where(x => x.idtask == idtask).ToList();
                return View(tasK);
            }
        }
        public IActionResult DeleteProject([FromBody]int idproject)
        {
            try
            {
                Project project = new Project();
                project.Delete(idproject);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public IActionResult Create(User user)
        {
            if (user.name != null && user.email != null && user.password != null)
            {
                var dbContext = new ApplicationContext();
                var existingUser = dbContext.Users.FirstOrDefault(u => u.email == user.email);

                if (existingUser == null)
                {
                    User useR = new User();
                    useR.Add(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Registration", "Home");
                }
            }
            return View();
        }
        public IActionResult Up([FromBody] int id)
        {
            try
            {
                User user = new User();
                user = user.GetById(id);
                if (user != null)
                {
                    user.role = "manager";
                    user.UpDate(user);
                }
                return Json(user);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
           
        }
        public IActionResult Down([FromBody] int id)
        {
            try
            {
                User user = new User();
                user = user.GetById(id);
                if (user != null)
                {
                    user.role = "user";
                    user.UpDate(user);
                }
                return Json(user);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
        public IActionResult CreateNewTask(int idsprint, int iduser, int idproject)
        {
            ForTask fortask = new ForTask();
            fortask.user = new User();
            fortask.user = fortask.user.GetById(iduser);
            fortask.sprint = new Sprint();
            fortask.sprint.id = idsprint;
            fortask.project = new Project();
            fortask.project.id = idproject;
            return View(fortask);
        }

        public IActionResult CCreateNewTask(ForTask forTask, int iduser, int idsprint, int idproject)
        {
            if (forTask != null)
            {
                Models.Task tasK = new Models.Task();
                tasK.Add(forTask.task, idsprint);
                User user = new User();
                user = user.GetById(iduser);
            }
            return RedirectToAction("OpenProject", new { iduser = iduser, idproject = idproject });
        }
        public IActionResult DeleteTask([FromBody] int idtask)
        {
            try
            {
                Models.Task task = new Models.Task();
                task.Delete(idtask);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        
        }

        public IActionResult CreateNewSprint(int iduser, int idproject)
        {
            ForSprint forSprint = new ForSprint();
            forSprint.user = new User();
            forSprint.user = forSprint.user.GetById(iduser);
            forSprint.project = new Project();
            forSprint.project.id = idproject;
            return View(forSprint);
        }

        public IActionResult CCreateNewSprint(ForSprint forSprint, int iduser, int idproject)
        {

            if (forSprint != null)
            {

                Sprint sprinT = new Sprint();
                sprinT.Add(forSprint.sprint, idproject);
                User user = new User();
                user = user.GetById(iduser);

            }
            return RedirectToAction("OpenProject", new { iduser = iduser, idproject = idproject });
        }
        public IActionResult DeleteSprint([FromBody]int idsprint)
        {
            try
            {
                Sprint sprint = new Sprint();
                sprint.Delete(idsprint);
                return Json(true);
            }
            catch(Exception ex) { return Json(false); }  
        }
        public IActionResult CreateNewProject(int iduser)
        {
            ForProject forProject = new ForProject();
            forProject.user = new User();
            forProject.user.id = iduser;
            return View(forProject);
        }

        public IActionResult CCreateNewProject(ForProject forProject, int iduser)
        {
            if (forProject != null)
            {
                Project projecT = new Project();
                projecT.Add(forProject.project);
            }
            return RedirectToAction("Entry", new { iduser = iduser });
        }
        public IActionResult RemoveFromTask([FromBody] int iduser)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    User user = new User();
                    user = user.GetById(iduser);
                    List<UserTask> userTasks = Db.UserTasks.ToList();
                    foreach (var item in userTasks)
                    {
                        if (item.iduser == user.id)
                        {
                            userTasks.Remove(item);
                            UserTask userTask = new UserTask();
                            userTask.Delete(item.id);
                            break;
                        }
                    }
                }
                return Json(true);
            }
            catch (Exception ex) 
            {
                return Json(false);
            }
        }
        public IActionResult AddToTask([FromBody] Models.Task task)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    UserTask userTask = new UserTask();
                    userTask.Add(task.id, task.idsprint);
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
        }
        public IActionResult DeleteFromProject([FromBody]int iduser)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    User user = new User();
                    user = user.GetById(iduser);
                    Db.UserTasks.RemoveRange(Db.UserTasks.Where(x => x.iduser == user.id).ToList());
                    Db.SaveChanges();
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        public IActionResult CheckTask([FromBody]int idtask)
        {
            try
            {
                Models.Task task = new Models.Task();
                task = task.GetById(idtask);
                task.Update(task);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public IActionResult Upload([FromForm] ForTask model, [FromForm] int idtask)
        {
            if (model.fileuploades.FileToUpload != null)
            {

                model.fileuploades.path = "wwwroot/Files";

                model.fileuploades.name = Path.GetFileName(model.fileuploades.FileToUpload.FileName);

                var filePath = Path.Combine(model.fileuploades.path, model.fileuploades.name);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.fileuploades.FileToUpload.CopyTo(fileStream);
                    using (ApplicationContext Db = new ApplicationContext())
                    {
                        model.fileuploades.idtask = idtask;
                        
                        Db.Files.Add(model.fileuploades);
                        Db.SaveChanges();
                        return Json(model);
                    }
                }

            }

            return Json(null);
        }
        public IActionResult RemoveFile([FromBody] int id)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    Files fileUploadModel = Db.Files.FirstOrDefault(x => x.id == id);

                    if (fileUploadModel != null)
                    {
                        string path = Path.Combine(fileUploadModel.path, fileUploadModel.name);
                        System.IO.File.Delete(path);


                        Db.Files.Remove(fileUploadModel);
                        Db.SaveChanges();

                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }
        public IActionResult AllComments([FromBody] int id)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                List<Comments> allcomment = Db.Comments.Where(x=>x.idtask==id).ToList();
                List<User> users = new List<User>();
                foreach (Comments comment in allcomment)
                {
                    users.Add(Db.Users.FirstOrDefault(x=>x.id==comment.iduser));
                }
                var data = new
                {
                    user = users,
                    comment= allcomment
                };
                return Json(data);
            }
            
        }
        
        public IActionResult AddComments([FromBody] Comments comments)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                User user = Db.Users.FirstOrDefault(x=>x.id == comments.iduser);
                Db.Comments.Add(comments);
                Db.SaveChanges();
                var data = new
                {
                    users = user,
                    comment = comments
                };
                return Json(data);
            }

        }
    }
}
