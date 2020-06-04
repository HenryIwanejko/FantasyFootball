using System;
using System.Collections.Generic;
using System.Text;
using Android.Widget;
using FantasyFootballShared.Utilities;
using Java.Security;

namespace FantasyFootballShared.Services
{
    public class RegisterTeamService
    {
        private readonly ISQLiteRepository _sqlLiteRepository;

        public RegisterTeamService()
        {
            _sqlLiteRepository = new SQLiteRepository();
        }

        private static KeyValuePair<bool, string> ValidateFields(string fantasyTeamName, string managerFirstName, string managerLastName)
        {
            if (Util.ValidateText(fantasyTeamName, managerFirstName, managerLastName))
            {
                return new KeyValuePair<bool, string>(true, "");
            }
            else
            {
                return new KeyValuePair<bool, string>(false, "Please enter valid inputs into the fields"); ;
            }
        }

        public KeyValuePair<bool, string> InsertedTeamToDatabase(string fantasyTeamName, string managerFirstName, string managerLastName)
        {
            KeyValuePair<bool, string> validatedFields = ValidateFields(fantasyTeamName, managerFirstName, managerLastName);
            if (validatedFields.Key == true)
            {
                int nextFantasyId = _sqlLiteRepository.GetNextFantasyTeamId();
                if (nextFantasyId < 2)
                {
                    FantasyTeam fantasyTeam = new FantasyTeam(nextFantasyId, fantasyTeamName, managerFirstName, managerLastName);
                    int dbResponse = _sqlLiteRepository.AddFantasyTeam(fantasyTeam);
                    if (dbResponse == 1)
                    {
                        return new KeyValuePair<bool, string>(true, ""); ;

                    }
                    else
                    {
                        return new KeyValuePair<bool, string>(false, "Error contacting the database");
                    }
                }
                else
                {
                    return new KeyValuePair<bool, string>(false, "Only 2 teams can be registered, delete one on the admin page to register");
                }
            }
            else
            {
                return validatedFields;
            }
        }
    }
}
