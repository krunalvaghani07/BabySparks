using BabySparksSharedClassLibrary.Models;
using System;

namespace BabySparksSharedClassLibrary.ServiceProvider
{
    public class AppState
    {
        public event EventHandler<UserChangedEventArgs> UserChanged;
        public bool IsWeb { get; set; }
        public bool IsDesktop { get; set; }

        public bool IsAuthenticated { get; set; }

        private User _user;

        public User user
        {
            get => _user;
            set
            {
                _user = value;
                OnUserChanged(new UserChangedEventArgs(value));
            }
        }

        protected virtual void OnUserChanged(UserChangedEventArgs e)
        {
            UserChanged?.Invoke(this, e);
        }
    }

    public class UserChangedEventArgs : EventArgs
    {
        public User NewUser { get; }

        public UserChangedEventArgs(User newUser)
        {
            NewUser = newUser;
        }
    }
}
