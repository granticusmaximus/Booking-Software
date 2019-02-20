using AutoMapper;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace coderush.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper  _mapper;

        public Functional(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IRoles roles,
           IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
           IHttpContextAccessor httpContextAccessor,
           IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

      

        public async Task InitAppData()
        {
            try
            {
                //todo: add initialization data

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendEmailBySendGridAsync(string apiKey, 
            string fromEmail, 
            string fromFullName, 
            string subject, 
            string message, 
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);

        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);

            }

        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.EmployeeCode = "000#EMP";
                    profile.FirstName = "Super";
                    profile.LastName = "Admin";
                    profile.Email = superAdmin.Email;
                    profile.ApplicationUserId = superAdmin.Id;
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();

                    await _roles.AddToRoles(superAdmin.Id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }

            return result;
        }

        public string GetCurrentLoginUserId()
        {
            string result;
            try
            {
                result = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public void AddAuditInfo<T>(ref T entity)
        {
            try
            {
                entity.GetType().GetProperty("CreateAtUtc", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, DateTime.UtcNow);
                entity.GetType().GetProperty("CreateBy", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, GetCurrentLoginUserId());
                entity.GetType().GetProperty("UpdateAtUtc", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, DateTime.UtcNow);
                entity.GetType().GetProperty("UpdateBy", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, GetCurrentLoginUserId());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateAuditInfo<T>(ref T entity)
        {
            try
            {
                entity.GetType().GetProperty("UpdateAtUtc", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, DateTime.UtcNow);
                entity.GetType().GetProperty("UpdateBy", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, GetCurrentLoginUserId());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            try
            {
                var entity = _context.Set<TEntity>().Find(id);

                return entity;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            try
            {
                return _context.Set<TEntity>().Where(predicate).AsEnumerable();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<TEntity> GetList<TEntity>() where TEntity : class
        {
            try
            {
                return _context.Set<TEntity>()
                    .AsNoTracking()
                    .AsEnumerable();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ResultModel Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                AddAuditInfo<TEntity>(ref entity);

                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();

                return new ResultModel { Success = true, Message = "Insert success.", Data = entity };
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = ex.Message, Data = null };
            }
        }

        public ResultModel Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                UpdateAuditInfo<TEntity>(ref entity);

                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();

                return new ResultModel { Success = true, Message = "Update success.", Data = entity };
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = ex.Message };
            }
        }

        public ResultModel Update<TSource, TDestination>(TSource entity, object id) where TSource : class where TDestination : class
        {
            try
            {
                ResultModel result = new ResultModel();

                TDestination objDest = GetById<TDestination>(id);
                if (objDest != null)
                {
                    _mapper.Map<TSource, TDestination>(entity, objDest);
                    result = Update<TDestination>(objDest);
                }
                
                return result;
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = ex.Message };
            }
        }

        public ResultModel Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();

                return new ResultModel { Success = true, Message = "Delete success.", Data = entity };
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = ex.Message };
            }
        }

        public ResultModel Delete<TEntity>(object id) where TEntity : class
        {
            try
            {
                var typeInfo = typeof(TEntity).GetTypeInfo();
                var key = _context.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
                var property = typeInfo.GetProperty(key?.Name);
                if (property != null)
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    property.SetValue(entity, id);
                    _context.Entry(entity).State = EntityState.Deleted;
                }
                else
                {
                    var entity = _context.Set<TEntity>().Find(id);
                    if (entity != null) _context.Remove(entity);
                }

                _context.SaveChanges();

                return new ResultModel { Success = true, Message = "Delete success." };
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = ex.Message };
            }
        }
        

    }
}
