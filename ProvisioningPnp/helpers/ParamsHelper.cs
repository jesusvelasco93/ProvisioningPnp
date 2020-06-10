using System;
using System.Collections.Generic;
using System.Linq;
using ProvisioningPnp.models;


namespace ProvisioningPnp.helpers
{
    public static class ParamsHelper
    {
        // Set necesary params from app
        public static ParamsModel ParseParams(string[] args)
        {
            ParamsModel _params = new ParamsModel();
            Dictionary<string, BaseParamModel> dicParams = ParseParamsFromCMD(args);

            _params.username = dicParams.ContainsKey("username") ? dicParams["username"] : new BaseParamModel();
            _params.password = dicParams.ContainsKey("password") ? dicParams["password"] : new BaseParamModel();
            _params.urlSite = dicParams.ContainsKey("urlSite") ? dicParams["urlSite"] : new BaseParamModel();
            _params.templateName = dicParams.ContainsKey("templateName") ? dicParams["templateName"] : new BaseParamModel();
            _params.dirTemplate = dicParams.ContainsKey("dirTemplate") ? dicParams["dirTemplate"] : new BaseParamModel();

            return _params;
        }

        // Validate necesary params from app
        public static ValidationParamsModel ValidateParams(ParamsModel options)
        {
            ValidationParamsModel validation = new ValidationParamsModel(true, new String[0]);

            foreach(var property in options.GetType().GetProperties())
            {
                string name = property.Name;
                BaseParamModel properties = (BaseParamModel) property.GetValue(options, null);

                if (properties.validations?.Length > 0)
                {
                    string errMsg = ValidateHelper.ValidateField(name, properties.value, properties.validations);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        validation.valid = false;
                        validation.errors.Append(errMsg);
                    }
                }

            }

            return validation;
        }

        // Parse array string into dictionary array
        private static Dictionary<string, BaseParamModel> ParseParamsFromCMD(string[] args)
        {
            Dictionary<string, BaseParamModel> dicArgs = new Dictionary<string, BaseParamModel> { };

            for (int i = 0; i < args.Length; i++) {
                if (args[i].IndexOf("--") == 0) {

                    string simplyName = args[i].Substring(2);
                    BaseParamModel _param = new BaseParamModel() { 
                        validations = SettingsHelper.GetSettingArray(simplyName)
                    };

                    if (args[i + 1].IndexOf("--") == 0) {
                        _param.value = "true";
                    } else {
                        _param.value = args[i + 1];
                        i++;
                    }
                    dicArgs.Add(simplyName, _param);
                }
            }

            return dicArgs;
        }
    }
}
