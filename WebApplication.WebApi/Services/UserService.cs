using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Classes;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Courses;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.Services
{
    public interface IUserService
    {
        Task<ApiResult<UserAuthenticate>> Authenticate(UserLoginRequest request);

        Task<PagedResultDto<UserVm>> GetListAsync(PagedAndSortedResultRequestDto request);

        Task<ApiResult<bool>> CreateAsync(CreateUserDto request);

        Task<ApiResult<UserVm>> UpdateAsync(Guid Id, UpdateUserDto request);

        Task<bool> DeleteAsync(Guid Id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<ApiResult<UserGetId>> GetById(Guid id);

        Task<bool> PermissionUser(Guid Id, string claimValue);

        Task<List<RoleVm>> GetListRole();

        Task<ApiResult<bool>> CourseAssign(CourseAssignRequest request);

        Task<ApiResult<bool>> ClassAssign(ClassAssignRequest request);

        Task<ApiResult<UserCourseVm>> GetCourseByIdUser(Guid Id);
        Task<ApiResult<UserClassVm>> GetCLassByIdUser(Guid Id);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ManagementDbContext _managementDbContext;

        public UserService(RoleManager<AppRole> roleManager, IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ManagementDbContext managementDbContext)
        {
            _roleManager = roleManager;
            _managementDbContext = managementDbContext;
            _mapper = mapper;
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        private string GenerateToken(AppUser user)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
            _configuration["JwtConfig:Audience"], claims, expires: DateTime.Now.AddHours(3), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApiResult<UserAuthenticate>> Authenticate(UserLoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<UserAuthenticate>("Can't find user in system");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded) return new ApiErrorResult<UserAuthenticate>("Password Wrong");
            var roles = await _userManager.GetRolesAsync(user);
            return new ApiSuccessResult<UserAuthenticate>(new UserAuthenticate
            {
                AccessToken = GenerateToken(user),
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            }
            );
        }

        public async Task<ApiResult<bool>> CreateAsync(CreateUserDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) return new ApiErrorResult<bool>("Tai khoan da ton tai");
            await _userManager.CreateAsync(_mapper.Map<AppUser>(request), request.Password);
            return new ApiSuccessResult<bool>();
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<PagedResultDto<UserVm>> GetListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _userManager.Users.Include(x => x.UserClasses).ThenInclude(x => x.Class).
                Include(x => x.UserCourses)
                .ThenInclude(x => x.Course).Include(x => x.AppUserRoles).ThenInclude(x => x.AppRole);

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.UserName.Contains(request.Filter) || x.Email.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(AppUser.UserName);
            if (request.SkipCount == 0) request.SkipCount = 1;
            if (request.MaxResultCount == 0) request.MaxResultCount = 10;
            var t = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).
                Take(request.MaxResultCount).ToListAsync();

            var user = t.Select(x => new UserVm()
            {
                Class = _mapper.Map<List<ClassVm>>(x.UserClasses.Select(x => x.Class)),
                FullName = x.FullName,
                Course = _mapper.Map<List<CourseVm>>(x.UserCourses.Select(x => x.Course)),
                Email = x.Email,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                Roles = _mapper.Map<List<RoleVm>>(x.AppUserRoles.Select(x => x.AppRole))
            }).ToList();
            return new PagedResultDto<UserVm> { Items = user, totalCount = t.Count };
        }

        public async Task<ApiResult<UserVm>> UpdateAsync(Guid Id, UpdateUserDto request)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) return new ApiErrorResult<UserVm>("Tafi khoan khong ton tai");
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            await _userManager.UpdateAsync(user);
            return new ApiSuccessResult<UserVm>(_mapper.Map<UserVm>(user));
        }

        public async Task<bool> PermissionUser(Guid Id, string claimValue)
        {
            //var user = await _userManager.FindByIdAsync(Id.ToString());
            //if (user == null) return false;
            ////var claims = new Claim(CustomClaimTypes.Permission, claimValue);
            //await _userManager.AddClaimAsync(user, claims);
            //return true;
            throw new Exception();
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<UserGetId>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserGetId>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserGetId()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserGetId>(userVm);
        }

        public async Task<List<RoleVm>> GetListRole()
        {
            var query = await _roleManager.Roles.Include(x => x.AppUserRoles).ThenInclude(x => x.AppUser).ToListAsync();
            var result = query.Select(x =>new RoleVm
            {
                Users=_mapper.Map<List<UserVm>>(x.AppUserRoles.Select(x=>x.AppUser)),
                Id=x.Id,
                Name=x.Name
            }).ToList();
            return result;
        }

        public async Task<ApiResult<bool>> CourseAssign(CourseAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var course = await _managementDbContext.Courses.FindAsync(request.CourseId);
            if (user != null && course != null)
            {
                var userCourse = new UserCourse()
                {
                    AppUser = user,
                    Course = course,
                    CourseId = course.Id,
                    UserId = user.Id,
                    UpdateTime = DateTime.Now,
                };
                await _managementDbContext.UserCourses.AddAsync(userCourse);
                await _managementDbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("User or Course can't find !!");
        }

        public async Task<ApiResult<bool>> ClassAssign(ClassAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var @class = await _managementDbContext.Classes.FindAsync(request.ClassId);
            if (user != null && @class != null)
            {
                var userClasscheck = await _managementDbContext.UserClasses.
                    FirstOrDefaultAsync(x => x.ClassId.Equals(@class.Id) && x.UserId.Equals(user.Id));
                if (userClasscheck != null) return new ApiErrorResult<bool>("Try with another diffirence class");
                var userClass = new UserClass()
                {
                    AppUser = user,
                    Class = @class,
                    ClassId = @class.Id,
                    UserId = user.Id,
                    UpdateTime = DateTime.Now,
                };
                await _managementDbContext.UserClasses.AddAsync(userClass);
                await _managementDbContext.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("User or Course can't find !!");
        }

        public async Task<ApiResult<UserCourseVm>> GetCourseByIdUser(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) return new ApiErrorResult<UserCourseVm>("can't find user");
            var query = from u in _userManager.Users
                        join uc in _managementDbContext.UserCourses on u.Id equals uc.UserId
                        join c in _managementDbContext.Courses on uc.CourseId equals c.Id
                        where u.Id.Equals(Id)
                        select new { u, uc, c };
            var result = await query.Select(x => new UserCourseVm()
            {
                Courses = _mapper.Map<List<CourseVm>>(x.u.UserCourses.Select(x => x.Course)),
                Email = x.u.Email,
                FullName = x.u.FullName,
                Id = x.u.Id,
                PhoneNumber = x.u.PhoneNumber,
                UserName = x.u.UserName
            }).FirstOrDefaultAsync();

            return new ApiSuccessResult<UserCourseVm>(result);
        }

        public async Task<ApiResult<UserClassVm>> GetCLassByIdUser(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) return new ApiErrorResult<UserClassVm>("can't find user");
            var query = from u in _userManager.Users
                        join uc in _managementDbContext.UserClasses on u.Id equals uc.UserId
                        join c in _managementDbContext.Classes on uc.ClassId equals c.Id
                        where u.Id.Equals(Id)
                        select new { u, uc, c };
            var result = await query.Select(x => new UserClassVm()
            {
                Classes = _mapper.Map<List<ClassVm>>(x.u.UserClasses.Select(x => x.Class)),
                Email = x.u.Email,
                FullName = x.u.FullName,
                Id = x.u.Id,
                PhoneNumber = x.u.PhoneNumber,
                UserName = x.u.UserName
            }).FirstOrDefaultAsync();

            return new ApiSuccessResult<UserClassVm>(result);
        }
    }
}