using System;

namespace ProvisioningPnp.helpers
{
    public static class ValidateHelper
    {
        public static  string ValidateField(string name, string value, string[] validations)
        {
            string errMsg = "";

            foreach(string validation in validations)
            {
                switch (validation)
                {
                    case "required": {
                        string result = ValidateRequiredField(name, value);
                        errMsg = !string.IsNullOrEmpty(result) ? string.Concat(errMsg, ", ", result) : errMsg;
                        break;
                    }
                    case "email": {
                        string result = ValidateEmailField(name, value);
                        errMsg = !string.IsNullOrEmpty(result) ? string.Concat(errMsg, ", ", result) : errMsg;
                        break;
                    }
                    case "url": {
                        string result = ValidateURLField(name, value);
                        errMsg = !string.IsNullOrEmpty(result) ? string.Concat(errMsg, ", ", result) : errMsg;
                        break;
                    }
                    default: {
                        LogHelper.writeWarning(string.Concat("Validation ", validation, "not valid in field: ", name));
                        break;
                    }
                }
            }

            return errMsg;
        }

        private static string ValidateRequiredField(string name, string value)
        {
            string msg = "";
            if (string.IsNullOrEmpty(value)) {
                msg = string.Concat("The field: ", name, " can't be empty.");
            }
            return msg;
        }

        private static string ValidateEmailField(string name, string value)
        {
            string msg = "";

            try {
                var addr = new System.Net.Mail.MailAddress(value);
                bool check = addr.Address == value;
            }
            catch {
                msg = string.Concat("The field: ", name, " with value: ", value, " is not valid email.");
            }

            return msg;
        }

        private static string ValidateURLField(string name, string value)
        {
            string msg = "";
            Uri uriResult;

            if (!(Uri.TryCreate(value, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                msg = string.Concat("The field: ", name, " with value: ", value, " is not valid url.");
            }

            return msg;
        }
    }
}
