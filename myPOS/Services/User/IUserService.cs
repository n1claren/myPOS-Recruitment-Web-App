﻿namespace myPOS.Services.User
{
    public interface IUserService
    {
        public bool PhoneNumberInUse(string phoneNumber);

        public bool EmailInUse(string email);

        public int GetCreditBalance(string userId);

        public string GetUsername(string userId);
    }
}
