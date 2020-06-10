using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet.helpers
{
    public static class ParamsHelper
    {
        // Set necesary params from app
        public static ParamsModel ParseParams(string[] args)
        {
            ParamsModel _params = new ParamsModel(true);
            DefinedParamsModel definedParams = _params.basicParams;
            Dictionary<string, BaseParamModel> dicParams = ParseParamsFromCMD(args);

            definedParams.username = dicParams.ContainsKey("username") ? dicParams["username"] : new BaseParamModel();
            definedParams.password = dicParams.ContainsKey("password") ? dicParams["password"] : new BaseParamModel();
            definedParams.urlSite = dicParams.ContainsKey("urlSite") ? dicParams["urlSite"] : new BaseParamModel();
            definedParams.templateName = dicParams.ContainsKey("templateName") ? dicParams["templateName"] : new BaseParamModel();
            definedParams.dirTemplate = dicParams.ContainsKey("dirTemplate") ? dicParams["dirTemplate"] : new BaseParamModel();
            _params.basicParams = definedParams;

            PropertyInfo[] properties = definedParams.GetType().GetProperties();
            foreach (KeyValuePair<string, BaseParamModel> dicParam in dicParams)
            {
                PropertyInfo finded = properties.FirstOrDefault(x => { return x.Name == dicParam.Key; });
                if (finded == null) {
                    _params.others.Add(dicParam.Key, dicParam.Value);
                }
            }

            return _params;
        }

        // Validate necesary params from app
        public static ValidationParamsModel ValidateParams(DefinedParamsModel options)
        {
            ValidationParamsModel validation = new ValidationParamsModel(true, new String[] { });
            // Go over all defined properties in ParamsModel
            foreach(PropertyInfo property in options.GetType().GetProperties())
            {
                string name = property.Name;
                // Get object
                BaseParamModel properties = (BaseParamModel)property.GetValue(options, null);
                // Check if have validations
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
