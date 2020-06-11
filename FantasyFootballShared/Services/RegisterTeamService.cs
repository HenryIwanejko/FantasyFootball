using System;
using System.Collections.Generic;
using System.Text;
using Android.Widget;
using FantasyFootballShared.Utilities;
using Java.Security;

namespace FantasyFootballShared
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
                if (_sqlLiteRepository.GetFantasyTeams().Count < 2)
                {
                    FantasyTeam fantasyTeam = new FantasyTeam(fantasyTeamName, managerFirstName, managerLastName);
                    if (!VerifyFantasyTeamName(fantasyTeam))
                    {
                        return new KeyValuePair<bool, string>(false, "Fantasy Team Name Already Exists");
                    }
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

        public bool VerifyFantasyTeamName(FantasyTeam registeringTeam)
        {
            List<FantasyTeam> fantasyTeams = _sqlLiteRepository.GetFantasyTeams();
            foreach(FantasyTeam team in fantasyTeams)
            {
                if (team.FantasyTeamName == registeringTeam.FantasyTeamName)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
