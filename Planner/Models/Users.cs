using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Models
{
    public class Users
    {
        public static void Create(Register register)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var salt = Crypt.GenerateSalt();
                var user = new User
                {
                    Uuid = Guid.NewGuid(),
                    EmailAddress = register.EmailAddress.ToLowerInvariant(),
                    Offset = register.Offset,
                    Status = "ACTIVE",
                    Created = DateTime.UtcNow,
                    UserPasswords = new List<UserPassword>
                    {
                        new UserPassword
                        {
                            PasswordSalt = salt,
                            PasswordHash = Crypt.GenerateHash(register.Password, salt),
                            Status = "ACTIVE",
                            Created = DateTime.UtcNow
                        }
                    }
                };
                dbContext.Users.Add(user);
                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException exception)
                {
                    var s = "";
                    foreach (var eve in exception.EntityValidationErrors)
                    {
                        s +=
                            string.Format(
                                "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            s += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
        }

        public static long Login(string emailAddress, string password)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var user =
                    dbContext.Set<User>()
                        .Where(_ => _.EmailAddress == emailAddress && _.Status == "ACTIVE")
                        .FirstOrDefault();
                if (user != null)
                {
                    var userPassword =
                        dbContext.Set<UserPassword>()
                            .Where(_ => _.UserId == user.Id && _.Status == "ACTIVE")
                            .FirstOrDefault();
                    if (userPassword != null)
                    {
                        if (Crypt.VerifyPassword(password, userPassword.PasswordSalt, userPassword.PasswordHash))
                        {
                            return user.Id;
                        }
                    }
                }
            }
            return -1;
        }

        public static User Get(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<User>().Where(_ => _.Id == userId).FirstOrDefault();
            }
        }

        public static void UpdatePassword(long userId, string newPassword)
        {
            using (var dbContext = new PlannerDbContext())
            {
                using (var dbTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userPassword = dbContext.Set<UserPassword>().Where(_ => _.UserId == userId).FirstOrDefault();
                        if (userPassword == null)
                        {
                            return;
                        }

                        var salt = Crypt.GenerateSalt();
                        userPassword.PasswordSalt = salt;
                        userPassword.PasswordHash = Crypt.GenerateHash(newPassword, salt);
                        userPassword.Updated = DateTime.UtcNow;

                        dbContext.Entry(userPassword).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }
        }
    }
}