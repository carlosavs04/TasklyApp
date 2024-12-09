using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Taskly_App.Helpers
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            var snakeCase = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                var currentChar = name[i];
                if (char.IsUpper(currentChar))
                {
                    if (i > 0)
                        snakeCase.Append('_');

                    snakeCase.Append(char.ToLower(currentChar));
                }
                else
                    snakeCase.Append(currentChar);
            }

            return snakeCase.ToString();
        }
    }
}
