using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myfreela.context;
using myfreela.Models;
using myfreela.viewmodels;

namespace myfreela.Controllers
{   


    public class ProjectsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MyFreelaContext _projectsContext;
        public ProjectsController(UserManager<User> userManager, MyFreelaContext projectsContext)
        {
            _userManager = userManager;
            _projectsContext = projectsContext;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {   

            var user = await GetCurrentUser();
            
            if (user == null)
            {   
                return RedirectToAction("Index","User");
            } 

            var projects = GetAllProjects(user.Id);

            return View(projects);  

        }

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(RegisterProjectsViewModel projectsViewModel)
        {
            
            try
            {   
                var user = await GetCurrentUser();
                var projectsDb = new Projects();

                MapperProjectsViewModelToProjectsDb(projectsViewModel,projectsDb,user!.Id);
                
                await _projectsContext.Projects!.AddAsync(projectsDb);
                await _projectsContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("","Erro ao salvar os dados");
            }
            catch (OperationCanceledException)
            {
                ModelState.AddModelError("","Operação cancelada");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("",$"Algum erro ocorreu {e.ToString()}");
            }

            return View();
            
        }

        public async Task<IActionResult> Edit(int projectId)
        {
            var currentUser = await GetCurrentUser();

            if (currentUser == null)
            {   
                return RedirectToAction("Index","User");
            }

            var project = GetProjectById(projectId, currentUser.Id);
            var projectVm = new RegisterProjectsViewModel();

            if (project != null)
            {
                projectVm.Id = project.Id;
                projectVm.Name = project.Name;
                projectVm.PriceByHours = project.PriceByHours;

                return View(projectVm);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(RegisterProjectsViewModel projectsViewModelEdit)
        {   
            
            var currentUser = await GetCurrentUser();

            if (currentUser == null)
            {   
                return RedirectToAction("Index","User");
            }

            var project = GetProjectById(projectsViewModelEdit.Id,currentUser.Id);
            
            if (project != null)
            {   
                try
                {
                    project.Name = projectsViewModelEdit.Name;
                    project.PriceByHours = projectsViewModelEdit.PriceByHours;

                    var result = _projectsContext.Projects!.Update(project); 
                    await _projectsContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("","Erro ao salvar os dados");
                }
                catch (OperationCanceledException)
                {
                    ModelState.AddModelError("","Operação cancelada");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("",$"Algum erro ocorreu {e.ToString()}");
                }
                return View();
            }

            return RedirectToAction(nameof(Index));

        }

        [Authorize]
        public async Task<IActionResult> StartOrPause(int projectId)
        {   
            var currentUser = await GetCurrentUser();
            
            if (currentUser == null)
            {   
                return RedirectToAction("Index","User");
            }

            var project = GetProjectById(projectId,currentUser.Id);

            if (project != null)
            {
                if (project.Status == ProjectStatus.Pendente || project.Status == ProjectStatus.Pausado)
                {

                    project.LastStart = DateTime.Now;
                    project.LastPause = null;
                    project.Status = ProjectStatus.Ativo;

                    _projectsContext.Projects!.Update(project);
                              
                }
                else
                {
                    if (project.Status == ProjectStatus.Ativo)
                    {

                        project.LastPause = DateTime.Now;
                        project.Status = ProjectStatus.Pausado;
                        project.TotalValue = CalculateTotal(project.LastStart!.Value,project.LastPause.Value,project.TotalValue,project.PriceByHours);
                        
                        _projectsContext.Projects!.Update(project);

                    }
                }
                
                await _projectsContext.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("","Não foi possivel iniciar ou pausar o projeto");      
            }
            
            return RedirectToAction(nameof(Index));
        }

        private decimal CalculateTotal(DateTime lastStart, DateTime lastPause, decimal currentTotalValue, decimal currentPriceByHours)
        {   
            //finalizar
            TimeSpan time = lastPause.Subtract(lastStart);

            var totalMinutes = Math.Round(time.TotalMinutes);

            var valueByMinutes = currentPriceByHours / 60;

            var totalValue = (decimal)totalMinutes * valueByMinutes + currentTotalValue;

            return totalValue;
        }

        private void MapperProjectsViewModelToProjectsDb(RegisterProjectsViewModel projectsViewModel, Projects projectsDb, int userId)
        {
            
            projectsDb.Name = projectsViewModel.Name;
            projectsDb.PriceByHours = projectsViewModel.PriceByHours;
            projectsDb.TotalValue = 0.00M;
            projectsDb.Status = ProjectStatus.Pendente;
            projectsDb.LastStart = null;
            projectsDb.LastPause = null;
            projectsDb.CreatedAt = DateTime.Now;
            projectsDb.UserId = userId;

        }
        
        private List<Projects> GetAllProjects(int userLogged)
        {   
            var projetos = _projectsContext.Projects!.Where(p => p.UserId == userLogged).ToList();
            return projetos;
        }

        private Projects? GetProjectById(int projectId,int userId)
        {   
            return _projectsContext.Projects!.Where(x => x.Id == projectId).Where(x => x.UserId == userId).FirstOrDefault();
        }

        private async Task<User?> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}