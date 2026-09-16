using Core;
using Persistence;

namespace Application
{
    public class AppUserService
    {
        private IStore<AppUser, string> _appUserRepo;

        public AppUserService(IStore<AppUser, string> appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public async Task DeleteUser(string email)
        {
            var existedItem = await _appUserRepo.GetById(email);
            if (existedItem == null)
            {
                throw new Exception();
            }
            await _appUserRepo.DeleteById(email);
        }

        public async Task UpdateUser(string email, AppUser user)
        {
            var existedItem = await _appUserRepo.GetById(email);
            if (existedItem == null)
            {
                throw new Exception();
            }
            await _appUserRepo.UpdateById(email, user);
        }


        public Task<AppUser?> GetUserByEmail(string email)
        {
            return _appUserRepo.GetById(email);
        }

        public Task<List<AppUser>> GetUsers()
        {
            return _appUserRepo.GetObjects();
        }


        public Task Register(AppUser user, string password)
        {
            return _appUserRepo.Create(user, password);
        }
    }
}
