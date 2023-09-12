using System.ComponentModel.DataAnnotations;

namespace webapi_playground.Models.DTOs.Auth;

public class Roles
{
    private string _title;
    private List<string> availableTitle = new List<string>() {"principal", "teacher", "student"};

    [StringLength(9, MinimumLength = 7,
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

            if (availableTitle.Contains(input))
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
