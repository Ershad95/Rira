using Domain.DTO;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string NationalCode { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public bool IsDeleted { get; private set; }

        protected User(
            string firstName,
            string lastName,
            string nationalCode,
            DateTime birthDate,
            bool isDeleted)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            BirthDate = birthDate;
            IsDeleted = isDeleted;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Update(UpdateUserDto userDto)
        {
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            BirthDate = userDto.BirthDate;
            NationalCode = userDto.NationalCode;
        }

        public static User Create(CreateUserDto userDto)
        {
            return new User(
                userDto.FirstName,
                userDto.LastName,
                userDto.NationalCode,
                userDto.BirthDate,
                false);
        }
    }
}
