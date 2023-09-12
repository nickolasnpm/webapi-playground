using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace webapi_playground.Controllers.Services;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    [HttpGet("datetime")]
    public IActionResult GetDateTime()
    {
        return Ok(DateTime.Now);
    }

    [HttpGet("guid")]
    public IActionResult GetGuid()
    {
        return Ok(Guid.NewGuid());
    }

    [HttpGet("random-number")]
    public IActionResult GetRandomNumber()
    {
        Random random = new Random();
        return Ok(random.Next(1000));
    }

    [HttpGet("random-password")]
    public IActionResult GetRandomPassword()
    {
        const string letB = "ABCDFGHIJKLMNOPQRSTUVWXYZ";
        const string letS = "abcdefghijklmnopqrstuvwxyz";
        const string num = "1234567890";
        const string chr = "!@#$%^&*_";

        int indexLetB = 0;
        int indexLetS = 0;
        int indexNum = 0;
        int indexChr = 0;

        var choiceList = new List<string> { num, letB, letS, chr };

        StringBuilder password = new StringBuilder();
        Random random = new Random();
        int passwordLength = 10;

        while (indexNum < 1 && indexLetB < 1 && indexLetS < 1 && indexChr < 1)
        {
            password = new();
            indexLetB = 0;
            indexLetS = 0;
            indexNum = 0;
            indexChr = 0;

            for (int i = 0; i < passwordLength; i++)
            {
                int listIndex = random.Next(choiceList.Count);
                string choosenStr = choiceList[listIndex];

                if (choosenStr == letB)
                {
                    indexLetB++;
                }
                else if (choosenStr == letB)
                {
                    indexLetB++;
                }
                else if (choosenStr == num)
                {
                    indexNum++;
                }
                else
                {
                    indexChr++;
                }

                int characterIndex = random.Next(choosenStr.Length);
                password.Append(choosenStr[characterIndex]);
            }
        }
        return Ok(password.ToString());
    }
}
