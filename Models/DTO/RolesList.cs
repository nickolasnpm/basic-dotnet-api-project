using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class RolesList
    {
        private string _title;

        [StringLength(8, MinimumLength = 6,
            ErrorMessage = "There is no such role in our system")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                string input = value.ToLower();

                if (input == "reader" || input == "writer")
                {
                    _title = input;
                }
                else
                {
                    _title = "";
                }
            }
        }
    }
}
