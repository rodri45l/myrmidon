namespace TestMyrmidonAPI.MockClasses;

public static class IdentityMockHelper
{
    public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync((TUser user, string password) =>
        {
            if (string.IsNullOrEmpty(password))
            {
                return IdentityResult.Failed();
            }
            ls.Add(user);
            return IdentityResult.Success;
        });
        mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

        return mgr;
    }
}