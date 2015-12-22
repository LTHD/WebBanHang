using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserAccountWebAPI.Models
{
    [Table("Users")]
    public class Users
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("FullName")]
        public string FullName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Mobile")]
        public string Mobile { get; set; }

        [Column("Birthday")]
        public Nullable<DateTime> Birthday { get; set; }

        [Column("Sex")]
        public string Sex { get; set; }

        [Column("Avatar")]
        public string Avatar { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("isAdmin")]
        public Nullable<int> isAdmin { get; set; }
    }

    public class AccountContext : DbContext 
    {
        public DbSet<Users> _Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Users>().HasKey<int>(i => i.Id);
        }


        public Users checkLogin(Users user)
        {
            Users result = findAccount(user.Email, user.Password);
            return result;
        }

        public int addAccount (Users user)
        {
            _Users.Add(user);
            return Save();
        }

        public int updateAccount(Users user)
        {
            Users acc = findAccount(user.Email, user.Password);
            if (acc != null)
            {
                acc.FullName = user.FullName;
                acc.Email = user.Email;
                acc.Address = user.Address;
                acc.Avatar = user.Avatar;
                acc.Birthday = user.Birthday;
                acc.Mobile = user.Mobile;
                acc.Sex = user.Sex;
                acc.Password = user.Password;
                acc.isAdmin = user.isAdmin;
                return Save();
            }
            return 0;
        }

        public int deleteAccount(Users user)
        {
            foreach (Users acc in _Users)
            {
                if (acc.Email == user.Email)
                {
                    _Users.Remove(acc);
                }
            }
            return Save();
        }

        public List<Users> getListUsers()
        {
            List<Users> users = _Users.ToList();
            return users;
        }

        private Users findAccount(string email, string password)
        {
            Users user = _Users.Where(acc => acc.Email == email && acc.Password == password).FirstOrDefault();
            return user;
        }

        private int Save()
        {
            try
            {
                return this.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}